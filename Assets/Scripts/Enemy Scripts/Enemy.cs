using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float enemyHealth;
    [SerializeField] private float enemyDamage;
    [SerializeField] private int XPCount;
    [SerializeField] private Slider slider;
    private float curHealth;
    private Transform player;
    private bool isDamaging;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        if (player == null)
        {
            Debug.LogError("Player not found. Make sure the player has the tag 'Player'.");
        }

        curHealth = enemyHealth;
    }

    void Update()
    {
        if (player != null)
        {
            Vector3 directionToPlayer = player.position - transform.position;
            Vector3 normalizedDirection = directionToPlayer.normalized;
            transform.Translate(normalizedDirection * moveSpeed * Time.deltaTime);
        }
    }

    private void OnCollisionStay2D(Collision2D other) 
    {
        if (other.transform.tag == "Player" && !isDamaging)
        {
            StartCoroutine(DamageCooldown());
            PlayerManager.Instance.DamagePlayerHealth(enemyDamage);
        }    
    }

    IEnumerator DamageCooldown()
    {
        isDamaging = true;
        yield return new WaitForSeconds(0.5f);
        isDamaging = false;
    }

    public void DamageEnemy(float damageVal) 
    {
        curHealth -= damageVal;
        if (curHealth <= 0) 
        {
            ResetEnemy();
            PlayerManager.Instance.GivePlayerXP(XPCount);
            EnemyManager.Instance.DestroyEnemy(this);
        }
        UpdateHealthbar(curHealth, enemyHealth);
    }

    public void UpdateHealthbar(float currentVal, float maxVal)
    {
        slider.value = currentVal / maxVal;
    }

    private void ResetEnemy() 
    {
        curHealth = enemyHealth;
        UpdateHealthbar(enemyHealth, enemyHealth);
    }
}

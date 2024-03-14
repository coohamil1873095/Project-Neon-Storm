using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float enemyHealth;
    [SerializeField] private float enemyDamage;
    [SerializeField] private float beingDamagedCooldown; 
    [SerializeField] private int XPCount;
    [SerializeField] private Slider slider;
    private float curHealth;
    private Transform player;
    private bool isBeingDamaged = false;
    private bool isDamagingPlayer = false;

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
        if (other.transform.tag == "Player" && !isDamagingPlayer)
        {
            StartCoroutine(DamageCooldown());
            PlayerManager.Instance.DamagePlayerHealth(enemyDamage);
        }    
    }

    IEnumerator DamageCooldown()
    {
        isDamagingPlayer = true;
        yield return new WaitForSeconds(0.5f);
        isDamagingPlayer = false;
    }

    public void DamageEnemy(float damageVal) 
    {
        curHealth -= damageVal;
        StartCoroutine(PlayerDamagingCooldown(beingDamagedCooldown));
        if (curHealth <= 0) 
        {
            ResetEnemy();
            PlayerManager.Instance.GivePlayerXP(XPCount);
            EnemyManager.Instance.DestroyEnemy(this);
        }
        UpdateHealthbar(curHealth, enemyHealth);

        
    }

    IEnumerator PlayerDamagingCooldown(float cooldown)
    {
        isBeingDamaged = true;
        yield return new WaitForSeconds(cooldown);
        isBeingDamaged = false;
    }

    public bool EnemyBeingDamaged()
    {
        return isBeingDamaged;
    }

    public void UpdateHealthbar(float currentVal, float maxVal)
    {
        slider.value = currentVal / maxVal;
    }

    private void ResetEnemy() 
    {
        curHealth = enemyHealth;
        isBeingDamaged = false;
        UpdateHealthbar(enemyHealth, enemyHealth);
    }
}

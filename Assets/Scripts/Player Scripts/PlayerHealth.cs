using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private Image healthBar;
    [SerializeField] private AudioClip damageSFX;
    private float currentHealth;
    
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.isPlaying && currentHealth <= 0) 
            GameManager.Instance.EndCurrentGame(false);
    }

    public void DamagePlayer(float damageAmount) 
    {
        currentHealth -= damageAmount;
        healthBar.fillAmount = currentHealth / maxHealth;

        //SFXManager.Instance.PlaySFXClip(damageSFX, transform, 1f);
    }

    public void HealPlayer(float healAmount)
    {
        currentHealth += healAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        healthBar.fillAmount = currentHealth / maxHealth;
    }

    public void ResetPlayerHealth()
    {
        currentHealth = maxHealth;

        healthBar.fillAmount = maxHealth;
    }
}

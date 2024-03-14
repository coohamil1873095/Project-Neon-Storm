using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPowers : MonoBehaviour
{
    public GameObject ringPrefab; // Drag your ring prefab into this field in the Unity Editor
    private GameObject ring; // Reference to the instantiated ring
    public float pushForce = 5f;
    public float powerupCooldown = 5f;
    public float powerupCooldown2 = 10f;
    public float powerupCooldown3 = 10f;
    public float powerupCooldown4 = 60f;
    public float ringUptime = 0.5f;
    private bool isAbility1Cooldown = false;
    private bool isAbility1Active = false;
    private bool isAbility2Cooldown = false;
    //private bool isAbility2Active = false;
    private bool isAbility3Cooldown = false;
    //private bool isAbility3Active = false;
    private bool isAbility4Cooldown = false;
    //private bool isAbility4Active = false;


    [Header("Ability 1")]
    public bool ability1Unlocked = false;
    public Image abilityImage1;
    public KeyCode ability1Key;
    private float currentAbilityCooldown;

    [Header("Ability 2")]
    public bool ability2Unlocked = false;
    public Image abilityImage2;
    public KeyCode ability2Key;
    private float currentAbilityCooldown2;
    public GameObject lightningCirclePrefab;
    public float circleDuration = 1f;
    public float circleRadius = 3f;
    public float damageAmount = 1.5f;

    [Header("Ability 3")]
    public bool ability3Unlocked = false;
    public Image abilityImage3;
    public KeyCode ability3Key;
    private float currentAbilityCooldown3;
    public float movementSpeedBoost = 1f;
    public float damageBoost = 1f;
    public float abilityDuration = 3f;
    private float originalMovementSpeed;
    private float originalDamage;
    private PlayerMove playerMovement;
    private PlayerDetection playerDetection;

    [Header("Ability 4")]
    public bool ability4Unlocked = false;
    public Image abilityImage4;
    public KeyCode ability4Key;
    private float currentAbilityCooldown4;
    public GameObject lightningUltimatePrefab;
    public float UltDuration = 1f;
    public float UltRadius = 3f;
    public float ultDamageAmount = 1.5f;

    private PlayerAudio m_SFX;

    void Start()
    {
        ring = Instantiate(ringPrefab, transform.position, Quaternion.identity);
        ring.SetActive(false);
        ring.transform.parent = transform;

        playerMovement = GetComponent<PlayerMove>();
        playerDetection = GetComponent<PlayerDetection>();
        originalMovementSpeed = playerMovement.moveSpeed;
        originalDamage = playerDetection.playerWeaponDamage;

        abilityImage1.fillAmount = 100;
        abilityImage2.fillAmount = 100;
        abilityImage3.fillAmount = 100;
        abilityImage4.fillAmount = 100;

        m_SFX = GetComponent<PlayerAudio>();
    }

    void Update()
    {
        Ability1Input();
        AbilityCooldown(ref currentAbilityCooldown, powerupCooldown, ref isAbility1Cooldown, abilityImage1);
        Ability2Input();
        AbilityCooldown(ref currentAbilityCooldown2, powerupCooldown2, ref isAbility2Cooldown, abilityImage2);
        Ability3Input();
        AbilityCooldown(ref currentAbilityCooldown3, powerupCooldown3, ref isAbility3Cooldown, abilityImage3);
        Ability4Input();
        AbilityCooldown(ref currentAbilityCooldown4, powerupCooldown4, ref isAbility4Cooldown, abilityImage4);
    }

    public void ResetPlayerPowers()
    {
        //remove powers from player
    }

    private void Ability1Input()
    {
        if (Input.GetKeyDown(ability1Key) && ability1Unlocked)
        {
            if (!isAbility1Cooldown)
            {
                //Debug.Log("activate!");
                ActivatePower1();
                m_SFX.playSound(m_SFX.Ability1Clip);
                isAbility1Cooldown = true;
                currentAbilityCooldown = powerupCooldown;
            }
            else
            {
                m_SFX.playSound(m_SFX.noAbilityClip);
            }
        }
    }
    private void Ability2Input()
    {
        if (Input.GetKeyDown(ability2Key) && ability2Unlocked)
        {
            if (!isAbility2Cooldown)
            {
                //Debug.Log("activate!");
                ActivatePower2();
                m_SFX.playSound(m_SFX.Ability2Clip);
                isAbility2Cooldown = true;
                currentAbilityCooldown2 = powerupCooldown2;
            }
            else
            {
                m_SFX.playSound(m_SFX.noAbilityClip);
            }

        }
    }
    private void Ability3Input()
    {
        if (Input.GetKeyDown(ability3Key) && ability3Unlocked)
        {
            if (!isAbility3Cooldown)
            {
                //Debug.Log("activate!");
                ActivatePower3();
                m_SFX.playSound(m_SFX.Ability3Clip);
                isAbility3Cooldown = true;
                currentAbilityCooldown3 = powerupCooldown3;
            }
            else
            {
                m_SFX.playSound(m_SFX.noAbilityClip);
            }

        }
    }
    private void Ability4Input()
    {
        if (Input.GetKeyDown(ability4Key) && ability4Unlocked)
        {
            if (!isAbility4Cooldown)
            {
                //Debug.Log("activate!");
                ActivatePower4();
                m_SFX.playSound(m_SFX.Ability4Clip);
                isAbility4Cooldown = true;
                currentAbilityCooldown4 = powerupCooldown4;
            }
            else
            {
                m_SFX.playSound(m_SFX.noAbilityClip);
            }
        }
    }

    void ActivatePower1()
    {
        ring.SetActive(true);
        StartCoroutine(DisableRingAfterDelay(ringUptime));
    }


    void ActivatePower2()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;
        GameObject lightningCircle = Instantiate(lightningCirclePrefab, mousePosition, Quaternion.identity);

        lightningCircle.transform.localScale = new Vector3(circleRadius * 2f, circleRadius * 2f, 1f);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(mousePosition, circleRadius);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                collider.GetComponent<Enemy>().DamageEnemy(damageAmount);
            }
        }

        // Remove the lightning circle after a certain duration
        StartCoroutine(DestroyAfterDelay(lightningCircle, circleDuration));
    }

    void ActivatePower3()
    {
        //isAbility3Active = true;

        // Increase movement speed and damage
        playerMovement.moveSpeed += movementSpeedBoost;
        playerDetection.playerWeaponDamage += damageBoost;

        // Start ability duration and cooldown timers
        StartCoroutine(DisableAbilityAfterDuration(abilityDuration));
        StartCoroutine(StartAbilityCooldown(powerupCooldown3));
    }
    void ActivatePower4()
    {
        // Spawn the blast effect at the player's position
        GameObject blastEffect = Instantiate(lightningUltimatePrefab, transform.position, Quaternion.identity);
        blastEffect.transform.localScale = new Vector3(UltRadius * 2f, UltRadius * 2f, 1f);
        // Detect enemies within the blast radius and damage them
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, UltRadius);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                collider.GetComponent<Enemy>().DamageEnemy(ultDamageAmount);
            }
        }

        // Remove the blast effect after a certain duration
        StartCoroutine(DestroyAfterDelay(blastEffect, UltDuration));
    }

    private void AbilityCooldown(ref float currentCooldown, float maxCooldown, ref bool isCooldown, Image skillImage)
    {
        if (isCooldown)
        {
            currentCooldown -= Time.deltaTime;
            if (currentCooldown <= 0f)
            {
                isCooldown = false;
                currentCooldown = 0f;
                if (skillImage != null)
                {
                    skillImage.fillAmount = 0f;
                }
            }
            else
            {
                if (skillImage != null)
                {
                    skillImage.fillAmount = currentCooldown / maxCooldown;
                }
            }
        }

    }

    /*IEnumerator PowerupCooldown()
    {
        canActivatePower = false;
        yield return new WaitForSeconds(powerupCooldown);
        canActivatePower = true;
    }*/

    IEnumerator DestroyAfterDelay(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(obj);
    }

    IEnumerator DisableRingAfterDelay(float delay)
    {
        isAbility1Active = true;
        yield return new WaitForSeconds(delay);
        isAbility1Active = false;
        DisableRing();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag == "Enemy" && isAbility1Active)
        {
            Rigidbody2D enemyRb = other.collider.GetComponent<Rigidbody2D>();
            if (enemyRb != null)
            {
                Vector2 pushDirection = (other.transform.position - transform.position).normalized;
                enemyRb.AddForce(pushDirection * pushForce, ForceMode2D.Impulse);

                // Start a coroutine to remove the force after 0.5 seconds
                StartCoroutine(RemoveForceAfterDuration(other.collider, 0.35f));
            }
        }
    }

    IEnumerator RemoveForceAfterDuration(Collider2D col, float duration)
    {
        col.enabled = false;
        yield return new WaitForSeconds(duration);
        col.enabled = true;

        // Remove the force applied earlier
        col.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    IEnumerator DisableAbilityAfterDuration(float duration)
    {
        yield return new WaitForSeconds(duration);

        // Reset movement speed and damage
        playerMovement.moveSpeed = originalMovementSpeed;
        playerDetection.playerWeaponDamage = originalDamage;

        //isAbility3Active = false;
    }

    IEnumerator StartAbilityCooldown(float cooldown)
    {
        isAbility3Cooldown = true;
        yield return new WaitForSeconds(cooldown);
        isAbility3Cooldown = false;
    }

    // Method to disable the ring
    void DisableRing()
    {
        ring.SetActive(false);
    }

    public void LevelPush()
    {
        pushForce += 1f;
        powerupCooldown -= 0.5f;
        ringUptime += 0.5f;
        AbilityLevelManager.Instance.Ability1LevelUp();
    }
    public void LevelAb2()
    {
        circleRadius += 0.2f;
        powerupCooldown -= 0.5f;
        damageAmount += damageAmount * 2 / GameManager.Instance.GetCurrentLevel();
        AbilityLevelManager.Instance.Ability2LevelUp();
    }
    public void LevelAb3()
    {
        movementSpeedBoost += 0.5f;
        damageBoost += 0.5f;
        powerupCooldown3 -= 0.5f;
        abilityDuration += 0.25f;
        AbilityLevelManager.Instance.Ability3LevelUp();
    }
    public void LevelAb4()
    {
        UltRadius += 0.5f;
        ultDamageAmount += ultDamageAmount * 2 / GameManager.Instance.GetCurrentLevel();
        powerupCooldown4 -= 3f;
        AbilityLevelManager.Instance.Ability4LevelUp();
    }
    public void UnlockAb1()
    {
        ability1Unlocked = true;
        abilityImage1.fillAmount = 0;
        AbilityLevelManager.Instance.Ability1LevelUp();
    }
    public void UnlockAb2()
    {
        ability2Unlocked = true;
        abilityImage2.fillAmount = 0;
        AbilityLevelManager.Instance.Ability2LevelUp();
    }
    public void UnlockAb3()
    {
        ability3Unlocked = true;
        abilityImage3.fillAmount = 0;
        AbilityLevelManager.Instance.Ability3LevelUp();
    }
    public void UnlockAb4()
    {
        ability4Unlocked = true;
        abilityImage4.fillAmount = 0;
        AbilityLevelManager.Instance.Ability4LevelUp();
    }
}
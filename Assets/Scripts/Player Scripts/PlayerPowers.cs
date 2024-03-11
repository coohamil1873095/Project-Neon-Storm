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
    public float powerupCooldown2 = 5f;
    public float ringUptime = 0.5f;
    private bool isAbility1Cooldown = false;
    private bool isAbility2Cooldown = false;

    [Header("Ability 1")]
    public Image abilityImage1;
    public KeyCode ability1Key;
    private float currentAbilityCooldown;

    [Header("Ability 2")]
    public Image abilityImage2;
    public KeyCode ability2Key;
    private float currentAbilityCooldown2;

    void Start()
    {
        ring = Instantiate(ringPrefab, transform.position, Quaternion.identity);
        ring.SetActive(false);
        ring.transform.parent = transform;

        abilityImage1.fillAmount = 0;
        abilityImage2.fillAmount = 0;
    }

    void Update()
    {
        Ability1Input();
        AbilityCooldown(ref currentAbilityCooldown, powerupCooldown, ref isAbility1Cooldown, abilityImage1);
        Ability2Input();
        AbilityCooldown2(ref currentAbilityCooldown2, powerupCooldown2, ref isAbility2Cooldown, abilityImage2);
    }
    private void Ability1Input()
    {
        if (Input.GetKeyDown(ability1Key) && !isAbility1Cooldown)
        {
            Debug.Log("activate!");
            ActivatePower1();
            isAbility1Cooldown = true;
            currentAbilityCooldown = powerupCooldown;

        }
    }
    private void Ability2Input()
    {
        if (Input.GetKeyDown(ability2Key) && !isAbility2Cooldown)
        {
            Debug.Log("activate!");
            //ActivatePower2();
            isAbility2Cooldown = true;
            currentAbilityCooldown2 = powerupCooldown2;

        }
    }

    void ActivatePower1()
    {
        ring.SetActive(true);
        StartCoroutine(DisableRingAfterDelay(ringUptime));
        //StartCoroutine(PowerupCooldown());
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

    private void AbilityCooldown2(ref float currentCooldown, float maxCooldown, ref bool isCooldown, Image skillImage)
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

    IEnumerator DisableRingAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        DisableRing();
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.collider.tag == "Enemy")
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

    // Method to disable the ring
    void DisableRing()
    {
        ring.SetActive(false);
    }

    public void LevelPush()
    {
        pushForce += 1f;
        powerupCooldown -= 1f;
        ringUptime += 0.5f;
    }
}
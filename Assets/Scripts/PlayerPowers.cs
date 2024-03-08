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
    public float ringUptime = 0.5f;
    private bool isAbility1Cooldown = false;

    [Header("Ability 1")]
    public Image abilityImage1;
    public KeyCode ability1Key;
    private float currentAbilityCooldown;

    void Start()
    {
        ring = Instantiate(ringPrefab, transform.position, Quaternion.identity);
        ring.SetActive(false);
        ring.transform.parent = transform;

        abilityImage1.fillAmount = 0;
    }

    void Update()
    {
        Ability1Input();
        AbilityCooldown(ref currentAbilityCooldown, powerupCooldown, ref isAbility1Cooldown, abilityImage1);
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

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Rigidbody2D enemyRb = other.GetComponent<Rigidbody2D>();
            if (enemyRb != null)
            {
                Vector2 pushDirection = (other.transform.position - transform.position).normalized;
                enemyRb.AddForce(pushDirection * pushForce, ForceMode2D.Impulse);

                // Start a coroutine to remove the force after 0.5 seconds
                StartCoroutine(RemoveForceAfterDuration(enemyRb, pushDirection, 0.35f));
            }
        }
    }

    IEnumerator RemoveForceAfterDuration(Rigidbody2D rb, Vector2 forceDirection, float duration)
    {
        yield return new WaitForSeconds(duration);

        // Remove the force applied earlier
        rb.velocity = Vector2.zero;
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
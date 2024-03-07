using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPowers : MonoBehaviour
{
    public GameObject ringPrefab; // Drag your ring prefab into this field in the Unity Editor
    private GameObject ring; // Reference to the instantiated ring
    public float pushForce = 5f;
    public float powerupCooldown = 5f;
    private bool canActivatePower = true;

    void Start()
    {
        ring = Instantiate(ringPrefab, transform.position, Quaternion.identity);
        ring.SetActive(false);
        ring.transform.parent = transform;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && canActivatePower)
        {
            ActivatePower1();
        }
    }

    void ActivatePower1()
    {
        ring.SetActive(true);
        StartCoroutine(DisableRingAfterDelay(0.5f));
        StartCoroutine(PowerupCooldown());
    }

    IEnumerator PowerupCooldown()
    {
        canActivatePower = false;
        yield return new WaitForSeconds(powerupCooldown);
        canActivatePower = true;
    }

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
}
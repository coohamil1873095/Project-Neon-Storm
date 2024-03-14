using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private float bulletUptime;
    [SerializeField] private float bulletDamage;
    [SerializeField] private float bulletSpeed;
    private float bulletCooldown;
    private Rigidbody2D rb;
    private GameObject player;

    void Start() 
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        
        Vector3 direction = player.transform.position - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * bulletSpeed;

        float rot = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 90);

        ResetBulletCooldown();
    }

    // Update is called once per frame
    void Update()
    {
        bulletCooldown -= Time.deltaTime;
        if (bulletCooldown <= 0f) {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        switch (other.gameObject.tag) 
        {
            case "Player":
                PlayerManager.Instance.DamagePlayerHealth(bulletDamage);
                Destroy(gameObject);
                break;
            case "Ring":
                Destroy(gameObject);
                break;
        }
    }

    public void ResetBulletCooldown()
    {
        bulletCooldown = bulletUptime;
    }
}

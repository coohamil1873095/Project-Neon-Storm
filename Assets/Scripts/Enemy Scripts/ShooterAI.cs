using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class ShooterAI : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform bulletPos;
    [SerializeField] private float firingRange;
    [SerializeField] private float firingCooldown;
    
    private float timer;
    private GameObject player;
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        float distance = Vector2.Distance(transform.position, player.transform.position);

        if (distance <= firingRange) 
        {
            timer += Time.deltaTime;
            if (timer > firingCooldown) 
            {
                timer = 0;
                ShootBullet();
            }
        }
    }

    private void ShootBullet()
    {
        Instantiate(bullet, bulletPos.position, Quaternion.identity, GameObject.Find("Bullets").transform);
    }
}

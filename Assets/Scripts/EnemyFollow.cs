using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public float moveSpeed = 3f;
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        if (player == null)
        {
            Debug.LogError("Player not found. Make sure the player has the tag 'Player'.");
        }
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
}

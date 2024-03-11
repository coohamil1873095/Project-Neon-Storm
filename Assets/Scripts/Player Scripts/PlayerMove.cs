using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!GameManager.Instance.GameIsPaused())
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            Vector2 movement = new Vector2(horizontalInput, verticalInput).normalized;
            rb.AddForce(movement * moveSpeed);
        }
        
    }
}
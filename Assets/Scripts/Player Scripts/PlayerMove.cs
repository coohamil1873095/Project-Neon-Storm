using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMove : MonoBehaviour
{
    [SerializeField] public float moveSpeed;
    private Rigidbody2D rb;
    private Vector2 moveInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!GameManager.Instance.GameIsPaused())
        {
            moveInput.x = Input.GetAxisRaw("Horizontal");
            moveInput.y = Input.GetAxisRaw("Vertical");

            moveInput.Normalize();

            rb.velocity = moveInput * moveSpeed;
        }

    }

    public void ApplyMoveSpeedUpgrade(float moveSpeedGain)
    {
        moveSpeed += moveSpeed * moveSpeedGain / 10;
    }
}
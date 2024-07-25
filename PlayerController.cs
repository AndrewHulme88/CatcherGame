using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private float groundCheckDistance = 1f;
    [SerializeField] private float groundCheckOffsetY = -0.5f;

    private Rigidbody2D rb;
    private float moveInput;
    private bool isGrounded;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Move();
        Jump();
        CollisionChecks();
    }

    private void Move()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveSpeed * moveInput, rb.velocity.y);
    }

    private void Jump()
    {
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    private void CollisionChecks()
    {
        Vector2 checkPosition = new Vector2(transform.position.x, transform.position.y + groundCheckOffsetY);
        isGrounded = Physics2D.OverlapCircle(checkPosition, groundCheckDistance, whatIsGround);
    }

    private void OnDrawGizmos()
    {
        Vector2 gizmosPosition = new Vector2(transform.position.x, transform.position.y + groundCheckOffsetY);
        Gizmos.DrawWireSphere(gizmosPosition, groundCheckDistance);
    }
}

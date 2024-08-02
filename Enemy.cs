using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private int facingDirection = 1;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private float groundCheckDistance = 0.5f;
    [SerializeField] private float wallCheckDistance = 0.5f;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private LayerMask whatToIgnore;
    [SerializeField] private bool isFacingRight = true;

    private Rigidbody2D rb;
    private bool wallDetected = false;
    private bool groundDetected = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (groundCheck == null)
            groundCheck = transform;
        if (wallCheck == null)
            wallCheck = transform;

        if (isFacingRight)
            Flip();
    }

    void Update()
    {
        Patrol();
        CollisionChecks();
    }

    private void Patrol()
    {
        rb.velocity = new Vector2(moveSpeed * facingDirection, rb.velocity.y);

        if(wallDetected || !groundDetected)
        {
            Flip();
        }
    }

    private void Flip()
    {
        facingDirection = facingDirection * -1;
        transform.Rotate(0, 180, 0);
    }

    private void CollisionChecks()
    {
        groundDetected = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
        wallDetected = Physics2D.Raycast(wallCheck.position, Vector2.right * facingDirection, wallCheckDistance, whatIsGround);
    }

    private void OnDrawGizmos()
    {
        if (groundCheck != null)
        {
            Gizmos.DrawLine(groundCheck.position, new Vector2(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        }

        if (wallCheck != null)
        {
            Gizmos.DrawLine(wallCheck.position, new Vector2(wallCheck.position.x + wallCheckDistance * facingDirection, wallCheck.position.y));
        }
    }
}

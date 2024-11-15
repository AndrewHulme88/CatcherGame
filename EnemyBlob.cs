using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBlob : Enemy
{
    [Header("Blob Specifics")]
    [SerializeField] private float dropSpeed = 5f;
    private enum EnemyState { Ceiling, Dropping, Ground }
    private EnemyState enemyState = EnemyState.Ceiling;
    private Vector2 moveDirection = Vector2.right;

    public bool playerFound = false;

    protected override void Start()
    {
        base.Start();
        rb.gravityScale = 0f;
    }

    void Update()
    {
        CollisionChecks();

        switch(enemyState)
        {
            case EnemyState.Ceiling:
                MoveOnCeiling();
                DetectPlayerBelow();
                break;
            case EnemyState.Dropping:
                DropDown();
                break;
            case EnemyState.Ground:
                Patrol();
                break;
        }
    }

    private void MoveOnCeiling()
    {
        rb.velocity = moveDirection * moveSpeed;

        if (wallDetected || !groundDetected)
        {
            idleTimeCounter = idleTime;
            Flip();
            moveDirection = -moveDirection;
        }
    }

    private void DetectPlayerBelow()
    {
        RaycastHit2D playerHit = Physics2D.Raycast(transform.position, Vector2.down, playerDetectDistance, playerMask);

        if(playerHit.collider != null)
        {
            playerFound = true;
            enemyState = EnemyState.Dropping;
            transform.localScale = new Vector3(1, 1, 1);
            groundCheckDistance = -groundCheckDistance;
            rb.gravityScale = 1f;
        }
    }

    private void DropDown()
    {
        rb.velocity = new Vector2(rb.velocity.x, -dropSpeed);

        if(groundDetected)
        {
            enemyState = EnemyState.Ground;
        }
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * playerDetectDistance);
    }

}

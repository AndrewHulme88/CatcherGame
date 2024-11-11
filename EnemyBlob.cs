using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBlob : Enemy
{
    [Header("Blob Specifics")]
    [SerializeField] private float dropSpeed = 5f;
    private Transform enemyTransform;
    private bool isDropping = false;
    private enum EnemyState { Ceiling, Dropping, Ground }
    private EnemyState enemyState = EnemyState.Ceiling;
    private Vector2 moveDirection = Vector2.right;

    protected override void Start()
    {
        enemyTransform = transform;
        rb.gravityScale = 0f;
    }

    void Update()
    {
        switch(enemyState)
        {
            case EnemyState.Ceiling:
                
        }
    }

    private void MoveOnCeiling()
    {
        rb.velocity = moveDirection * moveSpeed;

        if (wallDetected || !groundDetected)
        {
            idleTimeCounter = idleTime;
            Flip();
        }
    }

    private void DetectPlayerBelow()
    {

    }
}

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
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGhoul : Enemy
{
    [SerializeField] private bool isSpawnType;
    [SerializeField] private bool isAwake;
    [SerializeField] private float checkRadius = 5f;
    [SerializeField] private float wakeUpDelay = 2f;
    [SerializeField] private GameObject attackZone;
    [SerializeField] private float attackTriggerDistance = 3f;
    [SerializeField] private float spawnHitBoxDelay = 0.2f;
    [SerializeField] private float removeHitBoxDelay = 1f;


    protected override void Start()
    {
        base.Start();
    }

    private void Update()
    {
        CollisionChecks();

        playerDetected = Physics2D.OverlapCircle(transform.position, checkRadius, playerMask);
        bool attackPlayer = Physics2D.Raycast(wallCheck.position, Vector2.right * facingDirection, attackTriggerDistance, playerMask);

        if(playerDetected && !isAwake && isSpawnType)
        {
            anim.SetTrigger("spawn");
            Invoke("WakeUp", wakeUpDelay);
        }

        if(isAwake)
        {
            Patrol();
            anim.SetFloat("xVelocity", rb.velocity.x);

            if(attackPlayer && canMove)
            {
                canMove = false;
                anim.SetTrigger("attack");
                Invoke("SpawnHitBox", spawnHitBoxDelay);
            }
        }
    }

    private void WakeUp()
    {
        isAwake = true;
        anim.SetBool("isAwake", isAwake);
    }

    private void SpawnHitBox()
    {
        attackZone.SetActive(true);
        Invoke("RemoveHitBox", removeHitBoxDelay);
    }

    private void RemoveHitBox()
    {
        attackZone.SetActive(false);
        canMove = true;
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawWireSphere(transform.position, checkRadius);
    }
}

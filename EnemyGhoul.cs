using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGhoul : Enemy
{
    [SerializeField] private bool isSpawnType;
    [SerializeField] private bool isAwake;
    [SerializeField] private float checkRadius = 5f;
    [SerializeField] private float wakeUpDelay = 2f;


    protected override void Start()
    {
        base.Start();
    }

    private void Update()
    {
        CollisionChecks();

        playerDetected = Physics2D.OverlapCircle(transform.position, checkRadius, playerMask);

        if(playerDetected && !isAwake && isSpawnType)
        {
            anim.SetTrigger("spawn");
            Invoke("WakeUp", wakeUpDelay);
        }

        if(isAwake)
        {
            Patrol();
            anim.SetFloat("xVelocity", rb.velocity.x);
        }
    }

    private void WakeUp()
    {
        isAwake = true;
        anim.SetBool("isAwake", isAwake);
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawWireSphere(transform.position, checkRadius);
    }
}

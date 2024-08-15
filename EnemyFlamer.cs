using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;

public class EnemyFlamer : Enemy
{
    [SerializeField] private GameObject attackZone;
    [SerializeField] private float spawnHitBoxDelay = 0.5f;
    [SerializeField] private float removeHitBoxDelay = 1f;
    [SerializeField] private float deathAnimationDelay = 1f;
    [SerializeField] private float destroyDeathAnimDelay = 1f;
    [SerializeField] private GameObject flameFX;
    [SerializeField] private GameObject deathFXPosition;
    [SerializeField] private GameObject deathFX;

    protected override void Start()
    {
        base.Start();
    }

    void Update()
    {
        if(isDead)
        {
            Invoke("DeathAnimation", deathAnimationDelay);
        }

        CollisionChecks();

        if(!playerDetected)
        {
            Patrol();
            anim.SetFloat("xVelocity", rb.velocity.x);
            anim.SetBool("attack", false);
            flameFX.SetActive(false);
        }

        if(playerDetected)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            anim.SetBool("attack", true);
            flameFX.SetActive(true);
            Invoke("SpawnHitBox", spawnHitBoxDelay);
        }
    }

    private void DeathAnimation()
    {
        Instantiate(deathFX, deathFXPosition.transform.position, Quaternion.identity);
    }

    private void SpawnHitBox()
    {
        attackZone.SetActive(true);
        if(!playerDetected)
        {
            Invoke("RemoveHitBox", removeHitBoxDelay);
        }
    }

    private void RemoveHitBox()
    {
        attackZone.SetActive(false);
        idleTimeCounter = idleTime;
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
    }
}

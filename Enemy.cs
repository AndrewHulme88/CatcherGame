using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int enemyMaxHealth = 1;
    public int enemyCurrentHealth;
    [SerializeField] private float enemyDeathDelay = 1f;
    [SerializeField] protected float moveSpeed = 5f;
    [SerializeField] protected int facingDirection = 1;
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected Transform wallCheck;
    [SerializeField] protected float groundCheckDistance = 0.5f;
    [SerializeField] protected float wallCheckDistance = 0.5f;
    [SerializeField] protected LayerMask whatIsGround;
    [SerializeField] protected LayerMask whatToIgnore;
    [SerializeField] protected bool isFacingRight = true;
    [SerializeField] protected float playerDetectDistance = 10f;
    [SerializeField] protected LayerMask playerMask;
    [SerializeField] protected float idleTime = 2f;

    protected Rigidbody2D rb;
    protected Animator anim;
    protected bool wallDetected = false;
    protected bool groundDetected = true;
    protected bool playerDetected;
    protected GameObject player;
    protected float idleTimeCounter;
    protected bool canMove = true;
    protected DamagePlayer damagePlayer;


    protected virtual void Start()
    {
        enemyCurrentHealth = enemyMaxHealth;

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        InvokeRepeating("FindPlayer", 0, 0.5f);

        if (groundCheck == null)
            groundCheck = transform;
        if (wallCheck == null)
            wallCheck = transform;

        if (isFacingRight)
            Flip();
    }

    private void FindPlayer()
    {
        player = FindObjectOfType<PlayerController>().gameObject;
    }

    protected virtual void Patrol()
    {
        if(idleTimeCounter <= 0 && canMove)
        {
            rb.velocity = new Vector2(moveSpeed * facingDirection, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        idleTimeCounter -= Time.deltaTime;

        if(wallDetected || !groundDetected)
        {
            idleTimeCounter = idleTime;
            Flip();
        }
    }

    protected virtual void Flip()
    {
        facingDirection = facingDirection * -1;
        transform.Rotate(0, 180, 0);
    }

    public virtual void Damage()
    {
        enemyCurrentHealth--;

        if(enemyCurrentHealth > 0)
        {
            anim.SetTrigger("gotHit");
        }
        else
        {
            canMove = false;
            anim.SetTrigger("death");
            IsDead();
            Invoke("DestroyEnemy", enemyDeathDelay);
        }
    }

    private void IsDead()
    {
        damagePlayer = GetComponent<DamagePlayer>();
        damagePlayer.canDamagePlayer = false;
        rb.bodyType = RigidbodyType2D.Kinematic;
        GetComponent<Collider2D>().enabled = false;
    }

    public void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    protected virtual void CollisionChecks()
    {
        groundDetected = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
        wallDetected = Physics2D.Raycast(wallCheck.position, Vector2.right * facingDirection, wallCheckDistance, whatIsGround);
        playerDetected = Physics2D.Raycast(wallCheck.position, Vector2.right * facingDirection, playerDetectDistance, playerMask);
    }

    protected virtual void OnDrawGizmos()
    {
        if (groundCheck != null)
        {
            Gizmos.DrawLine(groundCheck.position, new Vector2(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        }

        if (wallCheck != null)
        {
            Gizmos.DrawLine(wallCheck.position, new Vector2(wallCheck.position.x + wallCheckDistance * facingDirection, wallCheck.position.y));
            Gizmos.DrawLine(wallCheck.position, new Vector2(wallCheck.position.x + playerDetectDistance * facingDirection, wallCheck.position.y));
        }
    }
}

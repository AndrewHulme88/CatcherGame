using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class PlayerController : MonoBehaviour
{
    [Header("Health")]
    public int playerMaxHealth = 3;
    public int playerCurrentHealth;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private float wallCheckDistance = 2f;
    [SerializeField] private float groundCheckDistance = 1f;
    [SerializeField] private float groundCheckOffsetY = -0.5f;
    [SerializeField] private float landingDelay = 0.5f;
    [SerializeField] private float cayoteJumpTime = 0.5f;
    private float cayoteJumpCounter;
    private bool canHaveCayoteJump;

    [Header("Shooting")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletOrigin;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private GameObject shotParticles;
    [SerializeField] private float shootKickbackTime = 0.5f;
    private bool canShoot = true;

    [Header("Knockback")]
    [SerializeField] private Vector2 knockbackDirection;
    [SerializeField] private float knockbackTime = 1f;
    [SerializeField] private float knockbackProtectionTime = 1f;

    private bool isKnocked;
    private bool canBeKnocked = true;

    [Header("Effects")]
    [SerializeField] private GameObject dustParticles;
    [SerializeField] private GameObject landingParticles;

    private Rigidbody2D rb;
    private Animator anim;
    private float moveInput;
    private bool isGrounded;
    private bool wasGrounded;
    private bool canMove = true;
    private bool facingRight = true;
    private int facingDirection = 1;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        playerCurrentHealth = playerMaxHealth;
    }

    void Update()
    {
        AnimationController();

        if(isKnocked)
        {
            return;
        }

        Move();
        JumpController();
        CollisionChecks();
        FlipController();
        Shoot();

        cayoteJumpCounter -= Time.deltaTime;

        if(isGrounded)
        {
            canHaveCayoteJump = true;
        }
        else
        {
            if (canHaveCayoteJump)
            {
                canHaveCayoteJump = false;
                cayoteJumpCounter = cayoteJumpTime;
            }
        }
    }

    private void Shoot()
    {
        if(Input.GetButtonDown("Fire1") && canShoot)
        {
            StartCoroutine(ShootKickbackDelay());
            anim.SetTrigger("shoot");
            var particleSystem = shotParticles.GetComponent<ParticleSystem>();
            Instantiate(particleSystem, bulletOrigin.transform.position, Quaternion.identity);
            GameObject newBullet = Instantiate(bulletPrefab, bulletOrigin.transform.position, bulletOrigin.transform.rotation);
            newBullet.GetComponent<Bullet>().SetupSpeed(bulletSpeed * facingDirection);
        }
    }

    private void AnimationController()
    {
        bool isMoving = Mathf.Abs(rb.velocity.x) > 0.1f;

        anim.SetBool("isKnocked", isKnocked);

        if(isKnocked)
        {
            return;
        }

        anim.SetBool("isMoving", isMoving);
        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("yVelocity", rb.velocity.y);

        var particleSystem = dustParticles.GetComponent<ParticleSystem>();

        if(isMoving && isGrounded)
        {
            if(!particleSystem.isEmitting)
            {
                particleSystem.Play();
            }
        }
        else
        {
            if(particleSystem.isEmitting)
            {
                particleSystem.Stop(withChildren: true, stopBehavior: ParticleSystemStopBehavior.StopEmitting);
            }
        }
    }

    private void Move()
    {
        if (!canMove)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            return;
        }

        moveInput = Input.GetAxisRaw("Horizontal");
        float targetVelocityX = moveSpeed * moveInput;
        Vector2 checkPosition = new Vector2(transform.position.x, transform.position.y + groundCheckOffsetY);

        RaycastHit2D hit = Physics2D.Raycast(checkPosition, Vector2.right * moveInput, wallCheckDistance, whatIsGround);
        if(hit.collider != null)
        {
            targetVelocityX = 0;
        }

        rb.velocity = new Vector2(targetVelocityX, rb.velocity.y);
    }

    private void FlipController()
    {
        if (facingRight && rb.velocity.x < -0.1f)
        {
            Flip();
        }
        else if (!facingRight && rb.velocity.x > 0.1f)
        {
            Flip();
        }
    }

    private void Flip()
    {
        facingDirection = facingDirection * -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }

    private void JumpController()
    {
        if(Input.GetButtonDown("Jump"))
        {
            if (isGrounded || cayoteJumpCounter > 0)
            {
                Jump();
            }
        }
    }

    private void Jump()
    {
        canHaveCayoteJump = false;
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    private void CollisionChecks()
    {
        Vector2 checkPosition = new Vector2(transform.position.x, transform.position.y + groundCheckOffsetY);
        wasGrounded = isGrounded;
        isGrounded = Physics2D.OverlapCircle(checkPosition, groundCheckDistance, whatIsGround);

        if(!wasGrounded && isGrounded)
        {
            TriggerLandingParticles();
            StartCoroutine(LandingDelay());
        }
    }

    private void TriggerLandingParticles()
    {
        var landingParticleSystem = landingParticles.GetComponent<ParticleSystem>();
        landingParticleSystem.Play();
    }

    private IEnumerator LandingDelay()
    {
        canMove = false;
        yield return new WaitForSeconds(landingDelay);
        canMove = true;
    }

    private IEnumerator ShootKickbackDelay()
    {
        canShoot = false;
        yield return new WaitForSeconds(shootKickbackTime);
        canShoot = true;
    }

    public void Knockback(Transform damageDirection)
    {
        if(!canBeKnocked)
        {
            return;
        }

        isKnocked = true;
        canBeKnocked = false;

        //PlayerDamage();

        int hitDirection = 0;
        if(transform.position.x > damageDirection.position.x)
        {
            hitDirection = 1;
        }
        else if(transform.position.x < damageDirection.position.x)
        {
            hitDirection = -1;
        }

        rb.velocity = new Vector2(knockbackDirection.x * hitDirection, knockbackDirection.y);

        Invoke("CancelKnockback", knockbackTime);
        Invoke("AllowKnockback", knockbackProtectionTime);
    }

    public void PlayerDamage()
    {
        playerCurrentHealth--;
        
        if(playerCurrentHealth < 1)
        {
            Destroy(gameObject);
        }
    }

    private void CancelKnockback()
    {
        isKnocked = false;
    }

    private void AllowKnockback()
    {
        canBeKnocked = true;
    }

    private void OnDrawGizmos()
    {
        Vector2 gizmosPosition = new Vector2(transform.position.x, transform.position.y + groundCheckOffsetY);
        Gizmos.DrawWireSphere(gizmosPosition, groundCheckDistance);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private GameObject hitParticles;
    [SerializeField] private Transform hitPoint;

    private float bulletSpeed;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rb.velocity = new Vector2(bulletSpeed, rb.velocity.y);
    }

    public void SetupSpeed(float speed)
    {
        bulletSpeed = speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var particleSystem = hitParticles.GetComponent<ParticleSystem>();
        Instantiate(particleSystem, hitPoint.transform.position, Quaternion.identity);

        if(collision.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    public bool canDamagePlayer = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>() != null && canDamagePlayer)
        {
            PlayerController player = collision.GetComponent<PlayerController>();

            player.Knockback(transform);
        }
    }
}

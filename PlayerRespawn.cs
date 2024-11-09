using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private Vector3 lastSafePosition;
    [SerializeField] PlayerController playerController;

    void Start()
    {
        lastSafePosition = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("SafeGround"))
        {
            lastSafePosition = collision.transform.position;
        }
        else if(collision.CompareTag("TrapSpikes"))
        {
            playerController.PlayerDamage();
            transform.position = lastSafePosition;
        }
    }
}

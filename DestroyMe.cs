using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyMe : MonoBehaviour
{
    [SerializeField] private float destroyDelay = 1f;

    private void Start()
    {
        Destroy(gameObject, destroyDelay);
    }
}

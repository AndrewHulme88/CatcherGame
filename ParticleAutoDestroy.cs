using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleAutoDestroy : MonoBehaviour
{
    private ParticleSystem particles;

    void Start()
    {
        particles = GetComponent<ParticleSystem>();
        if(particles == null )
        {
            Debug.LogError("ParticleSystem component missing!");
            Destroy(gameObject);
            return;
        }

        float maxLifetime = particles.main.duration + particles.main.startLifetime.constantMax;

        Destroy(gameObject, maxLifetime);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShielder : Enemy
{

    protected override void Start()
    {
        base.Start();
    }

    void Update()
    {
        CollisionChecks();

        Patrol();
    }
}

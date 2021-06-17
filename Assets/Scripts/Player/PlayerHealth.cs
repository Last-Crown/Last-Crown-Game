using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : LivingEntity
{
    public event Action OnDeath;

    private void Awake()
    {
        health = 100;
    }

    private void Update()
    {


    }

    public override void OnDamage(float damage)
    {
        base.OnDamage(damage);
    }

    public override void OnDie()
    {
        base.OnDie();
        OnDeath();
    }
}

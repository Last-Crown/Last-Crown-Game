using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : LivingEntity
{
    public event Action OnDeath;    // Invoke when player dead

    private void Start()
    {
        maxHealth = health = 100;
    }

    public override void OnDamage(float damage)
    {
        base.OnDamage(damage);

        UIManager.Instance.UpdateHealth(maxHealth, health);
    }

    public override void OnDie()
    {
        base.OnDie();
    }
}

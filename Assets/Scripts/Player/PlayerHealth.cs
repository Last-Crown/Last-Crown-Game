using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : LivingEntity
{
    private void Awake()
    {
        Health = 100;
    }

    public override void OnDamage(float damage)
    {
        base.OnDamage(damage);
    }
}

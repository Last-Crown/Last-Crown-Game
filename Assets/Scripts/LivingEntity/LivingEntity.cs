using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : MonoBehaviour
{
    public bool IsAlive => health > 0;

    protected float health, maxHealth;

    public virtual void OnDamage(float damage)
    {
        health -= damage;
        if (!IsAlive)
        {
            health = 0;
            OnDie();
        }
    }

    public virtual void OnDie()
    {
        Debug.Log("Player Dead!");
    }
}

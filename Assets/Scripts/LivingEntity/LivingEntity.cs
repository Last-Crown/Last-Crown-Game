using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : MonoBehaviour
{
    public bool IsAlive => health > 0;

    protected float health;

    public virtual void OnDamage(float damage)
    {
        if (IsAlive)
            health -= damage;
        else
        {
            health = 0;
            OnDie();
        }
    }

    public virtual void OnDie()
    {

    }
}

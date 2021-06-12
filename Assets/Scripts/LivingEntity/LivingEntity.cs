using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : MonoBehaviour
{
    public bool IsAlive => Health > 0;

    protected float Health;


    public virtual void OnDamage(float damage)
    {
        Health -= damage;
        Health = IsAlive ? Health : 0;
    }
}

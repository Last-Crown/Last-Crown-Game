using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class LivingEntity : MonoBehaviour
{
    public bool IsAlive => health > 0;

    public float health, maxHealth;

    public virtual void OnDamage(float damage)
    {
        JSONNode json = JSONNode.Parse("{ name: " + gameObject.name + " ,type: " + gameObject.tag + " ,health: " + health + " ,value:" + -damage + " }");

        GameObject.FindWithTag("Server").GetComponent<ServerInitializer>().EmitUpdateHealth(json.ToString());
    }

    public virtual void SetHealth(float _health)
    {
        health = _health;
        GetComponent<PlayerInfo>().Health = _health;

        if (!IsAlive)
        {
            health = 0;
            OnDie();
        }
    }

    public virtual void OnDie()
    {
        Debug.Log(gameObject.name + " Dead!");
    }
}

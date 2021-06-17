using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float attackSpeed;

    public float AttackSpeed {
        get => attackSpeed;
        set
        {
            attackSpeed = value;
            // AttackSpeed Updated
        }
    }
}

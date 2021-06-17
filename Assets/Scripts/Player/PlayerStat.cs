using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    private float _attackSpeed;
    public float AttackSpeed {
        get => _attackSpeed;
        set
        {
            _attackSpeed = value;
            // AttackSpeed Update
        }
    }
}

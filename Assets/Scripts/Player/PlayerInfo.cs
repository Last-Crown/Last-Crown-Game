using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public float attackSpeed;
    public int woodCount, stoneCount;

    private void Start()
    {
        WoodCount = 0;
        StoneCount = 0;
    }

    public int WoodCount 
    {
        get => woodCount;
        set
        {
            woodCount = value;
            UIManager.Instance.UpdateWoodCount(value);
        }
    }

    public int StoneCount
    {
        get => stoneCount;
        set
        {
            stoneCount = value;
            UIManager.Instance.UpdateStoneCount(value);
        }
    }

    public float AttackSpeed 
    {
        get => attackSpeed;
        set
        {
            attackSpeed = value;
            // AttackSpeed Updated
        }
    }
}

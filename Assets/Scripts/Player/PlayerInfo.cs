using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfo : MonoBehaviour
{
    public float attackSpeed;
    public int woodCount, stoneCount;

    private float playerMaxHealth, playerHealth;

    private void Start()
    {
        WoodCount = 0;
        StoneCount = 0;

        playerMaxHealth = Health = 100;
    }

    // Resources
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

    // PlayerStat
    public float AttackSpeed 
    {
        get => attackSpeed;
        set
        {
            // AttackSpeed Updated
            attackSpeed = value;
        }
    }

    public float Health
    {
        get => playerHealth;
        set
        {
            // Health Updated
            playerHealth = value;
            
            PlayerAction pa = GetComponent<PlayerAction>();

            Image healthIndicator = transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>();
            
            float ratio = playerHealth / playerMaxHealth;
            healthIndicator.fillAmount = ratio;
            healthIndicator.color = new Color(1, ratio, ratio);

        }
    }
}

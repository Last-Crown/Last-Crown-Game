using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfo : MonoBehaviour
{
    public float attackSpeed;
    public int woodCount, stoneCount;

    private float playerMaxHealth, playerHealth;

    private UIManager ui => _ui ??= GameObject.Find("Managers").GetComponent<UIManager>();
    private UIManager _ui;

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
            ui.UpdateWoodCount(value);
            CheckMoneyState();
        }
    }

    public int StoneCount
    {
        get => stoneCount;
        set
        {
            stoneCount = value;
            ui.UpdateStoneCount(value);
            CheckMoneyState();
        }
    }

    private void CheckMoneyState()
    {
        if (WoodCount >= 2)
            ui.MoveTowardPanel(new Vector2(250, 110), ui.woodCreatePanel);
        else if (StoneCount >= 2 && WoodCount >= 1)
            ui.MoveTowardPanel(new Vector2(250, 110), ui.stoneCreatePanel);
        else
        {
            ui.MoveTowardPanel(new Vector2(-250, 110), ui.stoneCreatePanel);
            ui.MoveTowardPanel(new Vector2(-250, 110), ui.woodCreatePanel);
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

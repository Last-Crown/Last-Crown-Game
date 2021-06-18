using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance => instance ??= FindObjectOfType<UIManager>();
    private static UIManager instance;

    public Text woodTxt;
    public Text stoneTxt;
    public Image healthIndicator;

    private GameObject player;
    private PlayerAction pa;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player"); // TODO: Server에서 플레이어 이름 찾기
        pa = player.GetComponent<PlayerAction>();

        healthIndicator = player.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>();
    }

    public void UpdateWoodCount(int count) => woodTxt.text = count.ToString();

    public void UpdateStoneCount(int count) => stoneTxt.text = count.ToString();

    public void UpdateHealth(float maxH, float curH) => healthIndicator.fillAmount = curH / maxH;

    public void ButtonActivate() => pa.ActivateEquipment();

    public void ButtonPickOrCycle() => pa.PickEquipment();  // TODO: Pick Or Cycle
}

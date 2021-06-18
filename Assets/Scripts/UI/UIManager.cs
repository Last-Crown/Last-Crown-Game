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

    public RectTransform woodPanel;

    private GameObject player;
    private PlayerAction pa;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player"); // TODO: Server에서 플레이어 이름 찾기
        pa = player.GetComponent<PlayerAction>();

        healthIndicator = player.transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<Image>();
    }

    public void UpdateWoodCount(int count) => woodTxt.text = count.ToString();

    public void UpdateStoneCount(int count) => stoneTxt.text = count.ToString();

    public void UpdateHealth(float maxH, float curH) => healthIndicator.fillAmount = curH / maxH;

    public void ButtonActivate() => pa.ActivateEquipment();

    public void ButtonPickOrCycle() => pa.PickEquipment();  // TODO: Pick Or Cycle


    /////
    public void MoveTowardPanel(Vector2 target)
    {
        StartCoroutine(MovePanel(target));
    }

    IEnumerator MovePanel(Vector2 target)
    {
        if (woodPanel.anchoredPosition.x < target.x)
        {
            while (woodPanel.anchoredPosition.x < target.x - 1)
            {
                woodPanel.anchoredPosition = Vector2.Lerp(woodPanel.anchoredPosition, target, Time.deltaTime * 5);
                yield return null;
            }
        } else
        {
            while (woodPanel.anchoredPosition.x > target.x + 1)
            {
                woodPanel.anchoredPosition = Vector2.Lerp(woodPanel.anchoredPosition, target, Time.deltaTime * 5);
                yield return null;
            }
        }
    }
}

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

    private GameObject player;
    private PlayerAction pa;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player"); // TODO: Server에서 플레이어 이름 찾기
        pa = player?.GetComponent<PlayerAction>();
    }

    private void Start()
    {
        if (!player)
        {
            player = GameObject.Find("Managers").GetComponent<GameManager>().playerObject; // TODO: Server에서 플레이어 이름 찾기
            pa = player.GetComponent<PlayerAction>();
        }
    }

    public void UpdateWoodCount(int count) => woodTxt.text = count.ToString();

    public void UpdateStoneCount(int count) => stoneTxt.text = count.ToString();

    public void ButtonActivate() => pa.ActivateEquipment();

    public void ButtonPickOrCycle() => pa.PickEquipment();  // TODO: Pick Or Cycle


    /////
    private bool isPanelMove;
    public void MoveTowardPanel(Vector2 target, RectTransform panel)
    {
        if (isPanelMove)
        {
            isPanelMove = false;
            return;
        }
        StartCoroutine(MovePanel(target, panel));
    }

    IEnumerator MovePanel(Vector2 target, RectTransform panel)
    {
        isPanelMove = true;
        if (panel.anchoredPosition.x < target.x)
        {
            while (isPanelMove && panel.anchoredPosition.x < target.x - 1)
            {
                panel.anchoredPosition = Vector2.Lerp(panel.anchoredPosition, target, Time.deltaTime * 10);
                yield return null;
            }
        }
        else
        {
            while (isPanelMove && panel.anchoredPosition.x > target.x + 1)
            {
                panel.anchoredPosition = Vector2.Lerp(panel.anchoredPosition, target, Time.deltaTime * 5);
                yield return null;
            }
        }
        isPanelMove = false;
    }
}

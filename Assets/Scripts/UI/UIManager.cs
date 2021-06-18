using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;

public class UIManager : MonoBehaviour
{
    public Text woodTxt;
    public Text stoneTxt;
    public Text tutorialTxt;
    public RectTransform stoneCreatePanel;
    public RectTransform woodCreatePanel;

    private GameObject player;
    private string playerName;
    private PlayerAction pa;
    private PlayerInfo pi;

    private void Awake()
    {
        playerName = GameObject.FindWithTag("Server").GetComponent<ServerInitializer>().playerName;
        player = GameObject.Find(playerName);
        pa = player?.GetComponent<PlayerAction>();
        pi = player?.GetComponent<PlayerInfo>();
    }

    private void Start()
    {
        if (!player)
        {
            player = GameObject.Find("Managers").GetComponent<GameManager>().playerObject; // TODO: Server에서 플레이어 이름 찾기
            pa = player.GetComponent<PlayerAction>();
            pi = player.GetComponent<PlayerInfo>();
        }
    }

    public void UpdateWoodCount(int count) => woodTxt.text = count.ToString();

    public void UpdateStoneCount(int count) => stoneTxt.text = count.ToString();

    public void ButtonActivate() => pa.ActivateEquipment();

    public void ButtonPickOrCycle() => pa.Pick_or_CycleEquipment();

    // Panel Button Click
    public void ButtonWoodAxe()
    {
        if (pi.WoodCount < 2)
            return;

        JSONNode json = JSONNode.Parse("{ type: wood, value: " + -2 + " }");

        GameObject.FindWithTag("Server").GetComponent<ServerInitializer>().socket.Emit("update inventory", json.ToString());

        var woodAxe = Instantiate(Resources.Load<GameObject>("Prefabs/Tools/WoodAxe"));
        var e = woodAxe.GetComponent<Equipment>();
        pa.ChangeEquipment(e);
        MoveTowardPanel(new Vector2(-250, 110), woodCreatePanel);
    }

    /////
    private bool isPanelMove;
    private Coroutine movePanel;
    public void MoveTowardPanel(Vector2 target, RectTransform panel)
    {
        if (movePanel != null)
        {
            StopCoroutine(movePanel);
            isPanelMove = false;
        }
        movePanel = StartCoroutine(MovePanel(target, panel));
    }

    IEnumerator MovePanel(Vector2 target, RectTransform panel)
    {
        isPanelMove = true;
        if (panel.anchoredPosition.x < target.x)
        {
            while (isPanelMove && panel.anchoredPosition.x < target.x - 5)
            {
                panel.anchoredPosition = Vector2.Lerp(panel.anchoredPosition, target, Time.deltaTime * 5);
                yield return null;
            }
        }
        else
        {
            while (isPanelMove && panel.anchoredPosition.x > target.x + 5)
            {
                panel.anchoredPosition = Vector2.Lerp(panel.anchoredPosition, target, Time.deltaTime * 5);
                yield return null;
            }
        }
        isPanelMove = false;
        movePanel = null;
    }

    public void UpdateTutorialText(string txt)
    {
        tutorialTxt.text = txt;
    }
}

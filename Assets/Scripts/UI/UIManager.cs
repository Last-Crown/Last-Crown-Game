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
        if (pi.WoodCount < 20)
            return;

        RequestServer1(20);
        InstantiateTools("Prefabs/Tools/WoodAxe");
        
        MoveTowardPanel(new Vector2(-250, 110), woodCreatePanel);
    }

    public void ButtonWoodPickAxe()
    {
        if (pi.WoodCount < 20)
            return;

        RequestServer1(20);
        InstantiateTools("Prefabs/Tools/WoodPickAxe");

        MoveTowardPanel(new Vector2(-250, 110), woodCreatePanel);
    }

    public void ButtonWoodSword()
    {
        if (pi.WoodCount < 20)
            return;

        RequestServer1(20);
        InstantiateTools("Prefabs/Tools/WoodSword");

        MoveTowardPanel(new Vector2(-250, 110), woodCreatePanel);
    }

    public void ButtonStoneAxe()
    {
        if (pi.WoodCount < 10 || pi.StoneCount < 20)
            return;

        RequestServer2(10, 20);
        InstantiateTools("Prefabs/Tools/Axe");

        MoveTowardPanel(new Vector2(-250, 110), stoneCreatePanel);
    }

    public void ButtonStonePickAxe()
    {
        if (pi.WoodCount < 10 || pi.StoneCount < 20)
            return;

        RequestServer2(10, 20);
        InstantiateTools("Prefabs/Tools/PickAxe");

        MoveTowardPanel(new Vector2(-250, 110), stoneCreatePanel);
    }

    public void ButtonStoneSword()
    {
        if (pi.WoodCount < 10 || pi.StoneCount < 20)
            return;

        RequestServer2(10, 20);
        InstantiateTools("Prefabs/Tools/Sword");

        MoveTowardPanel(new Vector2(-250, 110), stoneCreatePanel);
    }

    private void RequestServer1(int n)
    {
        JSONNode json = JSONNode.Parse("{ type: wood, value: " + -n + " }");
        GameObject.FindWithTag("Server").GetComponent<ServerInitializer>().socket.Emit("update inventory", json.ToString());
    }

    private void RequestServer2(int n1, int n2)
    {
        RequestServer1(n1);

        JSONNode json = JSONNode.Parse("{ type: stone, value: " + -n2 + " }");
        GameObject.FindWithTag("Server").GetComponent<ServerInitializer>().socket.Emit("update inventory", json.ToString());
    }

    private void InstantiateTools(string path)
    {
        var woodAxe = Instantiate(Resources.Load<GameObject>(path));
        var e = woodAxe.GetComponent<Equipment>();
        pa.ChangeEquipment(e);
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

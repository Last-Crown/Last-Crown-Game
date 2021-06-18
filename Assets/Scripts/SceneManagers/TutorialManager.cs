using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    public GameObject maincamera;
    public GameObject playerObject;
    private PlayerInfo playerInfo;
    private Animator playerAnim;

    public GameObject levelLoader;

    public string playerName;

    private UIManager ui => _ui ??= GameObject.Find("Managers").GetComponent<UIManager>();
    private UIManager _ui;

    private void Awake()
    {
        playerName = "�÷��̾�1";
        maincamera = GameObject.FindWithTag("MainCamera");
        playerObject = GameObject.FindWithTag("Player");
        playerInfo = playerObject.GetComponent<PlayerInfo>();
        playerAnim = playerObject.GetComponent<Animator>();
    }

    void Start()
    {
        playerObject.transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<Text>().text = playerName;
        playerObject.name = playerName;
    }

    private void LateUpdate()
    {
        maincamera.transform.position = Vector3.Lerp(maincamera.transform.position, playerObject.transform.position + new Vector3(0, 0, -10), Time.deltaTime * 4.5f);
    }

    // Tutorial Scenario
    public string[] sentences;
    public Transform tree;
    public Transform rock;

    [Header("UI")]
    public Button activeButton;
    public Button pickCycleButton;

    private GameObject pickAxe;

    private int step = 0;
    private bool step0_btn_click;

    public void OnClickActiveBtn()
    {
        switch (step)
        {
            case 0:
                step0_btn_click = true;
                playerAnim.SetTrigger("useHand");
                break;
            case 2:
                pickAxe.GetComponent<Equipment>().Use(playerAnim);

                if (!IsFront(rock)) /////////// Remove (!)
                {
                    playerInfo.StoneCount += 20;
                }

                if (playerInfo.StoneCount >= 100)
                {
                    Debug.Log(playerInfo.StoneCount);
                    ui.UpdateTutorialText("����â���� Į�� ������ּ���.");
                    ui.MoveTowardPanel(new Vector2(250, 110), ui.stoneCreatePanel);
                    step++;
                }
                break;
        }
    }

    public void OnClickCreatePickAxe()
    {
        // ����â���� ��� Ŭ��
        playerInfo.WoodCount -= 130;
        ui.MoveTowardPanel(new Vector2(-250, 110), ui.woodCreatePanel);

        pickAxe = Instantiate(Resources.Load<GameObject>("Prefabs/Tools/WoodPickAxe"));
        var e = pickAxe.GetComponent<Equipment>();
        playerObject.GetComponent<PlayerAction>().ChangeEquipment(e);
        step++;

        ui.UpdateTutorialText("��̷� ���� �ļ� �� �ڿ��� ��������.");
    }

    public void OnClickCreateSword()
    {
        // ����â���� Į Ŭ��
        playerInfo.WoodCount -= 70;
        playerInfo.StoneCount -= 30;
        ui.MoveTowardPanel(new Vector2(-250, 110), ui.stoneCreatePanel);

        var sword = Instantiate(Resources.Load<GameObject>("Prefabs/Tools/Sword"));
        var e = sword.GetComponent<Equipment>();

        playerObject.GetComponent<PlayerAction>().ChangeEquipment(e);

        //Destroy(playerObject.transform.GetChild(0).GetChild(1).GetChild(0).GetChild(0).gameObject);    // ��� ����

        //e.Equip(playerObject.transform, playerObject.transform.GetChild(0).GetChild(1).GetChild(1));    // right hand
        step++;

        ui.UpdateTutorialText("���忡�� ���ô�!");
        levelLoader.GetComponent<LevelLoader>().LoadScene("ConnectScene");
    }

    private void Update()
    {
        if (step == 0)
            Step0();
        if (Input.GetKeyDown(KeyCode.R))
            playerInfo.Health -= 10;
    }

    private bool IsFront(Transform tr)
    {
        float playerX = playerObject.transform.position.x;
        float playerY = playerObject.transform.position.y;

        return (playerX > tr.position.x - 0.5f && playerX < tr.position.x + 0.5f &&
                playerY > tr.position.y - 1.5f && playerY < tr.position.y + 0.5f);
    }

    private void Step0()
    {

        if (!IsFront(tree))
        {
            ui.UpdateTutorialText("���� ������ ���ּ���.");
            activeButton.enabled = false;
            step0_btn_click = false;
        } else
        {
            ui.UpdateTutorialText("��Ƽ�� ��ư�� ���� ������ �ı��ϰ� �ڿ��� ��������.");
            activeButton.enabled = true;
            if (step0_btn_click)
            {
                tree.GetComponent<TreeOnGround>().UpdateHitLimit(0);
                playerInfo.WoodCount += 200;
                ui.UpdateTutorialText("���� �ڿ� 200���� ȹ���߽��ϴ�.\n���� ����â���� ���� ��� Ŭ�� �� ������ּ���.");
                ui.MoveTowardPanel(new Vector2(250, 110), ui.woodCreatePanel);
                step++;
            }
        }
    }
}

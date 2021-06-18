using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager Instance => instance ??= FindObjectOfType<TutorialManager>();
    private static TutorialManager instance;

    public GameObject maincamera;
    public GameObject playerObject;
    private PlayerInfo playerInfo;
    private Animator playerAnim;

    public string playerName;

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
        playerObject.AddComponent<PlayerMovement>();
        playerObject.AddComponent<PlayerAction>();
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
    public RectTransform woodCreatePanel;
    public RectTransform createPanel;

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
                
                if (!IsFront(rock))
                    playerInfo.StoneCount += 20;
                if (playerInfo.StoneCount >= 100)
                {
                    Debug.Log("����â���� Į�� ������ּ���.");
                    UIManager.Instance.MoveTowardPanel(new Vector2(250, 110), createPanel);
                    step++;
                }
                break;
        }
    }

    public void OnClickCreatePickAxe()
    {
        // ����â���� ��� Ŭ��
        playerInfo.WoodCount -= 130;
        UIManager.Instance.MoveTowardPanel(new Vector2(-250, 110), woodCreatePanel);

        pickAxe = Instantiate(Resources.Load<GameObject>("Prefabs/Tools/PickAxe"));
        var e = pickAxe.GetComponent<Equipment>();
        playerObject.GetComponent<PlayerAction>().ChangeEquipment(e);
        step++;

        Debug.Log("��̷� ���� �ļ� �� �ڿ��� ��������.");
    }

    public void OnClickCreateSword()
    {
        // ����â���� Į Ŭ��
        playerInfo.WoodCount -= 70;
        playerInfo.StoneCount -= 30;
        UIManager.Instance.MoveTowardPanel(new Vector2(-250, 110), createPanel);

        var sword = Instantiate(Resources.Load<GameObject>("Prefabs/Tools/ShortSword"));
        var e = sword.GetComponent<Equipment>();

        Destroy(playerObject.transform.GetChild(0).GetChild(1).GetChild(0).GetChild(0).gameObject);    // ��� ����

        e.Equip(playerObject.transform, playerObject.transform.GetChild(0).GetChild(1).GetChild(1));    // right hand
        step++;

        Debug.Log("���忡�� ���ô�!");
    }

    private void Update()
    {
        switch (step)
        {
            case 0: Step0(); break;
            case 2: Step2(); break;
            case 3: Step3(); break;
        }
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
            Debug.Log("���� ������ ���ּ���.");
            activeButton.enabled = false;
            step0_btn_click = false;
        } else
        {
            Debug.Log("��Ƽ�� ��ư�� ���� ������ �ı��ϰ� �ڿ��� ��������.");
            activeButton.enabled = true;
            if (step0_btn_click)
            {
                tree.GetComponent<TreeOnGround>().UpdateHitLimit(0);
                playerInfo.WoodCount += 200;
                Debug.Log("���� �ڿ� 200���� ȹ���߽��ϴ�.\n���� ����â���� ���� ��̸� ������ּ���.");
                UIManager.Instance.MoveTowardPanel(new Vector2(250, 110), woodCreatePanel);
                step++;
            }
        }
    }

    private void Step2()
    {

    }

    private void Step3()
    {

    }
}

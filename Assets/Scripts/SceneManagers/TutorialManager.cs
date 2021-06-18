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
        playerName = "플레이어1";
        maincamera = GameObject.FindWithTag("MainCamera");
        playerObject = GameObject.FindWithTag("Player");
        playerInfo = playerObject.GetComponent<PlayerInfo>();
        playerAnim = playerObject.GetComponent<Animator>();
    }

    void Start()
    {
        playerObject.AddComponent<PlayerMovement>();
        playerObject.AddComponent<PlayerAction>();
        playerObject.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().text = playerName;
        playerObject.name = playerName;
    }

    private void LateUpdate()
    {
        maincamera.transform.position = Vector3.Lerp(maincamera.transform.position, playerObject.transform.position + new Vector3(0, 0, -10), Time.deltaTime * 4.5f);
    }

    // Tutorial Scenario
    public string[] sentences;
    public Button activeButton;
    public Button pickCycleButton;
    public Transform tree;

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
        }
    }

    public void OnClickPickAxe()
    {
        playerInfo.WoodCount -= 130;
        UIManager.Instance.MoveTowardPanel(new Vector2(-210, 110));

        var pickAxe = Instantiate(Resources.Load<GameObject>("Prefabs/Tools/PickAxe"));
        var e = pickAxe.GetComponent<Equipment>();
        playerObject.GetComponent<PlayerAction>().ChangeEquipment(e);
        step++;
    }

    private void Update()
    {
        switch (step)
        {
            case 0: Step0(); break;
            case 2: Step2(); break;
            case 3: Step3(); break;
        }
    }

    private void Step0()
    {
        float playerX = playerObject.transform.position.x;
        float playerY = playerObject.transform.position.y;

        if (playerX < tree.position.x - 0.5f || playerX > tree.position.x + 0.5f ||
            playerY < tree.position.y - 1.5f || playerY > tree.position.y - 0.5f)
        {
            Debug.Log("나무 앞으로 가주세요.");
            activeButton.enabled = false;
            step0_btn_click = false;
        } else
        {
            Debug.Log("액티브 버튼을 눌러 나무를 파괴하고 자원을 얻으세요.");
            activeButton.enabled = true;
            if (step0_btn_click)
            {
                tree.GetComponent<TreeOnGround>().UpdateHitLimit(0);
                playerInfo.WoodCount += 200;
                Debug.Log("나무 자원 200개를 획득했습니다.\n왼쪽 구매창에서 나무 곡괭이를 구매해주세요");
                UIManager.Instance.MoveTowardPanel(new Vector2(210, 110));
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

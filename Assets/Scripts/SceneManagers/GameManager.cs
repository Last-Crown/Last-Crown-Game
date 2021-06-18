using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance => instance ??= FindObjectOfType<GameManager>();    // instance가 null이면 대입
    private static GameManager instance;

    public GameObject maincamera = null;

    public Text frameText = null;

    public ServerInitializer serverScript = null;
    public string playerName = "";
    public GameObject playerObject = null;

    private void Awake()
    {
        maincamera = GameObject.FindWithTag("MainCamera");

        serverScript = GameObject.FindWithTag("Server").GetComponent<ServerInitializer>();
        playerName = serverScript.playerName;
    }

    private void Start()
    {
        JSONNode json = serverScript.playerData["data"];

        playerObject = Instantiate(Resources.Load<GameObject>("Prefabs/Player/Player"));
        playerObject.AddComponent<PlayerInput>();
        playerObject.transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<Text>().text = playerName;
        playerObject.name = playerName;
        playerObject.transform.position = new Vector3(json["pos"]["x"], json["pos"]["y"], 0);

        serverScript.playerObjectList.Add(playerObject);

        StartCoroutine(UpdateFrameText());
    }

    private void Update()
    {
        maincamera.transform.position = Vector3.Lerp(maincamera.transform.position, playerObject.transform.position + new Vector3(0, 0, -10), Time.deltaTime * 3);
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    IEnumerator UpdateFrameText()
    {
        while(true)
        {
            frameText.text = serverScript.customEventQueue.Count.ToString();

            yield return null;
        }
    }
}

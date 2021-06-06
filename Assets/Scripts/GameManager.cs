using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject maincamera = null;

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
        playerObject = Instantiate(Resources.Load<GameObject>("Prefabs/Player"));
        playerObject.AddComponent<PlayerMovement>();
        playerObject.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = playerName;
        playerObject.name = playerName;
    }

    private void Update()
    {
        maincamera.transform.position = Vector3.Lerp(maincamera.transform.position, playerObject.transform.position + new Vector3(0, 0, -10), 0.9f);
    }
}
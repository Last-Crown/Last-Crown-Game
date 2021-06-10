using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public GameObject maincamera;

    public GameObject playerObject;
    public string playerName;

    private void Awake()
    {
        playerName = "플레이어1";
        maincamera = GameObject.FindWithTag("MainCamera");
        playerObject = GameObject.FindWithTag("Player");
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
}

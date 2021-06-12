using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Quobject.SocketIoClientDotNet.Client;
using SimpleJSON;


public class ServerInitializer : MonoBehaviour
{
    private const float version = 1.4f;
    public static ServerInitializer instance = null;

    public Socket socket;

    public string playerName = "";

    private JSONNode json = null;
    private Stack<KeyValuePair<string, JSONNode>> customEventStack = new Stack<KeyValuePair<string, JSONNode>>();
    private List<GameObject> playerObjectList = new List<GameObject>();

    private bool joinedGame = false;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);

        socket = IO.Socket("http://192.168.0.4:15555");

        socket.On(Socket.EVENT_CONNECT, () =>
        {
            customEventStack.Push(new KeyValuePair<string, JSONNode>("connected", null));
        });

        socket.On("get ver", (data) =>
        {
            JSONNode parsedData = JSON.Parse(data.ToString());

            customEventStack.Push(new KeyValuePair<string, JSONNode>("get ver", parsedData));
        });

        socket.On(Socket.EVENT_DISCONNECT, () =>
        {
            customEventStack.Push(new KeyValuePair<string, JSONNode>("disconnected", null));
        });

        socket.On("set name", (data) =>
        {
            JSONNode parsedData = JSON.Parse(data.ToString());

            customEventStack.Push(new KeyValuePair<string, JSONNode>("set name", parsedData));
        });

        socket.On("set seed", (data) =>
        {
            JSONNode parsedData = JSON.Parse(data.ToString());

            customEventStack.Push(new KeyValuePair<string, JSONNode>("set seed", parsedData));
        });

        socket.On("delete name", (data) =>
        {
            JSONNode parsedData = JSON.Parse(data.ToString());

            customEventStack.Push(new KeyValuePair<string, JSONNode>("delete name", parsedData));
        });

        socket.On("update data", (data) =>
        {
            JSONNode parsedData = JSON.Parse(data.ToString());

            customEventStack.Push(new KeyValuePair<string, JSONNode>("update data", parsedData));
        });

        StartCoroutine(EventStackCheckRoot());
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
        if (socket != null)
        {
            socket.Disconnect();
            Debug.Log("Disconnect");
        }
    }

    private void Update()
    {
        if (joinedGame)
        {
            foreach (var value in json.Values)
            {
                if (value["name"] != playerName)
                {
                    GameObject currentPlayerObject = playerObjectList.Find(obj =>
                    {
                        return obj.name == value["name"];
                    });

                    if (currentPlayerObject == null)
                    {
                        currentPlayerObject = Instantiate(Resources.Load<GameObject>("Prefabs/Player/Player"));
                        currentPlayerObject.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().text = value["name"];
                        currentPlayerObject.name = value["name"];

                        playerObjectList.Add(currentPlayerObject);
                    }

                    Vector3 targetPosition = new Vector3(value["pos"]["x"], value["pos"]["y"], 0);
                    Vector3 nowPosotion = currentPlayerObject.transform.position;

                    currentPlayerObject.transform.position = Vector3.Lerp(nowPosotion, targetPosition, 0.5f);
                    currentPlayerObject.transform.GetChild(0).rotation = Quaternion.Euler(0, 0, value["rot"]);
                }
            }
        }
    }

    public void EmitName(string name)
    {
        socket.Emit("set name", name);

        playerName = name;
    }

    IEnumerator EventStackCheckRoot()
    {
        while (true)
        {
            if (customEventStack.Count > 0)
            {
                KeyValuePair<string, JSONNode> currentEvent = customEventStack.Pop();

                switch (currentEvent.Key)
                {
                    case "connected":
                        Debug.Log("Connected!");
                        socket.Emit("get ver", version);

                        break;
                    case "get ver":
                        if (currentEvent.Value["state"] == "ER")
                        {
                            Debug.LogError(currentEvent.Value["data"]["message"]);

                            Destroy(gameObject);
                        }
                        else if (currentEvent.Value["state"] == "OK")
                        {
                            SceneManager.LoadScene("MainScene");
                        }

                        break;
                    case "disconnected":
                        Debug.Log("Disconnect");
                        SceneManager.LoadScene("ConnectScene");
                        foreach (var value in json.Values)
                        {
                            Destroy(GameObject.Find(value["name"]));
                        }

                        name = "";
                        joinedGame = false;

                        break;
                    case "set name":
                        if (currentEvent.Value["state"] == "ER")
                        {
                            Debug.LogError(currentEvent.Value["data"]["message"]);

                            break;
                        }
                        else if (currentEvent.Value["state"] == "OK")
                        {
                            socket.Emit("set pos", JsonUtility.ToJson(Vector2.zero));

                            SceneManager.LoadScene("GameScene");
                            joinedGame = true;

                            socket.Emit("get seed");
                        }

                        break;
                    case "set seed":
                        MapGenerator mapGenerator = GameObject.Find("Managers").GetComponent<MapGenerator>();

                        mapGenerator.UpdateSeed(currentEvent.Value);
                        mapGenerator.GenerateMap();

                        break;
                    case "delete name":
                        GameObject currentPlayerObject = playerObjectList.Find(obj =>
                        {
                            return obj.name == currentEvent.Value["name"];
                        });

                        playerObjectList.Remove(currentPlayerObject);

                        Destroy(GameObject.Find(currentEvent.Value["name"]));

                        break;
                    case "update data":
                        json = currentEvent.Value;

                        break;
                }
            }

            yield return null;
        }
    }
}

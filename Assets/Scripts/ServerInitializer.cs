using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Quobject.SocketIoClientDotNet.Client;
using SimpleJSON;


public class ServerInitializer : MonoBehaviour
{
    private const float version = 1.5f;
    private static ServerInitializer instance = null;

    private Slider loadbar;

    public Socket socket;

    public string playerName = "";

    private JSONNode json = JSONNode.Parse("");
    public Queue<KeyValuePair<string, JSONNode>> customEventQueue = new Queue<KeyValuePair<string, JSONNode>>();
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
        loadbar = GameObject.FindWithTag("LoadBar").GetComponent<Slider>();

        socket = IO.Socket("http://server.hyunwoo.kim:15555");

        socket.On(Socket.EVENT_CONNECT, () =>
        {
            customEventQueue.Enqueue(new KeyValuePair<string, JSONNode>("connected", null));
        });

        socket.On("get ver", (data) =>
        {
            JSONNode parsedData = JSON.Parse(data.ToString());

            customEventQueue.Enqueue(new KeyValuePair<string, JSONNode>("get ver", parsedData));
        });

        socket.On(Socket.EVENT_DISCONNECT, () =>
        {
            customEventQueue.Enqueue(new KeyValuePair<string, JSONNode>("disconnected", null));
        });

        socket.On("set name", (data) =>
        {
            JSONNode parsedData = JSON.Parse(data.ToString());

            customEventQueue.Enqueue(new KeyValuePair<string, JSONNode>("set name", parsedData));
        });

        socket.On("set seed", (data) =>
        {
            JSONNode parsedData = JSON.Parse(data.ToString());

            customEventQueue.Enqueue(new KeyValuePair<string, JSONNode>("set seed", parsedData));
        });

        socket.On("delete name", (data) =>
        {
            JSONNode parsedData = JSON.Parse(data.ToString());

            customEventQueue.Enqueue(new KeyValuePair<string, JSONNode>("delete name", parsedData));
        });

        socket.On("update data", (data) =>
        {
            JSONNode parsedData = JSON.Parse(data.ToString());

            if (customEventQueue.Count == 0)
            {
                customEventQueue.Enqueue(new KeyValuePair<string, JSONNode>("update data", parsedData));
            }
        });
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

        if (customEventQueue.Count > 0)
        {
            KeyValuePair<string, JSONNode> currentEvent = customEventQueue.Dequeue();
            Debug.Log("EventStackCount: " + customEventQueue.Count + " " + currentEvent.Key);

            switch (currentEvent.Key)
            {
                case "connected":
                    Debug.Log("Connected!");
                    loadbar.value += 0.5f;
                    loadbar.transform.Find("Text").GetComponent<Text>().text = "Collecting version..";
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
                        loadbar.value += 0.5f;
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
                        SceneManager.LoadScene("GameScene");
                        joinedGame = true;

                        socket.Emit("get seed");
                    }

                    break;
                case "set seed":
                    MapGenerator mapGenerator = GameObject.Find("Managers").GetComponent<MapGenerator>();

                    mapGenerator.UpdateSeed(currentEvent.Value);
                    StartCoroutine(mapGenerator.GenerateMap());
                    socket.Emit("get data");

                    break;
                case "delete name":
                    GameObject currentPlayerObject = GameObject.Find(currentEvent.Value["name"]);
                    
                    playerObjectList.Remove(currentPlayerObject);
                    Destroy(currentPlayerObject);

                    foreach(var value in json)
                    {
                        if(value.Value["name"] == currentEvent.Value["name"])
                        {
                            json.Remove(value);
                            break;
                        }
                    }

                    break;
                case "update data":
                    json = currentEvent.Value;
                    socket.Emit("get data");

                    break;
            }
        }
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

    public void EmitName(string name)
    {
        socket.Emit("set name", name);

        playerName = name;
    }
}
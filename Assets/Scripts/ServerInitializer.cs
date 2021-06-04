using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Quobject.SocketIoClientDotNet.Client;
using SimpleJSON;


public class ServerInitializer : MonoBehaviour
{
    public static ServerInitializer instance = null;

    public Socket socket;

    private JSONNode json = null;
    private Stack<KeyValuePair<string, JSONNode>> customEventStack = new Stack<KeyValuePair<string, JSONNode>>();

    private bool joinedGame = false;
    private string playerName = "";

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);


        socket = IO.Socket("http://localhost:7000");

        socket.On(Socket.EVENT_CONNECT, () =>
        {
            Debug.Log("Connected!");

            customEventStack.Push(new KeyValuePair<string, JSONNode>("connected", null));
        });

        socket.On(Socket.EVENT_DISCONNECT, () =>
        {
            Debug.Log("Disconnect");

            customEventStack.Push(new KeyValuePair<string, JSONNode>("disconnected", null));
        });

        socket.On("set name", (data) =>
        {
            JSONNode parsedData = JSON.Parse(data.ToString());

            customEventStack.Push(new KeyValuePair<string, JSONNode>("set name", parsedData));
        });

        socket.On("delete name", (data) =>
        {
            JSONNode parsedData = JSON.Parse(data.ToString());

            customEventStack.Push(new KeyValuePair<string, JSONNode>("delete name", parsedData));
        });

        socket.On("update data", (data) =>
        {
            json = JSON.Parse(data.ToString());
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
                GameObject currentPlayerObject = GameObject.Find(value["name"]);

                if (currentPlayerObject == null)
                {
                    currentPlayerObject = Instantiate(Resources.Load<GameObject>("Prefabs/Player"));
                    currentPlayerObject.name = value["name"];
                    if (playerName == value["name"])
                    {
                        currentPlayerObject.AddComponent<PlayerMovement>();
                    }
                }

                currentPlayerObject.transform.position = new Vector3(value["pos"]["x"], value["pos"]["y"], 0);
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
                        SceneManager.LoadScene("MainScene");

                        break;
                    case "disconnected":
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

                        }

                        break;
                    case "delete name":
                        Destroy(GameObject.Find(currentEvent.Value["name"]));

                        break;
                }
            }

            yield return null;
        }
    }
}

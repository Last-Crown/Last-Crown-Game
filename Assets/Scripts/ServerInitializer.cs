using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Quobject.SocketIoClientDotNet.Client;
using SimpleJSON;


public class ServerInitializer : MonoBehaviour
{
    public Socket socket;

    private JSONNode json;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        socket = IO.Socket("http://localhost:7000");

        socket.On(Socket.EVENT_CONNECT, () =>
        {
            Debug.Log("Connected!");
            
        });

        socket.On("update data", (data) =>
        {
            json = JSON.Parse(data.ToString());
        });
    }

    private void OnDestroy()
    {
        socket.Disconnect();
    }

    private void FixedUpdate()
    {
        foreach (var value in json.Values)
        {
            GameObject.Find(value["name"]).transform.position = new Vector2(value["pos"]["x"], value["pos"]["y"]);
        }
    }
}

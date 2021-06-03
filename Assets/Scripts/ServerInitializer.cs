using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Quobject.SocketIoClientDotNet.Client;


public class ServerInitializer : MonoBehaviour
{
    public Socket socket;

    private void Awake()
    {
        socket = IO.Socket("http://localhost:7000");

        socket.On(Socket.EVENT_CONNECT, () =>
        {
            Debug.Log("Connected!");
            socket.Emit("set name", "TestName");
        });
    }

    private void OnDestroy()
    {
        socket.Disconnect();
    }
}

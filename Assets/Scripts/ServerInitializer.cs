using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Quobject.SocketIoClientDotNet.Client;


public class ServerInitializer : MonoBehaviour
{
    private Socket socket;

    void Awake()
    {
        socket = IO.Socket("http://localhost:7000");


        socket.On(Socket.EVENT_CONNECT, () =>
        {
            Debug.Log("Connected!");
        });

        socket.On("shakeHand", () =>
        { 
            Debug.Log("Shaking");
        });

        socket.Emit("shakeHand" );
    }
}

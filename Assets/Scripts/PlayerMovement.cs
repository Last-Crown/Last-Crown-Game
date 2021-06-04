using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Quobject.SocketIoClientDotNet.Client;

public class PlayerMovement : MonoBehaviour
{
    private Socket socket;

    void Update()
    {
        if(socket == null)
        {
            socket = GameObject.Find("Server").GetComponent<ServerInitializer>().socket;
        }

        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(new Vector3(0, 0.05f, 0));
            ServerSetPos();
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(new Vector3(-0.05f , 0, 0));
            ServerSetPos();
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(new Vector3(0, -0.05f , 0));
            ServerSetPos();
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(new Vector3(0.05f, 0, 0));
            ServerSetPos();
        }

    }

    private void ServerSetPos()
    {
        string data = JsonUtility.ToJson((Vector2)transform.position);

        socket.Emit("set pos", data);
    }
}

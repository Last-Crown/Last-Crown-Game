using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Quobject.SocketIoClientDotNet.Client;

public class PlayerMovement : MonoBehaviour
{
    private Socket socket;

    private void Awake()
    {
        socket = GameObject.FindWithTag("Server").GetComponent<ServerInitializer>().socket;
    }

    void Update()
    {

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


        socket.Emit("update rot", transform.localEulerAngles.z);
    }

    private void ServerSetPos()
    {
        string data = JsonUtility.ToJson((Vector2)transform.position);

        socket.Emit("update pos", data);
    }
}

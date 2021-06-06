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

        Vector2 lookat = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)transform.position;

        transform.GetChild(0).rotation = Quaternion.Euler(0, 0, Mathf.Atan2(lookat.y, lookat.x) * 180 / Mathf.PI -90);
        
        socket.Emit("update rot", transform.GetChild(0).localEulerAngles.z);
    }

    private void ServerSetPos()
    {
        string data = JsonUtility.ToJson((Vector2)transform.position);

        socket.Emit("update pos", data);
    }
}

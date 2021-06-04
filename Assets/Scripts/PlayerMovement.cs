using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Quobject.SocketIoClientDotNet.Client;

public class PlayerMovement : MonoBehaviour
{
    private JoyStick joyStick;
    private float speed;

    void Start()
    {
        speed = 15f;
        // TODO: search joyStick
    }


    void Update()
    {
        if (Input.anyKey) move();
        private Socket socket;

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
            transform.Translate(new Vector3(-0.05f, 0, 0));
            ServerSetPos();
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(new Vector3(-0.05f , 0, 0));
            ServerSetPos();
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(new Vector3(0.05f, 0, 0));
            ServerSetPos();
        }
    }

    private void move()
    {
        float dx = Input.GetAxis("Horizontal");
        float dy = Input.GetAxis("Vertical");

        transform.Translate(new Vector2(dx, dy) * speed * Time.deltaTime);
    }

    private void ServerSetPos()
    {
        string data = JsonUtility.ToJson((Vector2)transform.position);

        socket.Emit("set pos", data);
    }
}

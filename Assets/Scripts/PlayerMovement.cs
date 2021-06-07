using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Quobject.SocketIoClientDotNet.Client;

public class PlayerMovement : MonoBehaviour
{
    private JoyStick joyStick;
    private float speed;

    private Socket socket;

    private void Awake()
    {
        speed = 10f;

        joyStick = GameObject.Find("JoyStick").GetComponent<JoyStick>();
    }


    private void Update()
    {
        if(socket == null)
        {
            socket = GameObject.Find("Server").GetComponent<ServerInitializer>().socket;
        }

        if (Input.anyKey) KeyboardMove();
        if (joyStick.Dir != Vector2.zero) JoystickMove();
    }

    private void KeyboardMove()
    {
        float dx = Input.GetAxis("Horizontal");
        float dy = Input.GetAxis("Vertical");

        transform.Translate(new Vector2(dx, dy) * speed * Time.deltaTime);

        ServerSetPos();
    }

    private void JoystickMove()
    {
        transform.Translate(joyStick.Dir * speed * Time.deltaTime);

        ServerSetPos();
    }

    private void ServerSetPos()
    {
        string data = JsonUtility.ToJson((Vector2)transform.position);

        socket.Emit("set pos", data);
    }
}

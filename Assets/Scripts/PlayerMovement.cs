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

        transform.Translate(speed * Time.deltaTime * new Vector2(dx, dy));

        transform.GetChild(0).rotation = Quaternion.Euler(0, 0, Mathf.Atan2(dy, dx) * 180 / Mathf.PI - 90);

        ServerUpdatePos();
        ServerUpdateRot();
    }

    private void JoystickMove()
    {
        transform.Translate(speed * Time.deltaTime * joyStick.Dir);

        transform.GetChild(0).rotation = Quaternion.Euler(0, 0, Mathf.Atan2(joyStick.Dir.y, joyStick.Dir.x) * 180 / Mathf.PI - 90);

        ServerUpdatePos();
        ServerUpdateRot();
    }

    private void ServerUpdatePos()
    {
        string data = JsonUtility.ToJson((Vector2)transform.position);

        socket.Emit("update pos", data);
    }

    private void ServerUpdateRot()
    {
        socket.Emit("update rot", transform.GetChild(0).localEulerAngles.z);
    }
}

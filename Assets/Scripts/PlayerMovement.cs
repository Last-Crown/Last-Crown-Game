using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Quobject.SocketIoClientDotNet.Client;

public class PlayerMovement : MonoBehaviour
{
    private JoyStick joyStick;
    private Socket socket;
    private Animator PlayerAnim;

    private float speed;
    private bool serverExists;

    private void Awake()
    {
        speed = 4.5f;
        serverExists = true;

        joyStick = GameObject.Find("JoyStick").GetComponent<JoyStick>();
        PlayerAnim = GetComponent<Animator>();
    }


    private void Update()
    {
        if (serverExists && socket == null)
        {
            GameObject server = GameObject.FindWithTag("Server") ?? null;
            if (server == null)
            {
                Debug.Log("Tutorial Mode");
                serverExists = false;
            }
            else
                socket = server.GetComponent<ServerInitializer>().socket;
        }

        if (Input.anyKey) KeyboardMove();
        if (joyStick.Dir != Vector2.zero) JoystickMove();
        
        if (!Input.anyKey && joyStick.Dir == Vector2.zero) PlayerAnim.SetBool("Walk", false);
    }

    private void KeyboardMove()
    {
        float dx = Input.GetAxis("Horizontal");
        float dy = Input.GetAxis("Vertical");

        if (!(dx == 0 && dy == 0))
        {
            transform.Translate(speed * Time.deltaTime * new Vector2(dx, dy));
            transform.GetChild(0).rotation = Quaternion.Euler(0, 0, Mathf.Atan2(dy, dx) * 180 / Mathf.PI - 90);
            PlayerAnim.SetBool("Walk", true);
        }
            

        if (serverExists)
        {
            ServerUpdatePos();
            ServerUpdateRot();
        }
    }

    private void JoystickMove()
    {
        transform.Translate(speed * Time.deltaTime * joyStick.Dir);

        transform.GetChild(0).rotation = Quaternion.Euler(0, 0, Mathf.Atan2(joyStick.Dir.y, joyStick.Dir.x) * 180 / Mathf.PI - 90);

        PlayerAnim.SetBool("Walk", true);

        if (serverExists)
        {
            ServerUpdatePos();
            ServerUpdateRot();
        }
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

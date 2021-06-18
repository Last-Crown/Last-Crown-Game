using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Quobject.SocketIoClientDotNet.Client;

public class PlayerInput : MonoBehaviour
{
    private JoyStick joyStick;
    private Animator playerAnim;

    private bool serverExists;
    private Socket socket;

    private PlayerAction playerAction;
    private PlayerMovement playerMovement;

    private void Awake()
    {
        joyStick = GameObject.Find("JoyStick").GetComponent<JoyStick>();
        playerAnim = GetComponent<Animator>();
        playerAction = GetComponent<PlayerAction>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))   // Pick
            playerAction.PickEquipment();

        if (Input.GetKeyDown(KeyCode.F))    // Drop
            playerAction.DropEquipment();

        if (Input.GetKeyDown(KeyCode.E))
        { // Tool Cycle
            playerAction.CycleTools();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {  // Tool Activate
            if (GameObject.FindWithTag("Server"))
                GameObject.FindWithTag("Server").GetComponent<ServerInitializer>().socket.Emit("play playerAnimation");
            playerAction.ActivateEquipment();
        }

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

        if (Input.anyKey) playerMovement.KeyboardMove();
        if (joyStick.Dir != Vector2.zero) playerMovement.JoystickMove();

        if (!Input.anyKey && joyStick.Dir == Vector2.zero) playerAnim.SetBool("Walk", false);
    }
}

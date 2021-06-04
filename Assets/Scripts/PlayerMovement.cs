using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(new Vector3(0, 0.05f, 0));
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(new Vector3(-0.05f, 0, 0));
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(new Vector3(0, -0.05f, 0));
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(new Vector3(0.05f, 0, 0));
        }
    }

    private void move()
    {
        float dx = Input.GetAxis("Horizontal");
        float dy = Input.GetAxis("Vertical");

        transform.Translate(new Vector2(dx, dy) * speed * Time.deltaTime);
    }
}

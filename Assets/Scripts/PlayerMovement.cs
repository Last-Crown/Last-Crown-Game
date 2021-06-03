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
    }

    private void move()
    {
        float dx = Input.GetAxis("Horizontal");
        float dy = Input.GetAxis("Vertical");

        transform.Translate(new Vector2(dx, dy) * speed * Time.deltaTime);
    }
}

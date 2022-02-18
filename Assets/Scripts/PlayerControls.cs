using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float runSpeed = 10f;

    public Rigidbody2D rb;

    public Camera cam;

    public bool enableRun = false;

    Vector2 movement;
    Vector2 mousePos;

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            enableRun = true;
        }
        else
        {
            enableRun = false;
        }
    }

    void FixedUpdate()
    {
        float speed = 0f;
        if (enableRun == true)
        {
            speed = runSpeed;
        }
        else if (enableRun == false)
        {
            speed = moveSpeed;
        }
        
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
        Vector2 lookDir = mousePos - rb.position;
        float angle = (Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg) - 90f;
        rb.rotation = angle;
    }
}
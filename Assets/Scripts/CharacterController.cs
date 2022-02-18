using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{

    //Control keys
    string UP = "w";
    string DOWN = "s";
    string LEFT = "a";
    string RIGHT = "d";
    string HIDE = "h";
    string CROUCH = "c";
    string VAULT = "alt";
    string RUN = "shift";
    string STEALH_ATT = "f";
    string NORMAL_ATT = "f";

    //Status

    [SerializeField] bool isRunning = false;
    [SerializeField] bool isCrouching = false;
    [SerializeField] bool isHiding = false;
    [SerializeField] bool isVaulting = false;
    

    //Stat Values
    [SerializeField] private float movementSpeed = 2f;
    [SerializeField] private float normalSpeed = 2f;
    [SerializeField] private float runningSpeed = 4f;
    [SerializeField] private float crouchSpeed = 1.2f;
    [SerializeField] private float movementSmoothing = 0.05f;

    //Misc Values
    Vector2 movement;
    Vector2 mousePos;

    Vector2 velocity = Vector2.zero;

    //GameObjects/Components
    Rigidbody2D m_Rigidbody2D;

    public Camera cam;

    void Update()
    {
        if (Input.GetKeyDown(HIDE))
        {
            isHiding = !isHiding;
        }

        if (!isHiding || !isVaulting)
        {
            mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

            movement.x = Input.GetAxisRaw("Horizontal") * movementSpeed;
            movement.y = Input.GetAxisRaw("Vertical") * movementSpeed;

            if (Input.GetKeyDown(CROUCH))
            {
                //CROUCH
                isCrouching = !isCrouching;

                if (!isCrouching)
                {
                    movementSpeed = normalSpeed;
                }
                else if (isCrouching)
                {
                    movementSpeed = crouchSpeed;
                    isRunning = false;
                }
            }

            if (Input.GetKeyDown(RUN))
            {
                //CROUCH
                isRunning = !isRunning;

                if (!isRunning)
                {
                    movementSpeed = normalSpeed;
                }
                else if (isRunning)
                {
                    movementSpeed = runningSpeed;
                    isCrouching = false;
                }
            }

            if (Input.GetKeyDown(VAULT))
            {

            }

            if (Input.GetKeyDown(HIDE))
            {

            }

            if (Input.GetKeyDown(STEALH_ATT))
            {
                //STEALH ATTACK
            }

            if (Input.GetKeyDown(NORMAL_ATT))
            {
                //NORMAL_ATTACK
            }

            m_Rigidbody2D.velocity = Vector2.SmoothDamp(m_Rigidbody2D.velocity, movement, ref velocity, movementSmoothing);

            Vector2 lookDir = mousePos - m_Rigidbody2D.position;
            float angle = (Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg) - 90f;
            m_Rigidbody2D.rotation = angle;
        }
    }

    private void FixedUpdate()
    {

    }
}

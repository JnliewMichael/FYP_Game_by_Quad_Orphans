using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideScrollControls : MonoBehaviour
{

    //Control keys
    string UP = "w";
    string JUMP = "space";
    //string DOWN = "s";
    string LEFT = "a";
    string RIGHT = "d";
    string HIDE = "h";
    string CROUCH = "c";
    string STEALH_ATT = "f";
    string NORMAL_ATT = "f";

    //Status
    [SerializeField] bool isFacingRight = true;

    [SerializeField] bool isCrouching = false;
    [SerializeField] bool isHiding = false;
    [SerializeField] bool isGrounded = false;


    //Stat Values
    [SerializeField] private float movementSpeed = 2f;
    [SerializeField] private float normalSpeed = 2f;
    [SerializeField] private float crouchSpeed = 1.2f;
    [SerializeField] private float movementSmoothing = 0.05f;
    [SerializeField] private float jumpForce = 400f;

    //Misc Values
    float horizontalMovement = 0f;
    Vector3 targetVelocity = Vector3.zero;
    Vector3 velocity = Vector3.zero;

    //GameObjects/Components
    Rigidbody2D m_Rigidbody2D;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask whatIsGround;
    const float groundedRadius = 2f;



    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(HIDE))
        {
            isHiding = !isHiding;
        }

        if (!isHiding && isGrounded)
        {
            horizontalMovement = Input.GetAxisRaw("Horizontal") * movementSpeed;

            if (Input.GetKeyDown(UP))
            {
                //JUMP
                isGrounded = false;
                isCrouching = false;
                m_Rigidbody2D.AddForce(new Vector2(0f, jumpForce));
            }

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
                }
            }

            if (Input.GetKeyDown(STEALH_ATT))
            {
                //STEALH ATTACK
            }

            if (Input.GetKeyDown(NORMAL_ATT))
            {
                //NORMAL_ATTACK
            }

            targetVelocity = new Vector2(horizontalMovement * 10f, m_Rigidbody2D.velocity.y);
            m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref velocity, movementSmoothing);

            // If the input is moving the player right and the player is facing left...
            if (horizontalMovement > 0 && !isFacingRight)
            {
                // ... flip the player.
                Flip();
            }
            // Otherwise if the input is moving the player left and the player is facing right...
            else if (horizontalMovement < 0 && isFacingRight)
            {
                // ... flip the player.
                Flip();
            }
        }
    }

    private void FixedUpdate()
    {
        isGrounded = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundedRadius, whatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
                isGrounded = true;
        }
    }




    private void Flip()
    {
        isFacingRight = !isFacingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}

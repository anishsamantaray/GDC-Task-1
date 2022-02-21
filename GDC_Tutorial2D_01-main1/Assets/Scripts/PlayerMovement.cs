using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    PlayerControls controls;

    float direction = 0;
    [SerializeField]
    public float speed;                    // player movement speed
    public bool isFacingRight = true;
    [SerializeField]
    public float jumpForce;                // player jump force
    bool isGrounded;
    int numberOfJumps = 0;
    [SerializeField]
    public Transform groundCheck;          //ground check gameobject
    [SerializeField]
    public LayerMask groundLayer;          // layer mask to use as ground

    public Rigidbody2D playerRB;

    
    private void Awake()
    {
        controls = new PlayerControls();
        controls.Enable();

        controls.Player.Move.performed += ctx =>
        {
            direction = ctx.ReadValue<float>();
        };

        controls.Player.Jump.performed += ctx => Jump();
    }

    // better optimized for physics objects
    void FixedUpdate()
    {
        // obeys physics and instant movement
        playerRB.velocity = new Vector2(direction * speed * Time.fixedDeltaTime, playerRB.velocity.y);

        // check if player is grounded
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
        
        if (isFacingRight && direction < 0 || !isFacingRight && direction > 0)
            Flip();
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
    }

    // jump input
    void Jump()
    {
        if (isGrounded)
        {
            numberOfJumps = 0;
            playerRB.velocity = new Vector2(playerRB.velocity.x, jumpForce);
            numberOfJumps++;
            
        }
        else
        {
            if (numberOfJumps == 1)
            {
                playerRB.velocity = new Vector2(playerRB.velocity.x, jumpForce);
                numberOfJumps++;
                
            }
        }
    }

}
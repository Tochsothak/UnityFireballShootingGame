using System;
using System.Net.Mail;
using TreeEditor;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider2D;
    private float wallJumpCooldown;

    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;

    [Header ("Coyote Time")]
    [SerializeField]private float coyoteTime; // How much time the player can hang in the air before jumping
    private float coyoteCounter;

    [Header("Multiple Jump")]
    [SerializeField] private int extraJumps;
    private int jumpCounter;

    [Header("Wall Jumping")]
    [SerializeField]private float wallJumpX;
    [SerializeField]private float wallJumpY;
    private float horizontalInput;
    [Header("Sounds")]
    [SerializeField]private AudioClip jumpSound;

    // Grab reference for rigidbody and animator from game object
    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        // Horizontal movement
         horizontalInput = Input.GetAxis("Horizontal");
       

        // Flip the player when moving left and right
        if (horizontalInput > 0.01f) 
            transform.localScale = Vector3.one;
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-1,1,1);

        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", IsGrounded());

        // Jump
        if(Input.GetKeyDown(KeyCode.Space))
            Jump();
        // Adjustable jump height
        if (Input.GetKeyUp(KeyCode.Space) && body.linearVelocity.y > 0)
            body.linearVelocity = new Vector2(body.linearVelocity.x, body.linearVelocity.y / 2);
        
        if (OnWall()){
            body.gravityScale = 0;
            body.linearVelocity = Vector2.zero;
        }
        else {
            body.gravityScale = 7;
            body.linearVelocity = new Vector2(horizontalInput * moveSpeed, body.linearVelocity.y);

            if (IsGrounded()){
                coyoteCounter = coyoteTime;
                jumpCounter = extraJumps; 
            }
            else 
                coyoteCounter -= Time.deltaTime;

        }
        
    }

    private void Jump ( ){
        if (coyoteCounter <=0 && !OnWall() && jumpCounter <= 0 ) return;
         SoundManager.instance.PlaySound(jumpSound);

         if (OnWall())
            WallJump();
        else {
            if (IsGrounded())
            body.linearVelocity = new Vector2(body.linearVelocity.x, jumpForce);
            else {
                if (coyoteCounter > 0)
                body.linearVelocity = new Vector2(body.linearVelocity.x, jumpForce);
                else {
                    if (jumpCounter > 0){
                        body.linearVelocity = new Vector2(body.linearVelocity.x, jumpForce);
                        jumpCounter--;
                    }
                }
            }
            coyoteCounter = 0;
        }

    }

    private void WallJump(){
        body.AddForce(new Vector2(-Math.Sign(transform.localScale.x) * wallJumpX, wallJumpY));
        wallJumpCooldown = 0;
    }

    private bool IsGrounded(){
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    private bool OnWall (){
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null; 
    }

    // Player shooting
    public bool canAttack (){
        return horizontalInput == 0 && IsGrounded() && !OnWall();
    }
}

using System.Net.Mail;
using TreeEditor;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

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
    [SerializeField] private float horizontalInput;
   

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

        //Wall jump logic
        if (wallJumpCooldown < 0.2f){
           body.linearVelocity = new Vector2(horizontalInput * moveSpeed,
           body.linearVelocity.y);

           if (OnWall() && !IsGrounded()){
            body.gravityScale = 0;
            body.linearVelocity = Vector2.zero;
           }
           else body.gravityScale = 7;

           if (Input.GetKey(KeyCode.Space)){
            Jump();
           }
        }
        else wallJumpCooldown += Time.deltaTime;
    }

    private void Jump ( ){
        if (IsGrounded() ){
             body.linearVelocity = new Vector2(body.linearVelocity.x, jumpForce);
             anim.SetTrigger("jump");
        }
        else if (OnWall() && !IsGrounded()){

            if (horizontalInput == 0){
                body.linearVelocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 10, 0);
                transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else 
             body.linearVelocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 16, 6);
              wallJumpCooldown = -0;   
              anim.SetTrigger("jump");  
        }

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

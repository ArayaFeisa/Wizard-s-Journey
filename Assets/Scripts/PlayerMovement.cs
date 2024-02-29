using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private LayerMask groundLayer;
    // public bool midAir;
    // public groundCheckTest check;
    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private float wallJumpCd;
    private bool doubleJumpUnlocked;
    private bool doubleJumpCD;
    private float horizontalInput;
    public Vector2 vel;

    public bool facing;
    private bool inactive;
    public int offset;
    private float pushAbleTimer;
    public PushAbleScript push;
    public GameObject summonPush;

    private void Awake() {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        summonPush.SetActive(false);
        inactive = true;
        doubleJumpUnlocked = true;
    }

    private void Update() {
        vel = body.velocity;
        horizontalInput = Input.GetAxis("Horizontal");

        if (horizontalInput > 0.01f){
            facing = true;
            transform.localScale = Vector3.one*5f;
        }
        else if (horizontalInput < -0.01f) {
            facing = false;
            transform.localScale = new Vector3(-5, 5, 5);
        }

        anim.SetBool("walk", horizontalInput != 0);
        anim.SetBool("grounded", isGrounded());

        if (wallJumpCd < 0.7f){
            body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

            if (Input.GetKey(KeyCode.Space) && isGrounded()){
                Jump();
                anim.SetTrigger("jump");
            }
            if (Input.GetKeyDown(KeyCode.Space) && doubleJumpCD && doubleJumpUnlocked && !isGrounded())
            {
                Jump();
                doubleJumpCD = false;
            }
            if (isGrounded())
            {
                doubleJumpCD = true;
            }
            if (onWall() && !isGrounded()){
                body.velocity = new Vector2(0, body.velocity.y);
            } else {
                body.gravityScale = 2;
            }
            
        } else {
            wallJumpCd += Time.deltaTime;
        }

        if (inactive && Input.GetKey(KeyCode.E))
        {
            summonPush.SetActive(true);
            push.transform.position = transform.position + new Vector3((facing) ? offset : -offset, 0, 0);
            inactive = false;
            pushAbleTimer = 0;
        }
        else if (Input.GetKey(KeyCode.E))
        {
            push.body1.velocity = vel;
        }

        if (!inactive)
        {
            if (pushAbleTimer < 5)
            {
                pushAbleTimer += Time.deltaTime;
            }
            else
            {
                summonPush.SetActive(false);
                inactive = true;
            }
        }

    }
    private void Jump(){
        body.velocity = new Vector2(body.velocity.x, speed);
    }
    private bool isGrounded(){
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }
    private bool onWall(){
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x,0), 0.1f, groundLayer);
        return raycastHit.collider != null;
    }
    public bool canAttack(){
        return !onWall(); //&& horizontalInput == 0; 
    }
}

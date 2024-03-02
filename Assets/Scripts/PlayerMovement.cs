using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float jumpPow=12;
    [SerializeField] private float speed;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private TrailRenderer trail;

    private bool canDash = true;
    private bool isDashing;
    private float dashSpeed = 5f;
    private float dashCD = 1f;
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
    public doorScript door;

    public Transform levers;
    private void Awake() {
        //Physics2D.IgnoreCollision(GetComponent<Collider2D>(), levers.GetComponent<Collider2D>());
        isDashing = false;
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        summonPush.SetActive(false);
        inactive = true;
        doubleJumpUnlocked = true;
    }

    private void Update() {
        if (isDashing)
        {
            return;
        }
        vel = body.velocity;
        horizontalInput = Input.GetAxis("Horizontal");
        horizontalMovement();
        summonPushable();
        Movement();
        anim.SetBool("walk", horizontalInput != 0);
        anim.SetBool("grounded", isGrounded());
    }
    private void Movement()
    {
        if (Input.GetKey(KeyCode.W) && isGrounded())
        {
            Jump();
            anim.SetTrigger("jump");
        }
        if (Input.GetKeyDown(KeyCode.W) && doubleJumpCD && doubleJumpUnlocked && !isGrounded())
        {
            Jump();
            doubleJumpCD = false;
        }
        if (isGrounded())
        {
            doubleJumpCD = true;
        }
        if (onWall() && !isGrounded())
        {
            body.velocity = new Vector2(0, body.velocity.y);
        }
        else
        {
            body.gravityScale = 3;
        }

        if (!isDashing && !onWall()) body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }
    }
    private void Jump(){
        body.velocity = new Vector2(body.velocity.x, jumpPow);
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

    private void horizontalMovement()
    {
        if (horizontalInput > 0.01f)
        {
            facing = true;
            transform.localScale = Vector3.one * 5f;
        }
        else if (horizontalInput < -0.01f)
        {
            facing = false;
            transform.localScale = new Vector3(-5, 5, 5);
        }
    }

    private void summonPushable()
    {
        if (inactive && Input.GetKeyDown(KeyCode.E))
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
            if (pushAbleTimer < 10)
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

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        body.gravityScale = 0f;
        body.velocity = new Vector2(transform.localScale.x * dashSpeed, 0f);
        trail.emitting = true;
        yield return new WaitForSeconds(0.2f);
        trail.emitting = false;
        body.gravityScale = 3f;
        isDashing = false;
        yield return new WaitForSeconds(dashCD);
        canDash = true;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Lever"))
        {
            Debug.Log("interact");
            door.interactAble = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Lever"))
        {
            door.interactAble = false;
        }
    }
}

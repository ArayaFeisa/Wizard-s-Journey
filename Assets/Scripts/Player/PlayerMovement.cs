using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private float jumpPow=14;
    [SerializeField] private float speed;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private TrailRenderer trail;

    private bool isDashing;
    private float dashSpeed = 10f;
    private float dashCD = 1f;
    // public bool midAir;
    // public groundCheckTest check;
    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private float wallJumpCd;
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

    private float coyoteTime = 0.1f;
    private float coyoteTimer;
    private float jumpBufferTime = 0.1f;
    private float jumpBufferTimer;

    private bool canDash;
    private bool canDoubleJump;
    private bool canSummonPushable;

    public int lvl1ShardCount;
    public UnityEngine.UI.Text shardCounter;
    public int lvl2ShardCount;
    public int lvl3ShardCount;
    private void Awake() {
        //Physics2D.IgnoreCollision(GetComponent<Collider2D>(), levers.GetComponent<Collider2D>());
        isDashing = false;
        canDoubleJump = GameManager.instance.canDoubleJump;
        canDash = GameManager.instance.canDash;
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        summonPush.SetActive(false);
        inactive = true;
        lvl1ShardCount = GameManager.instance.lvl1ShardCount;
        lvl2ShardCount = GameManager.instance.lvl2ShardCount;
    }
    private void Start() {
        // Debug.Log(PlayerPrefs.GetInt("Lompat"));
    }

    private void Update() {

        if (SceneManager.GetActiveScene().name.Equals("Stage 1")) shardCounter.text = lvl1ShardCount + "/18";
        if (SceneManager.GetActiveScene().name.Equals("Stage 2")) shardCounter.text = lvl2ShardCount + "/24";
        if (SceneManager.GetActiveScene().name.Equals("Stage 3")) shardCounter.text = lvl3ShardCount + "/30";
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
        if (isGrounded())
        {
            coyoteTimer = coyoteTime;
        }
        else
        {
            coyoteTimer -= Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.W)){
            jumpBufferTimer = jumpBufferTime;
        }
        else
        {
            jumpBufferTimer -= Time.deltaTime;
        }
        if (jumpBufferTimer > 0 && coyoteTimer > 0f)
        {
            Jump();
            jumpBufferTimer = 0f;
            doubleJumpCD = true;
            anim.SetTrigger("jump");
        }
        if(Input.GetKeyUp(KeyCode.W) && body.velocity.y > 0f)
        {
            body.velocity = new Vector2(body.velocity.x, body.velocity.y * 0.5f);
            coyoteTimer = 0f;
        }
        if (Input.GetKeyDown(KeyCode.W) && doubleJumpCD && canDoubleJump && !isGrounded() && coyoteTimer <= 0f)
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
        // PlayerPrefs.SetInt("Lompat", 5);
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
        if (horizontalInput > 0)
        {
            facing = true;
            transform.localScale = Vector3.one * 5f;
        }
        else if (horizontalInput < 0)
        {
            facing = false;
            transform.localScale = new Vector3(-5, 5, 5);
        }
    }

    private void summonPushable()
    {
        if (inactive && Input.GetKeyDown(KeyCode.E) && canSummonPushable)
        {
            summonPush.SetActive(true);
            push.transform.position = transform.position + new Vector3(0, 1.5f, 0);
            inactive = false;
            pushAbleTimer = 0;
        }
        else if (Input.GetKey(KeyCode.E))
        {
            push.body1.gravityScale = 0;
            push.body1.velocity = vel;
        }
        else 
        {
            push.body1.gravityScale = 1;
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
        yield return new WaitForSeconds(0.1f);
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("AirOrb"))
        {
            canDoubleJump = true;
            GameManager.instance.canDoubleJump = canDoubleJump;
            collision.gameObject.SetActive(false);
        }
        if (collision.CompareTag("LightningOrb"))
        {
            canDash = true;
            GameManager.instance.canDash = canDash;
            collision.gameObject.SetActive(false);
        }
        if (collision.CompareTag("EarthOrb"))
        {
            canSummonPushable = true;
            GameManager.instance.canSummonPushable = canSummonPushable;
            collision.gameObject.SetActive(false);
        }
        if (collision.CompareTag("lvl1Shard"))
        {
            lvl1ShardCount++;
            GameManager.instance.lvl1ShardCount = lvl1ShardCount;
            collision.gameObject.tag = ("Untagged");
        }
        if (collision.CompareTag("lvl2Shard"))
        {
            lvl2ShardCount++;
            GameManager.instance.lvl2ShardCount = lvl2ShardCount;
            collision.gameObject.tag = ("Untagged");
        }
        if (collision.CompareTag("lvl3Shard"))
        {
            lvl3ShardCount++;
            GameManager.instance.lvl3ShardCount = lvl3ShardCount;
            collision.gameObject.tag = ("Untagged");
        }
        if (collision.CompareTag("FallingDripstone"))
        {
            GetComponent<Health>().takeDamage(20);
            collision.gameObject.SetActive(false);
        }
    }
}

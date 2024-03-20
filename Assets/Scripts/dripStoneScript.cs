using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dripStoneScript : MonoBehaviour
{
    [SerializeField] private float fallspeed;
    public dripstoneTriggerScript trig;
    public ParticleSystem particle;
    public Rigidbody2D gravity;
    public SpriteRenderer sprite;
    public Transform pos;
    public CapsuleCollider2D capsule;
    public Health respawnDrip;
    private bool falling;
    private bool hit;
    private bool idle;
    private float timer;
    private Vector3 ori;


    // Start is called before the first frame update
    void Start()
    {
        //capsule.enabled = true;
        ori = gameObject.transform.position;
        gravity.gravityScale = 0;

    }

    // Update is called once per frame
    void Update()
    {
        if (trig.isIn && !falling && !idle)
        {
            StartCoroutine(fall());
        }

        if(respawnDrip.respawnDripStone && idle)
        {
            StartCoroutine(respawn());
        }
        if (hit)
        {
            gravity.gravityScale = 0;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && falling)
        {
            collision.gameObject.GetComponent<Health>().takeDamage(20);
        }
        //if(collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("P"))
        hit = true;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        hit = true; 
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        hit = false;
    }

    private IEnumerator fall()
    {
        //capsule.enabled = true;
        idle = false;
        falling = true;
        gravity.gravityScale = fallspeed;
        yield return new WaitUntil(() => hit);
        gravity.gravityScale = 0;
        gravity.velocity = new Vector2(0,0);
        GetComponent<ParticleSystem>().Emit(100);
        yield return new WaitForSeconds(0.35f);
        gameObject.transform.position = ori;
        sprite.enabled = false;
        timer = 0;
        trig.isIn = false;
        falling = false;
        idle = true;
        hit = false;
        //capsule.enabled = false;
    }

    private IEnumerator respawn()
    {
        trig.isIn = false;
        timer = 0;
        sprite.enabled = true;
        idle = false;
        yield return new WaitForSeconds(1);
        respawnDrip.respawnDripStone = false;
    }
}

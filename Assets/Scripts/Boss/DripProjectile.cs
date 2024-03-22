using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DripProjectile : BossDamaged
{
    [SerializeField] private float speed;
    [SerializeField] private float resetTime;
    private float lifetime;
    private Animator anim;
    private BoxCollider2D coll;
    private bool canStun;

    private bool hit;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();
    }
    public void ActivateProjectile()
    {
        hit = false;
        lifetime = 0;
        gameObject.SetActive(true);
        coll.enabled = true;
    }
    private void Update()
    {
        canStun = GameManager.instance.canStun;
        if (hit) return;
        if (canStun){
            float movementSpeed = speed * Time.deltaTime;
            transform.Translate(0, movementSpeed, 0);

            lifetime += Time.deltaTime;
            if (lifetime > resetTime)
                gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") || collision.CompareTag("Ground"))
        {
            hit = true;
            base.OnTriggerEnter2D(collision);
            coll.enabled = false;
            if (anim != null)
                anim.SetTrigger("explode");
            else
                gameObject.SetActive(false);
                canStun = false;
                GameManager.instance.canStun = canStun;
        }
    }
}

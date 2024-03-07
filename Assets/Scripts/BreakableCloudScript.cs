using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableCloudScript : MonoBehaviour
{
    // Start is called before the first frame update
    private float timer;
    private bool broken;
    private Animator anim;
    private BoxCollider2D collider;
    private SpriteRenderer render;
    private bool action;
    void Start()
    {
        action = false;
        broken = false;
        timer = 0;
        anim = GetComponent<Animator>();
        collider = GetComponent<BoxCollider2D>();
        render = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (broken)
        {
            if (timer > 3 && !action)
            {
                timer = 0;
                regenerating();
            }
            else
            {
                timer += Time.deltaTime;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!action)
        {
            breaking();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        
    }

    private void breaking()
    {
        action = true;
        anim.SetTrigger("destroy");
        broken = true;
        action = false;
    }


    private void regenerating()
    {
        action = true;
        anim.SetTrigger("regenerating");
        broken = false;  
        action = false;
        anim.SetTrigger("idle");
    }
}

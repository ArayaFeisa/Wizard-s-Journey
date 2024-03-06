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
    private bool action;
    void Start()
    {
        action = false;
        broken = false;
        timer = 0;
        anim = GetComponent<Animator>();
        collider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetTrigger("regenerating");
        while (broken)
        {
            if (timer < 3 && !action)
            {
                timer = 0;
                StartCoroutine(regenerating());
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
            StartCoroutine(breaking());
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        
    }

    private IEnumerator breaking()
    {
        action = true;
        anim.SetTrigger("destroy");
        yield return new WaitForSeconds(1.2f);
        collider.enabled = false;
        broken = true;
        action = false;
    }

    private IEnumerator regenerating()
    {
        action = true;
        anim.SetTrigger("regenerating");
        yield return new WaitForSeconds(0.35f);
        collider.enabled = true;
        broken = false;  
        action = false;
    }
}

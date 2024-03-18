using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particleTesting : MonoBehaviour
{
    // Start is called before the first frame update
    private ParticleSystem ps;
    private Animator animator;
    private float ori;
    private bool move;
    private bool collected;
    private SpriteRenderer spriteRenderer;
    void Start()
    {
        collected = false;
        animator = GetComponent<Animator>();
        ps = GetComponent<ParticleSystem>();
        ori = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        var shape = ps.shape;
        shape.rotation = new Vector3(0, 0, shape.rotation.z + (720 * Time.deltaTime));
        if (!collected)
        {
            if (transform.position.y >= ori)
            {
                move = true;
            }
            if (transform.position.y <= ori - 0.1f)
            {
                move = false;
            }
            if (!move)
            {
                transform.position = new Vector2(transform.position.x, transform.position.y + (0.1f * Time.deltaTime));
            }
            if (move)
            {
                transform.position = new Vector2(transform.position.x, transform.position.y - (0.1f * Time.deltaTime));
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!collected)
            {
                StartCoroutine(collect());
            }
        }
    }

    private IEnumerator collect()
    {
        var shape = ps.shape;
        collected = true;
        shape.enabled = false;
        ps.enableEmission = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
        for (int i=0;i<100;i++) 
        {
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, spriteRenderer.color.a - 0.01f);
            transform.position = new Vector2(transform.position.x,transform.position.y + 0.01f);
            yield return new WaitForSeconds(0.01f);
        }
        gameObject.SetActive(false);
    }
}

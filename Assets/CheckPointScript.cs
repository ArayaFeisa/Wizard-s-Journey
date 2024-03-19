using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointScript : MonoBehaviour
{
    public SpriteRenderer render;
    public Sprite checkPointOn;
    private bool on;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!on)
            {
                StartCoroutine(turnOn());
            }
        }
    }

    private IEnumerator turnOn()
    {
        ParticleSystem ps = GetComponent<ParticleSystem>();
        on = true;
        var em = ps.emission;
        em.enabled = true;
        yield return new WaitForSeconds(1);
        render.sprite = checkPointOn;
        yield return new WaitForSeconds(1);
        em.enabled = false;
    }
}

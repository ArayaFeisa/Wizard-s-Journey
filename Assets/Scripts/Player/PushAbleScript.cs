using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushAbleScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public Rigidbody2D body1;
    public PlayerMovement pos;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //body.transform.localScale += new Vector3(0.01f, 0.01f, 0.01f);
        if (body1.velocity == Vector2.zero)
        {
            gameObject.tag = "Ground";
            gameObject.layer = 6;
        }
        else
        {
            gameObject.tag = "Pushable";
            gameObject.layer = 8;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            body1.velocity = body1.velocity * -1f;
        }
    }
}

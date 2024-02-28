using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushAbleScript : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayerMovement push;
    private Rigidbody2D body;
    private bool pushed;
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            body.velocity = push.vel;
        }
        if (body.velocity == Vector2.zero)
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
}

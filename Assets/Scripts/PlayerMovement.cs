using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D body;
    [SerializeField] private float speed;
    public bool midAir;
    public groundCheckTest check;

    private void Awake() {
        body = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        body.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, body.velocity.y);

        if (Input.GetKey(KeyCode.Space) && midAir == false){
            body.velocity = new Vector2(body.velocity.x, speed);
        }

    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D body;
    private bool grounded;
    [SerializeField] private float speed;
    // public bool midAir;
    // public groundCheckTest check;

    private void Awake() {
        body = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        float horizontalInput = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

        if (horizontalInput > 0.01f){
            transform.localScale = Vector3.one;
        }
        else if (horizontalInput < -0.01f) {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        if (Input.GetKey(KeyCode.Space) && grounded){
            Jump();
        }
    }
    
    private void Jump(){
        body.velocity = new Vector2(body.velocity.x, speed);
        grounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Ground")
            grounded = true;
    }

    private void OnCollisionExit2D(Collision2D collision) {
        if (collision.gameObject.tag == "Ground")
            grounded = false;
    }
    // private bool isGrounded(){
    //     return false;
    // }
}

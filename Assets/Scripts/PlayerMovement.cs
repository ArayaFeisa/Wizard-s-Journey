using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private LayerMask groundLayer;
    // public bool midAir;
    // public groundCheckTest check;
    private Rigidbody2D body;
    private BoxCollider2D boxCollider;
    private float wallJumpCd;

    private void Awake() {
        body = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update() {
        float horizontalInput = Input.GetAxis("Horizontal");

        if (horizontalInput > 0.01f){
            transform.localScale = Vector3.one*5f;
        }
        else if (horizontalInput < -0.01f) {
            transform.localScale = new Vector3(-5, 5, 5);
        }

        if (wallJumpCd < 0.7f){
            body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

            if (Input.GetKey(KeyCode.Space) && isGrounded()){
                Jump();
            }
            if (onWall() && !isGrounded()){
                body.gravityScale++;
                body.velocity = Vector2.zero;
            } else {
                body.gravityScale = 25/10;
            }
            
        } else {
            wallJumpCd += Time.deltaTime;
        }
    }
    private void Jump(){
        body.velocity = new Vector2(body.velocity.x, speed);
    }
    private bool isGrounded(){
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }
    private bool onWall(){
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x,0), 0.1f, groundLayer);
        return raycastHit.collider != null;
    }
}

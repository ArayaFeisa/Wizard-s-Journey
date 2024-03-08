using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlying : MonoBehaviour
{
    [SerializeField] private float flySpeed;
    private GameObject player;
    public bool chase = false;
    // private bool movingLeft;
    [SerializeField] private Transform enemy;
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;
    [SerializeField] private Transform startingPoint;
    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update() {
        if (player == null)
            return;

        if (chase == true){
            Chase();
            Flip();
        } else {
            ReturnToStart();
        }
    }

    private void Chase(){
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, flySpeed * Time.deltaTime);
    }

    private void Flip(){
        if (transform.position.x > player.transform.position.x){
            transform.localScale = new Vector3(-3,3,3);
        } else {
            transform.localScale = Vector3.one * 3f;
        }
    }
    private void ReturnToStart(){
        if (transform.position.x > startingPoint.transform.position.x){
            transform.localScale =  new Vector3(-3,3,3);
        } else {
            transform.localScale =  Vector3.one * 3f;
        }
        transform.position = Vector2.MoveTowards(transform.position, startingPoint.position, flySpeed * Time.deltaTime);
    }

    // private void MoveIntoDirection(int _direction){
    //     transform.position = new Vector3(transform.position.x + Time.deltaTime * flySpeed * _direction, 
    //         transform.position.y, transform.position.z);
    // }

    // private void DirectionChange(){
    //     movingLeft = !movingLeft;
    // }
}

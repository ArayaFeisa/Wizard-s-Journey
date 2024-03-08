using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlying : MonoBehaviour
{
    [SerializeField] private float flySpeed;
    private GameObject player;
    public bool chase = false;
    [SerializeField] private Transform startingPoint;
    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update() {
        if (player == null)
            return;
            
        if (chase == true){
            Chase();  
        } else {
            ReturnToStart();
        }
        Flip();
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
        transform.position = Vector2.MoveTowards(transform.position, startingPoint.position, flySpeed * Time.deltaTime);
    }
}

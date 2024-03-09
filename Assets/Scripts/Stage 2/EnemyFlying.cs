using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlying : MonoBehaviour
{
    [SerializeField] private float flySpeed;
    private GameObject player;
    public bool chase = false;
    [SerializeField] private Transform enemy;
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;
    [SerializeField] private Transform[] patrolPoints;
    private int patrolDestination;
    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update() {
        if (player == null)
            return;

        if (!chase){
            if (patrolDestination == 0){
                transform.position = Vector2.MoveTowards(transform.position, patrolPoints[0].position, flySpeed * Time.deltaTime);
                transform.localScale =  new Vector3(-3,3,3);

                if(Vector2.Distance(transform.position, patrolPoints[0].position) < .2f){
                    transform.localScale =  Vector3.one * 3f;
                    patrolDestination = 1;
                }
            }
            if (patrolDestination == 1){
                transform.position = Vector2.MoveTowards(transform.position, patrolPoints[1].position, flySpeed * Time.deltaTime);
                transform.localScale =  Vector3.one * 3f;

                if(Vector2.Distance(transform.position, patrolPoints[1].position) < .2f){
                    transform.localScale =  new Vector3(-3,3,3);
                    patrolDestination = 0;
                }
            }
        } else {
            Chase();
            Flip();
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
}

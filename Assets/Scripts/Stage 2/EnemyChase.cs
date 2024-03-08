using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    [Header ("Patrol Points")]
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;

    [Header ("Enemy")]
    [SerializeField] private Transform enemy;

    [Header ("Movement")]
    [SerializeField] private float speed;
    private Vector3 initScale;
    private bool movingLeft;
    public Transform playerTransform;
    private bool isChasing;
    private float chaseDistance;

    private void Awake() {
        initScale = enemy.localScale;
    }
    private void Update() {
        if (isChasing){
            if (transform.position.x > playerTransform.position.x){
                transform.position += Vector3.left * speed * Time.deltaTime;
            } 
            if (transform.position.x < playerTransform.position.x){
                transform.position += Vector3.right * speed * Time.deltaTime;
            } 
        } else {
            if (Vector2.Distance(transform.position, playerTransform.position) < chaseDistance){
                isChasing = true;
            }
        }
        
        if (movingLeft){
            if (enemy.position.x >= leftEdge.position.x){
                MoveInDirection(-1);
            } else {
                DirectionChange();
            }
        } else {
            if (enemy.position.x <= rightEdge.position.x){
                MoveInDirection(1);
            } else {
                DirectionChange();
            }
        }
    }
    private void DirectionChange(){
        movingLeft = !movingLeft;
    }
    private void MoveInDirection(int _direction){

        enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * _direction, 
            initScale.y, initScale.z);

        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * _direction * speed,
            enemy.position.y, enemy.position.z);
    }
}

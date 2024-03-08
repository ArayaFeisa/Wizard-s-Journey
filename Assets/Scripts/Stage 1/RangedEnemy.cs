using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    [Header ("Attack")]
    [SerializeField] private float attackCD;
    [SerializeField] private float range;
    [SerializeField] private int damage;

    [Header ("Range Attack")]
    [SerializeField] private Transform firepoint;
    [SerializeField] private GameObject[] poisons;

    [Header ("Collider")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;

    [Header ("Player")]
    [SerializeField] private LayerMask playerLayer;
    private float cdTimer = Mathf.Infinity;
    private Animator anim;
    private EnemyPatrol enemyPatrol;

    private void Awake() {
        anim = GetComponent<Animator>();
        enemyPatrol = GetComponentInParent<EnemyPatrol>();
    }
    private void Update() {
        cdTimer += Time.deltaTime;

        if ((PlayerInRange())){
            if (cdTimer >= attackCD){
                cdTimer = 0;
                anim.SetTrigger("rangedAttack");
            }
        }

        if (enemyPatrol != null){
            enemyPatrol.enabled = !PlayerInRange();
        }
    }
    private void RangedAttack(){
        cdTimer = 0;
        poisons[findFireball()].transform.position = firepoint.position;
        poisons[findFireball()].GetComponent<EnemyProjectile>().ActivateProjectile();
    }
    private int findFireball(){
        for (int i = 0; i < poisons.Length; i++)
        {
            if (!poisons[i].activeInHierarchy){
                return i;
            }
        }
        return 0;
    }
    private bool PlayerInRange(){
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z)
            , 0, Vector2.left, 0, playerLayer);

        return hit.collider != null;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, 
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }
}

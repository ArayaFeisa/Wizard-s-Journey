using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    [SerializeField] private float attackCD;
    [SerializeField] private float range;
    [SerializeField] private float colliderDistance;
    [SerializeField] private int damage;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask playerLayer;
    private float cdTimer = Mathf.Infinity;
    private Animator anim;
    private PlayerHealth playerHealth;
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
                anim.SetTrigger("meleeAttack");
            }
        }

        if (enemyPatrol != null){
            enemyPatrol.enabled = !PlayerInRange();
        }
    }
    private bool PlayerInRange(){
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z)
            , 0, Vector2.left, 0, playerLayer);

        if (hit.collider != null){
            playerHealth = hit.transform.GetComponent<PlayerHealth>();
        }
        return hit.collider != null;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, 
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

    private void damagePlayer(){
        if(PlayerInRange()){
            playerHealth.takeDamage(damage);
        }
    }
}

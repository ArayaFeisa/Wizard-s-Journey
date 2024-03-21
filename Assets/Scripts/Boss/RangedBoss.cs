using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedBoss : MonoBehaviour
{
    [Header ("Attack")]
    [SerializeField] private float attackCD;
    [SerializeField] private float range;
    [SerializeField] private int damage;

    [Header ("Range Attack")]
    [SerializeField] private Transform[] firepoint;
    [SerializeField] private GameObject[] slime;

    [Header ("Collider")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;

    [Header ("Player")]
    [SerializeField] private LayerMask playerLayer;
    private float cdTimer = Mathf.Infinity;
    private Animator anim;

    private void Awake() {
        anim = GetComponent<Animator>();
    }
    private void Update() {
        cdTimer += Time.deltaTime;

        if ((PlayerInRange())){
            if (cdTimer >= attackCD){
                cdTimer = 0;
                anim.SetTrigger("rangedAttack");
            }
        }
    }

    private void RangedAttack() {
    cdTimer = 0;
    for (int i = 0; i < firepoint.Length; i++) {
        Vector2 direction = (firepoint[i].position - transform.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        slime[findSlime()].transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        slime[findSlime()].transform.position = firepoint[i].position;
        slime[findSlime()].GetComponent<BossProjectile>().ActivateProjectile();
    }
}

    private int findSlime(){
        for (int i = 0; i < slime.Length; i++)
        {
            if (!slime[i].activeInHierarchy){
                return i;
            }
        }
        return 0;
    }

    private bool PlayerInRange(){
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y * range, boxCollider.bounds.size.z)
            , 0, Vector2.left, 0, playerLayer);

        return hit.collider != null;
    }
    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, 
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y * range, boxCollider.bounds.size.z));
    }
}

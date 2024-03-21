using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedBoss2 : MonoBehaviour
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

    private void RangedAttack2() {
    cdTimer = 0;
    
    float angleStep = 360f / firepoint.Length;

        for (int i = 0; i < firepoint.Length; i++) {
            float angle = i * angleStep;

            float x = Mathf.Cos(angle * Mathf.Deg2Rad) * range;
            float y = Mathf.Sin(angle * Mathf.Deg2Rad) * range;
            Vector3 newPosition = transform.position + new Vector3(x, y, 0);

            Vector2 direction = (newPosition - transform.position).normalized;
            float rotationAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            slime[findSlime()].transform.rotation = Quaternion.AngleAxis(rotationAngle, Vector3.forward);

            slime[findSlime()].transform.position = newPosition;
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

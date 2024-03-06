using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardAttack : MonoBehaviour
{
    [SerializeField] private float fireballCd;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireballs;
    private Animator anim;
    private PlayerMovement wizardMov;
    private float cdTimer = Mathf.Infinity;
    private bool canAttack;

    private void Awake() {
        anim = GetComponent<Animator>();
        wizardMov = GetComponent<PlayerMovement>();
    }

    private void Update() {
        if (canAttack && Input.GetMouseButton(0) && cdTimer > fireballCd && wizardMov.canAttack()){
            Attack();
        }
        cdTimer += Time.deltaTime;
    }
    private void Attack(){
        anim.SetTrigger("attack");
        cdTimer = 0;

        fireballs[findFireball()].transform.position = firePoint.position;
        fireballs[findFireball()].GetComponent<Fireball>().setDirection(Mathf.Sign(transform.localScale.x));
    }

    private int findFireball(){
        for (int i = 0; i < fireballs.Length; i++){
            if (!fireballs[i].activeInHierarchy){
                return i;
            }
        }
        return 0;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("FireOrb")) {
            canAttack = true;
            collision.gameObject.SetActive(false);
        }
    }
}

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

    private void Awake() {
        anim = GetComponent<Animator>();
        wizardMov = GetComponent<PlayerMovement>();
    }

    private void Update() {
        if (Input.GetMouseButton(0) && cdTimer > fireballCd && wizardMov.canAttack()){
            Attack();
        }
        cdTimer += Time.deltaTime;
    }
    private void Attack(){
        anim.SetTrigger("attack");
        cdTimer = 0;

        fireballs[0].transform.position = firePoint.position;
        fireballs[0].GetComponent<Fireball>().setDirection(Mathf.Sign(transform.localScale.x));
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardAttack : MonoBehaviour
{
    [SerializeField] private float fireballCd;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireballs;
    [SerializeField] private AudioClip fireballSound;
    [SerializeField] private AudioClip fireorbSound;
    private Animator anim;
    private PlayerMovement wizardMov;
    private float cdTimer = Mathf.Infinity;
    private bool canAttack;
    // private int numAttack;

    private void Awake() {
        anim = GetComponent<Animator>();
        wizardMov = GetComponent<PlayerMovement>();
        canAttack = GameManager.instance.canAttack;
    }
    // private void Start() {
    //     numAttack = 0;
    //     Debug.Log(PlayerPrefs.GetInt("numAttack"));
    // }

    private void Update() {
        if (canAttack && Input.GetMouseButton(0) && cdTimer > fireballCd && wizardMov.canAttack()){
            Attack();
        }
        cdTimer += Time.deltaTime;
    }
    private void Attack(){
        SoundManager.instance.PlaySound(fireballSound);
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
            SoundManager.instance.PlaySound(fireorbSound);
            canAttack = true;
            GameManager.instance.canAttack = canAttack;
            PlayerPrefs.SetInt("canAttack", 1);
            // numAttack = 1;
            // PlayerPrefs.SetInt("numAttack", 1);
            // Debug.Log(PlayerPrefs.GetInt("numAttack"));
            collision.gameObject.SetActive(false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDripstone : MonoBehaviour
{
    [SerializeField] private float attackCd;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] stones;
    private float cdTimer;
    private bool canStun;
    private void Awake() {
        
    }
    private void Update() {
        canStun = GameManager.instance.canStun;
        cdTimer += Time.deltaTime;

        if (canStun && cdTimer >= attackCd){
            Attack();
        }
    }
    private void Attack(){
        cdTimer = 0;

        stones[findStone()].transform.position = firePoint.position;
        stones[findStone()].GetComponent<DripProjectile>().ActivateProjectile();
    }

    private int findStone(){
        for (int i = 0; i < stones.Length; i++)
        {
            if(!stones[i].activeInHierarchy){
                return i;
            }
        }
        return 0;
    }
}

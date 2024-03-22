using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossImmune : MonoBehaviour
{
    public bool isImmune = true;
    public bool isFinish;
    private Animator anim;
    private void Awake() {
        anim = GetComponent<Animator>();
        BossImmunity();
    }
    private void BossImmunity(){
        Physics2D.IgnoreLayerCollision(10, 11, true);
    }

    public IEnumerator BossVulnerable(){
        Physics2D.IgnoreLayerCollision(10, 11, false);
        anim.SetTrigger("stun");
        yield return new WaitForSeconds(4);
        Physics2D.IgnoreLayerCollision(10, 11, true);
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.CompareTag("Drip")){
            StartCoroutine(BossVulnerable());
        }
    }
}

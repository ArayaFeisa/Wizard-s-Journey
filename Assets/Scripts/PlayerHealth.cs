using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header ("Health")]
    public HealthBar healthBar;
    [SerializeField] private float startingHealth;
    private float currentHealth;
    private bool dead;
    private Animator anim;

    [Header ("iframes")]
    [SerializeField] private float immuneDuration;
    [SerializeField] private int numberOfflashes;
    private SpriteRenderer spriteRend;

    private void Awake() {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
        healthBar.setMaxHealth(startingHealth);
    }

    public void takeDamage(float _damage){
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);
        healthBar.setHealth(currentHealth);
        if (currentHealth > 0){
            StartCoroutine(immune());
        } else {
            if (!dead){
                
                anim.SetTrigger("die");

                //player
                if(GetComponent<PlayerMovement>() != null){
                    GetComponent<PlayerMovement>().enabled = false;
                }

                //enemy
                if(GetComponentInParent<EnemyPatrol>() != null){
                    GetComponentInParent<EnemyPatrol>().enabled = false;
                }
                if(GetComponent<MeleeEnemy>() != null){
                    GetComponent<MeleeEnemy>().enabled = false;
                }

                dead = true;
            }
        }
    }
    private IEnumerator immune(){
        Physics2D.IgnoreLayerCollision(7, 10, true);
        for (int i = 0; i < numberOfflashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(immuneDuration / (numberOfflashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(immuneDuration / (numberOfflashes * 2));
        }

        Physics2D.IgnoreLayerCollision(7, 10, false);
    }

    // private void Update() {
    //     if (Input.GetKeyDown(KeyCode.Z)){
    //         takeDamage(20);
    //     }
    // }
}

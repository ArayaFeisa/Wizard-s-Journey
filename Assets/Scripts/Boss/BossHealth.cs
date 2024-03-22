using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    [Header ("Health")]
    public HealthBar healthBar;
    [SerializeField] private float startingHealth;
    public float currentHealth;
    private bool dead;
    private Animator anim;

    [Header ("iframes")]
    [SerializeField] private float immuneDuration;
    [SerializeField] private int numberOfflashes;
    private SpriteRenderer spriteRend;

    [Header ("Components")]
    [SerializeField] private Behaviour[] components;

    [Header ("Dead Sound")]
    [SerializeField] private AudioClip deadSound;
    [SerializeField] private AudioClip hurtSound;

    public bool respawnDripStone;
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
            anim.SetTrigger("hurt");
            SoundManager.instance.PlaySound(hurtSound);
        } else {
            if (!dead){
                // anim.SetBool("grounded", true);
                anim.SetTrigger("die");
                Die();
                dead = true;
                SoundManager.instance.PlaySound(deadSound);
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

    private void AddHealth(float _value){
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }

    public void Respawn(){
        respawnDripStone = true;
        dead = false;
        AddHealth(startingHealth);
        healthBar.setMaxHealth(startingHealth);
        anim.ResetTrigger("die");
        anim.Play("idle");
        StartCoroutine(immune());

        //aktifin semua komponen dikelasnya
        foreach (Behaviour component in components) {
            component.enabled = true;
        }
    }

    public void Die(){

        //nonaktif semua komponen dikelasnya
        foreach (Behaviour component in components) {
            component.enabled = false;
        }
    }

    private void deactivate(){
        gameObject.SetActive(false);
    }
}

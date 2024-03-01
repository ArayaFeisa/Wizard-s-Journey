using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public HealthBar healthBar;
    [SerializeField] private float startingHealth;
    private float currentHealth;
    private bool dead;

    private void Awake() {
        currentHealth = startingHealth;
        healthBar.setMaxHealth(startingHealth);
    }

    public void takeDamage(float _damage){
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);
        healthBar.setHealth(currentHealth);
        if (currentHealth > 0){
            //anim
        } else {
            if (!dead){
                //anim
                GetComponent<PlayerMovement>().enabled = false;
                dead = true;
            }
        }
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Z)){
            takeDamage(20);
        }
    }
}

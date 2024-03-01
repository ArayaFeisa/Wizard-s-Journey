using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health2 : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    private bool dead;

    private void Awake() {
        currentHealth = startingHealth;
    }
    public void TakeDamage(float _damage){
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if (currentHealth > 0){
            
        }
        else {
            if (!dead){
                GetComponent<PlayerMovement>().enabled = false;
                dead = true;
            }
        }
    }
    private void Update() {
        if(Input.GetKeyDown(KeyCode.Z)){
            TakeDamage(1);
        }
    }
}

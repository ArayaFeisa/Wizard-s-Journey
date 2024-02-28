using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardAttack : MonoBehaviour
{
    private Animator anim;
    private PlayerMovement wizardMov;
    [SerializeField] private float fireballCd;

    private void Awake() {
        anim = GetComponent<Animator>();
        wizardMov = GetComponent<PlayerMovement>();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lvl2PortalScript : MonoBehaviour
{
    public bool lvl2Unlocked;
    private Animator anim;
    private bool inPortal;
    void Start()
    {
        anim = GetComponentInParent<Animator>();
        anim.enabled = false;
    }
    private void Awake() {
        lvl2Unlocked = GameManager.instance.lvl2unlocked;
    }

    void Update()
    {
            if (inPortal && Input.GetKeyDown(KeyCode.Q) && lvl2Unlocked)
            {
                unlockStage2();
            }
    }

    private void unlockStage2(){
        SceneManager.LoadScene("Stage 2");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        inPortal = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        inPortal = false;
    }
}

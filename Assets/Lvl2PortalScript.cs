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
        lvl2Unlocked = false;
        anim.enabled = false;
    }

    void Update()
    {
        if(lvl2Unlocked)
        {
            anim.enabled = true;
        }
        if (inPortal && Input.GetKeyDown(KeyCode.Q))
        {
            unlockStage2();
        }
    }

    private void unlockStage2(){
        SceneManager.LoadScene(2);
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

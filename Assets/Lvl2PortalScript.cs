using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lvl2PortalScript : MonoBehaviour
{
    // Start is called before the first frame update
    public bool lvl2Unlocked;
    private Animator anim;
    void Start()
    {
        anim = GetComponentInParent<Animator>();
        lvl2Unlocked = false;
        anim.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(lvl2Unlocked)
        {
            anim.enabled = true;
        }
    }
}

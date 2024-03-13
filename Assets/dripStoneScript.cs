using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dripStoneScript : MonoBehaviour
{
    public dripstoneTriggerScript trig;
    public Rigidbody2D gravity;
    // Start is called before the first frame update
    void Start()
    {
        gravity.gravityScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(trig.isIn) gravity.gravityScale = 1;
    }
}

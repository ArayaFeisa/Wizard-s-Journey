using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dripstoneTriggerScript : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isIn;
    void Start()
    {
        isIn = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isIn = true;
        }
    }
}

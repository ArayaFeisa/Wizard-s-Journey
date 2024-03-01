using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leverScriptTest : MonoBehaviour
{
    public PlayerMovement enter;
    public doorScript door;
    // Start is called before the first frame update
    void Start()
    {
        enter.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("in");
        door.interactAble = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("out");
        door.interactAble = false;
    }
}

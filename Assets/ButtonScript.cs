using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    // Start is called before the first frame update
    public ButtonDoorScript door;
    private bool pressed = false;
    private bool pressing = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position == new Vector3(transform.position.x, -2.71f))
        {
            door.buttonDown = true;
        }
        else
        {
            door.buttonDown = false;
        }
        if(!pressing)StartCoroutine(buttonPush());
    }

    IEnumerator buttonPush()
    {
        pressing = true;
        if (pressed)
        {
            if (transform.position.y >= -2.701f)
            {
                yield return new WaitForSeconds(0.02f);
                transform.position = new Vector2(transform.position.x, transform.position.y - 0.01f);
            }
        } 
        else
        {
            if (transform.position.y <= -2.501f)
            {
                yield return new WaitForSeconds(0.02f);
                transform.position = new Vector2(transform.position.x, transform.position.y + 0.01f);
            }
        }
        pressing = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        pressed = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        pressed = false;
    }
}

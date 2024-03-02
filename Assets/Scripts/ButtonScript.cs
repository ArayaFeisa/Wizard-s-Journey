using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    // Start is called before the first frame update
    public ButtonDoorScript door;
    private bool pressed = false;
    private bool pressing = false;
    private float startPos;
    void Start()
    {
        startPos = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y <= startPos - 0.04f)
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
            if (transform.position.y > startPos - 0.04f)
            {
                yield return new WaitForSeconds(0.01f);
                transform.position = new Vector2(transform.position.x, transform.position.y - 0.001f);
            }
        } 
        else
        {
            if (transform.position.y < startPos)
            {
                yield return new WaitForSeconds(0.01f);
                transform.position = new Vector2(transform.position.x, transform.position.y + 0.001f);
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

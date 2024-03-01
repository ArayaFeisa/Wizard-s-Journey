using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonDoorScript : MonoBehaviour
{
    // Start is called before the first frame update
    private bool open = false;
    private bool opening = false;
    public bool buttonDown = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (buttonDown && !open && !opening)
        {
            StartCoroutine(Open());
        }
        if (!buttonDown && open &&!opening)
        {
            StartCoroutine(Close());
        }
    }

    private IEnumerator Open()
    {
        opening = true;
        while (transform.rotation != Quaternion.Euler(new Vector3(90, 0, 0)) && buttonDown)
        {
            yield return new WaitForSeconds(0.02f);
            transform.rotation = Quaternion.Euler(new Vector3(transform.eulerAngles.x + 1f, 0, 0));
            transform.position = new Vector2(transform.position.x, transform.position.y - 0.016f);
        }
        yield return new WaitForSeconds(0.1f);
        opening = false;
        open = true;
    }

    private IEnumerator Close()
    {
        opening = true;
        while (transform.rotation != Quaternion.Euler(new Vector3(0, 0, 0)) && !buttonDown)
        {
            yield return new WaitForSeconds(0.02f);
            transform.rotation = Quaternion.Euler(new Vector3(transform.eulerAngles.x - 1f, 0, 0));
            transform.position = new Vector2(transform.position.x, transform.position.y + 0.016f);
        }
        yield return new WaitForSeconds(0.1f);
        opening = false;
        open = false;
    }
}

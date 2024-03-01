using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorScript : MonoBehaviour
{
    // Start is called before the first frame update
    private bool open = false;
    private bool opening = false;
    public bool interactAble = false;
    public BoxCollider2D lever;
    public Transform head;
    private float count;
    void Start()
    {
        lever = GetComponent<BoxCollider2D>();
    }
    private
    // Update is called once per frame
    void Update()
    {
        if(opening)
        {
            return;
        }
        if (interactAble) interact();
        
        
    }
    private IEnumerator Open()
    {
        opening = true;
        yield return new WaitForSeconds(0.1f);
        while(transform.rotation != Quaternion.Euler(new Vector3(90, 0, 0))) transform.rotation = Quaternion.Euler(new Vector3(transform.eulerAngles.x + 1f, 0, 0));
        yield return new WaitForSeconds(0.1f);
        opening = false;
        open = true;
    }

    private IEnumerator Close()
    {
        opening = true;
        yield return new WaitForSeconds(0.1f);
        while (transform.rotation != Quaternion.Euler(new Vector3(0, 0, 0))) transform.rotation = Quaternion.Euler(new Vector3(transform.eulerAngles.x - 1f, 0, 0));
        yield return new WaitForSeconds(0.1f);
        opening = false;
        open = false;
    }

    void interact()
    {
        if(Input.GetKeyDown(KeyCode.Q) && !open && !opening) 
        {
            Debug.Log("open");
            StartCoroutine(Open());
            head.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 30));
        }
        if(Input.GetKeyDown(KeyCode.Q) && open && !opening)
        {
            Debug.Log("close");
            StartCoroutine(Close());
            head.transform.rotation = Quaternion.Euler(new Vector3(0, 0, -30));
        }
    }
}

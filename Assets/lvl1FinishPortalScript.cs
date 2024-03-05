using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lvl1FinishPortalScript : MonoBehaviour
{
    public GameObject lvl1;
    public GameObject lvlSelect;
    public Transform playerPos;
    private bool inPortal;
    void Start()
    {
        inPortal = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (inPortal && Input.GetKeyDown(KeyCode.Q))
        {
            lvl1.SetActive(false);
            lvlSelect.SetActive(true);
            playerPos.transform.position = new Vector2(-21, -3);
        }
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

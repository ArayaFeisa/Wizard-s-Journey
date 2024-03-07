using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class lvl1FinishPortalScript : MonoBehaviour
{
    public Transform playerPos;
    public bool lvl2;
    private bool inPortal;
    void Start()
    {
        inPortal = false;
    }
    private void Awake() {
    }
    // Update is called once per frame
    void Update()
    {
        if (inPortal && Input.GetKeyDown(KeyCode.Q))
        {
            reenter();
        }
    }

    private void reenter(){
        lvl2 = true;
        GameManager.instance.lvl2unlocked = lvl2;
        SceneManager.LoadScene("Selector");
        playerPos.transform.position = new Vector2(-19, -3);
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

using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class Lvl1PortalScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject lvl1;
    public GameObject lvlSelect;
    public Transform playerPos;
    public CinemachineVirtualCamera vcam;
    private bool inPortal;
    void Start()
    {
        inPortal = false;
        lvl1.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (inPortal && Input.GetKeyDown(KeyCode.Q))
        {
            StartCoroutine(enterLvl1());
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

    private IEnumerator enterLvl1()
    {
        for (int i = 0; i < 66; i++)
        {
            vcam.m_Lens.OrthographicSize = vcam.m_Lens.OrthographicSize - 0.1f;
            yield return new WaitForSeconds(0.015f);
        }
        lvl1.SetActive(true);
        playerPos.transform.position = new Vector2(-21, -3);
        for (int i = 0; i < 66; i++)
        {
            vcam.m_Lens.OrthographicSize = vcam.m_Lens.OrthographicSize + 0.1f;
            yield return new WaitForSeconds(0.015f);
        }
        lvlSelect.SetActive(false);
    }
}

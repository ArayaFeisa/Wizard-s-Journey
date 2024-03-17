using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class lvl3PortalScript : MonoBehaviour
{
    public bool lvl3Unlocked;
    private Animator anim;
    public CinemachineVirtualCamera vcam;
    private bool inPortal;
    public PlayerMovement shardReset;
    // void Start()
    // {
    //     anim = GetComponent<Animator>();
    //     anim.enabled = false;
    // }
    private void Awake()
    {
        StartCoroutine(zoomOut());
        lvl3Unlocked = GameManager.instance.lvl3unlocked;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (lvl3Unlocked)
        {
            anim.SetTrigger("openLevel3");
        }
        if (inPortal && Input.GetKeyDown(KeyCode.Q) && lvl3Unlocked)
        {
            StartCoroutine(unlockStage3());
        }
    }

    private IEnumerator unlockStage3()
    {
        for (int i = 0; i < 66; i++)
        {
            vcam.m_Lens.OrthographicSize = vcam.m_Lens.OrthographicSize - 0.1f;
            yield return new WaitForSeconds(0.015f);
        }
        shardReset.lvl3ShardCount = 0;
        SceneManager.LoadScene("Stage 3");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        inPortal = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        inPortal = false;
    }

    private IEnumerator zoomOut()
    {
        vcam.m_Lens.OrthographicSize = 0.1f;
        for (int i = 0; i < 66; i++)
        {
            vcam.m_Lens.OrthographicSize = vcam.m_Lens.OrthographicSize + 0.1f;
            yield return new WaitForSeconds(0.015f);
        }
    }
}

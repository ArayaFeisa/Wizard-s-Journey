using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lvl2PortalScript : MonoBehaviour
{
    public bool lvl2Unlocked;
    private Animator anim;
    public CinemachineVirtualCamera vcam;
    private bool inPortal;
    public PlayerMovement shardReset;
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.enabled = false;
    }
    private void Awake() {
        lvl2Unlocked = GameManager.instance.lvl2unlocked;
    }

    void Update()
    {
        if (lvl2Unlocked){
            anim.SetTrigger("openlevel2");
        }
            if (inPortal && Input.GetKeyDown(KeyCode.Q) && lvl2Unlocked)
            {
                StartCoroutine(unlockStage2());    
            }
    }

    private IEnumerator unlockStage2(){
        for (int i = 0; i < 66; i++)
        {
            vcam.m_Lens.OrthographicSize = vcam.m_Lens.OrthographicSize - 0.1f;
            yield return new WaitForSeconds(0.015f);
        }
        shardReset.lvl1ShardCount = 0;
        SceneManager.LoadScene("Stage 2");
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossFinishPortal : MonoBehaviour
{
    [SerializeField] private AudioClip finishSound;
    private bool inPortal;
    private bool isFinish = false;

    private void Start() {
        inPortal = false;
    }

    private void Update() {
        isFinish = GameManager.instance.isFinish;
        if (inPortal && isFinish && Input.GetKeyDown(KeyCode.Q)){
            SoundManager.instance.PlaySound(finishSound);
            SceneManager.LoadScene("Selector");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")){
            inPortal = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")){
            inPortal = false;
        }
    }
}

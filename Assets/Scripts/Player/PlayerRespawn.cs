using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    // [SerializeField] private AudioClip checkpointSound;
    private Transform currentCheckpoint;
    private Health playerHealth;
    private int attempts = 2;
    private bool checkpointed;
    private UIManager uiManager;

    private void Awake() {
        playerHealth = GetComponent<Health>();
        checkpointed = false;
        uiManager = FindObjectOfType<UIManager>();
        DontDestroyOnLoad(this.gameObject);

    }

    public void CheckRespawn(){
        if (!checkpointed) {
            uiManager.GameOver();
            // ResetGame();
        } else {
            if (attempts > 0){
            attempts--;

            transform.position = currentCheckpoint.position; // move to cp
            playerHealth.Respawn();

            //camera
            Camera.main.GetComponent<CameraFollowPlayer>();
            } else {
                // ResetGame();
                uiManager.GameOver();
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.transform.tag == "Checkpoint"){
            currentCheckpoint = collision.transform;
            collision.GetComponent<Collider2D>().enabled = false;
            checkpointed = true;
        }
    }

    // private void ResetGame()
    // {
    //     Collider2D checkpoints = GameObject.FindGameObjectWithTag("Checkpoint").GetComponent<Collider2D>();
    //     checkpoints.enabled = true;

    //     attempts = 2;
    //     checkpointed = false;
    //     transform.position = new Vector3(-21, -3, 0);
    //     playerHealth.Respawn();
    //     Debug.Log("Game Reset");
    // }
}

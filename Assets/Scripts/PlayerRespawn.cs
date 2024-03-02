using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    // [SerializeField] private AudioClip checkpointSound;
    private Transform currentCheckpoint;
    private Health playerHealth;
    private int attempts = 3;

    private void Awake() {
        playerHealth = GetComponent<Health>();
    }

    public void CheckRespawn(){
        if (attempts > 0){
            attempts--;

            transform.position = currentCheckpoint.position; // move to cp
            playerHealth.Respawn();

        //camera
            Camera.main.GetComponent<CameraFollowPlayer>();
        } else {
            ResetGame();
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.transform.tag == "Checkpoint"){
            currentCheckpoint = collision.transform;
            collision.GetComponent<Collider2D>().enabled = false;
        }
    }

    private void ResetGame()
    {
        Collider2D checkpoints = GameObject.FindGameObjectWithTag("Checkpoint").GetComponent<Collider2D>();
        checkpoints.enabled = true;

        attempts = 3;
        transform.position = new Vector3(-14, 1, 0);
        playerHealth.Respawn();
        Debug.Log("Game Reset");
    }
}

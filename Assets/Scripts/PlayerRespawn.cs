using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    // [SerializeField] private AudioClip checkpointSound;
    private Transform currentCheckpoint;
    private Health playerHealth;

    private void Awake() {
        playerHealth = GetComponent<Health>();
    }

    public void CheckRespawn(){
        transform.position = currentCheckpoint.position; // move to cp
        playerHealth.Respawn();

        //camera
        Camera.main.GetComponent<CameraFollowPlayer>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.transform.tag == "Checkpoint"){
            currentCheckpoint = collision.transform;
            collision.GetComponent<Collider2D>().enabled = false;
        }
    }
}

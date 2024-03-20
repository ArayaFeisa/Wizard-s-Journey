using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerRespawn : MonoBehaviour
{
    // [SerializeField] private AudioClip checkpointSound;
    private Transform currentCheckpoint;
    private Health playerHealth;
    private int attempts;
    private bool checkpointed;
    private UIManager uiManager;
    private float fallHold = -50f;
    public UnityEngine.UI.Text attemptCounter;

    private void Update() {
        if (transform.position.y < fallHold){
            CheckRespawn();
        }
        if (SceneManager.GetActiveScene().name.Equals("Stage 1")){
            attemptCounter.text = (attempts+1).ToString();
        }
        if (SceneManager.GetActiveScene().name.Equals("Stage 2")){
            attemptCounter.text = (attempts+1).ToString();
        }
        if (SceneManager.GetActiveScene().name.Equals("Stage 3")){
            attemptCounter.text = (attempts+1).ToString();
        }
    }

    private void Awake() {
        attempts = GameManager.instance.attempts;
        playerHealth = GetComponent<Health>();
        checkpointed = false;
        uiManager = FindObjectOfType<UIManager>();
        // DontDestroyOnLoad(this.gameObject);
    }

    public void CheckRespawn(){
        if (!checkpointed) {
            playerHealth.Die();
            uiManager.GameOver();

            transform.position = new Vector3(transform.position.x, fallHold, transform.position.z);
            transform.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GetComponent<SpriteRenderer>().enabled = false;
            // ResetGame();
        } else {
            if (attempts > 0){
            attempts--;
            GameManager.instance.attempts = attempts;
            PlayerPrefs.SetInt("attempts", attempts);
            transform.position = currentCheckpoint.position; // move to cp
            playerHealth.Respawn();

            //camera
            Camera.main.GetComponent<CameraFollowPlayer>();
            } else {
                // ResetGame();
                playerHealth.Die();
                uiManager.GameOver();

                transform.position = new Vector3(transform.position.x, fallHold, transform.position.z);
                transform.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                GetComponent<SpriteRenderer>().enabled = false;
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

    public void Restart(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

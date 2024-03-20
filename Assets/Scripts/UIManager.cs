using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverScreen;
    private PlayerRespawn pRespawn;

    private void Awake() {
        gameOverScreen.SetActive(false);
    }
    public void GameOver(){
        gameOverScreen.SetActive(true);
    }

    public void Restart(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        // pRespawn.Restart();
        GameManager.instance.attempts = 2;
    }

    public void MainMenu(){
        SceneManager.LoadScene(0);
    }

    public void Quit(){
        // UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
}

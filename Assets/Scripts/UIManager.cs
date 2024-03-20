using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverScreen;

    private void Awake() {
        gameOverScreen.SetActive(false);
    }
    public void GameOver(){
        gameOverScreen.SetActive(true);
    }

    public void Restart(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        GameManager.instance.attempts = 2;
        GameManager.instance.lvl1ShardCount = 0;
        GameManager.instance.lvl2ShardCount = 0;
        GameManager.instance.lvl3ShardCount = 0;
    }

    public void MainMenu(){
        SceneManager.LoadScene(0);
    }

    public void Quit(){
        // UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
}

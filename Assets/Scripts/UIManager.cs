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
        if (SceneManager.GetActiveScene().name.Equals("Stage 1")) GameManager.instance.lvl1ShardCount = 0;
        else if (SceneManager.GetActiveScene().name.Equals("Stage 2")) GameManager.instance.lvl2ShardCount = 0;
        else if (SceneManager.GetActiveScene().name.Equals("Stage 3")) GameManager.instance.lvl3ShardCount = 0;
        SceneManager.LoadScene("Selector");
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

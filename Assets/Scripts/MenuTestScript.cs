using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuTestScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("Selector");
    }

    public void Load()
    {
        SceneManager.LoadScene("Selector");
    }

    public void Exit()
    {
        Application.Quit();
    }
}

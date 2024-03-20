using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveTutorEnableScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject moveTutor;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.newSave)
        {
            StartCoroutine(movementTutorial());
        }
    }
    private IEnumerator movementTutorial()
    {
        GameManager.instance.newSave = false;
        moveTutor.SetActive(true);
        yield return new WaitUntil(wait);
        moveTutor.SetActive(false);
    }

    private bool wait()
    {
        return Input.GetKeyDown(KeyCode.Backspace);
    }
}

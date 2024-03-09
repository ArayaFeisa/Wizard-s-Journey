using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool canAttack;
    public bool canDoubleJump;
    public bool canDash;
    public bool lvl2unlocked;
    private void Awake() {
        if(instance != null) return;
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
    public void Test(){

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool canAttack;
    public bool canDoubleJump;
    public bool canDash;
    public bool canSummonPushable;
    public bool lvl2unlocked;
    public bool lvl3unlocked;
    public int lvl1ShardCount;
    public int lvl2ShardCount;
    public int lvl3ShardCount;
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

    public void loadData()
    {
        canAttack = (PlayerPrefs.GetInt("canAttack") == 1) ? true : canAttack;
        canDoubleJump = (PlayerPrefs.GetInt("canDoubleJump") == 1) ? true : canDoubleJump;
        canDash = (PlayerPrefs.GetInt("canDash") == 1) ? true : canDash;
        canSummonPushable = (PlayerPrefs.GetInt("canSummonPushable") == 1) ? true : canSummonPushable;
        lvl2unlocked = (PlayerPrefs.GetInt("lvl2unlocked") == 1) ? true : lvl2unlocked;
        lvl3unlocked = (PlayerPrefs.GetInt("lvl3unlocked") == 1) ? true : lvl3unlocked;
        lvl1ShardCount = (PlayerPrefs.GetInt("lvl1ShardCount"));
        lvl2ShardCount = (PlayerPrefs.GetInt("lvl2ShardCount"));
        lvl3ShardCount = (PlayerPrefs.GetInt("lvl3ShardCount"));
    }
}

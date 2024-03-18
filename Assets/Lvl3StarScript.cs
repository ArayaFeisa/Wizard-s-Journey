using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lvl3StarScript : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayerMovement player;
    [SerializeField] private int shardReq;
    public Color newColor;
    private SpriteRenderer rend;
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.lvl3ShardCount >= shardReq)
        {
            rend.color = newColor;
        }
    }
}

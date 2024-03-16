using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particleTesting : MonoBehaviour
{
    // Start is called before the first frame update
    private ParticleSystem ps;
    private float ori;
    private bool move;
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        ori = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        var shape = ps.shape;
        shape.rotation = new Vector3(0, 0, shape.rotation.z + (720 * Time.deltaTime));
        if(transform.position.y >= ori)
        {
            move = true;
        }
        if(transform.position.y <= ori - 0.1f)
        {
            move = false;
        }
        if(!move)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + (0.1f * Time.deltaTime));
        }
        if (move)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - (0.1f * Time.deltaTime));
        }
    }
}

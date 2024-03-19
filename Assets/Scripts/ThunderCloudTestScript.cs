using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderCloudTestScript : MonoBehaviour
{
    // Start is called before the first frame update
    public BoxCollider2D thunHitBox;
    public SpriteRenderer thunSprite;
    private BoxCollider2D detect;
    private bool striking;
    void Start()
    {
        detect = GetComponent<BoxCollider2D>();
        striking = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player"  && !striking)
        {
            StartCoroutine(strike());
        }
    }

    private IEnumerator strike()
    {
        striking = true;
        yield return new WaitForSeconds(0.25f);
        thunHitBox.enabled = true;
        thunSprite.enabled = true;
        yield return new WaitForSeconds(0.5f);
        thunHitBox.enabled = false;
        thunSprite.enabled = false;
        striking = false;
    }
}

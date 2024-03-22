

using System.Collections;
using UnityEngine;

public class ButtonDrip : MonoBehaviour
{
    private bool isEnabled = true;
    private BoxCollider2D boxCollider;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && isEnabled)
        {
            Delay();
            boxCollider.enabled = false;
            isEnabled = false;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        boxCollider.enabled = true;
        isEnabled = true;
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(3);
    }
}

using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    // Start is called before the first frame update

    public CinemachineVirtualCamera vcam;
    void Start()
    {

    }

    private void Awake()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();
        StartCoroutine(zoomOut());
    }
    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator zoomOut()
    {
        vcam.m_Lens.OrthographicSize = 0;
        for (int i = 0; i < 66; i++)
        {
            vcam.m_Lens.OrthographicSize = vcam.m_Lens.OrthographicSize + 0.1f;
            yield return new WaitForSeconds(0.015f);
        }
    }
}

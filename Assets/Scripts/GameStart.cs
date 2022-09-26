using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameStart : MonoBehaviour
{
    public CinemachineVirtualCamera cam;
    private float velocity = 0f;
    private bool isDone = false;

    void Start()
    {
        GameManager.Instance.isUiOpen = true;
    }

    void Update()
    {
        if (!isDone)
        {
            float newZoom = Mathf.SmoothDamp(cam.m_Lens.OrthographicSize, 5, ref velocity, 1f);
            cam.m_Lens.OrthographicSize = newZoom;
            if (cam.m_Lens.OrthographicSize <= 5.3)
            {
                isDone = true;
                GameManager.Instance.isUiOpen = false;
            }
        }
        
    }
}

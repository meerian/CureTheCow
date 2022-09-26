using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausebuttonScript : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !GameManager.Instance.isUiOpen)
        {
            UIManager.Instance.PauseGame();
        }
    }
}

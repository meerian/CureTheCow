using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private int curActive = 0;
    private int curSelected = 0;

    public GameObject[] selections;
    public Animator transitionAnim;
    public CanvasGroup cg;

    // Update is called once per frame
    void Update()
    {
    if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Q))
        {
            curSelected = 0;
            ConfirmSelect();
            return;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            ConfirmSelect();
            return;
        }

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveParser("right");
        }

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveParser("left");
        }   
    }

    private void ConfirmSelect()
    {
        switch (curSelected)
        {
            case 0:
                UIManager.Instance.UnpauseGame();
                UpdateIndicator();
                return;
            case 1:
                AudioManager.Instance.ToggleMute();
                return;
            case 2:
                Time.timeScale = 1;
                StartCoroutine("LoadLevel", 0);
                return;
        }
    }

    private void MoveParser(string dir)
    {
        AudioManager.Instance.Play("mouseover");
        if (dir == "right")
        {
            if (curSelected == 2)
            {
                return;
            }
            else
            {
                curSelected++;
            }
        }

        if (dir == "left")
        {
            if (curSelected == 0)
            {
                return;
            }
            else
            {
                curSelected--;
            }
        }
        UpdateIndicator();
    }

    private void UpdateIndicator()
    {
        selections[curActive].SetActive(false);
        selections[curSelected].SetActive(true);
        curActive = curSelected;
    }

    IEnumerator LoadLevel(int levelindex)
    {
        cg.alpha = 0;
        transitionAnim.SetTrigger("Start");

        yield return new WaitForSecondsRealtime(1f);

        SceneManager.LoadScene(levelindex);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private int curSelected = 0;
    private int curActive = 0;
    private bool uiOpen = false;
    private bool canClose = true;
    private bool canMove = false;

    public CanvasGroup cg;
    public Animator anim;
    public Animator transitionAnim;
    public GameObject[] indicators;
    public GameObject[] panels;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        StartCoroutine("StartCountdown");
    }

    // Update is called once per frame
    void Update()
    {
        if (!canMove)
        {
            return;
        }
        if (Input.anyKey && uiOpen)
        {
            ConfirmSelection();
            return;
        }

        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Return))
        {
            ConfirmSelection();
        }

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            MoveParser("up");
        }

        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            MoveParser("down");
        }
    }

    private void MoveParser(string dir)
    {
        AudioManager.Instance.Play("mouseover");
        if (!uiOpen)
        {
            if (dir == "up")
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
            if (dir == "down")
            {
                if (curSelected == 3)
                {
                    return;
                }
                else
                {
                    curSelected++;
                }
            }
            UpdateIndicator();
        }
    }

    private void ConfirmSelection()
    {
        if (curSelected == 0)
        {
            AudioManager.Instance.Play("mousedown");
            StartGame();
            return;
        }

        if (curSelected == 3)
        {
            AudioManager.Instance.Play("mousedown");
            Application.Quit();
            return;
        }

        if (canClose)
        {
            AudioManager.Instance.Play("mousedown");
            if (uiOpen)
            {
                panels[curSelected].SetActive(false);
            }
            else
            {
                panels[curSelected].SetActive(true);
                canClose = false;
                StartCoroutine("SetTimeout");
            }
            uiOpen = !uiOpen;
        }
    }

    public void StartGame()
    {
        StartCoroutine("LoadLevel", 1);
    }

    IEnumerator LoadLevel(int levelindex)
    {
        cg.alpha = 0;
        transitionAnim.SetTrigger("Start");

        yield return new WaitForSecondsRealtime(1f);

        SceneManager.LoadScene(levelindex);
    }

    private void UpdateIndicator()
    {
        indicators[curActive].SetActive(false);
        indicators[curSelected].SetActive(true);
        curActive = curSelected;
    }

    IEnumerator SetTimeout()
    {
        yield return new WaitForSeconds(0.3f);
        canClose = true;
    }

    IEnumerator StartCountdown()
    {
        yield return new WaitForSeconds(1f);
        anim.SetBool("start", true);
        yield return new WaitForSeconds(1f);
        canMove = true;
    }
}

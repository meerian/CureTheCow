using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantingManager : MonoBehaviour
{
    // 0 = corn, 1 = beet, 2 = exit
    public int curSelected = 0;
    private int curActive = 0;

    public GameObject[] indicators;
    public GameObject[] plots;

    private void Start()
    {
        indicators[curSelected].SetActive(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Q))
        {
            curSelected = 2;
            ConfirmSelection();
            return;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            ConfirmSelection();
            return;
        }

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveParser("horizontal");
        }

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            MoveParser("vertical");
        }
    }

    private void MoveParser(string dir)
    {
        AudioManager.Instance.Play("mouseover");
        if (dir == "horizontal")
        {
            switch (curSelected)
            {
                case 0:
                    curSelected = 1;
                    break;
                case 1:
                    curSelected = 0;
                    break;
            }
        }

        if (dir == "vertical")
        {
            switch (curSelected)
            {
                case 0:
                    curSelected = 2;
                    break;
                case 1:
                    curSelected = 2;
                    break;
                case 2:
                    curSelected = 0;
                    break;
            }
        }
        UpdateIndicator();
    }

    private void UpdateIndicator()
    {
        indicators[curActive].SetActive(false);
        indicators[curSelected].SetActive(true);
        curActive = curSelected;
    }

    private void ConfirmSelection()
    {
        AudioManager.Instance.Play("mousedown");
        switch (curSelected)
        {
            case 0:
                plots[GameManager.Instance.curPlot].GetComponent<Plot>().GrowCorn();
                break;
            case 1:
                plots[GameManager.Instance.curPlot].GetComponent<Plot>().GrowBeet();
                break;
            case 2:
                plots[GameManager.Instance.curPlot].GetComponent<Plot>().Exit();
                break;
        }
        GameObject.FindWithTag("Player").GetComponent<PlayerController>().ToggleGrowing();
        curSelected = 0;
        UpdateIndicator();
        gameObject.SetActive(false);
    }
}

using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class MedicineManager : MonoBehaviour
{
    public GameObject[] boxes;
    public GameObject[] selections;
    public GameObject selectionPanel;
    public GameObject errorText;

    public int[] chosen;

    private int curActive = 0;
    private int curSelected = 0;

    private bool selectingTop = true;
    private int curActiveBottom = 0;
    private int curSelectedBottom = 0;
    private int[] lst = new int[4];
    private List<int> occupied = new List<int>();

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Q))
        {
            CloseMenu();
        }

        if (Input.GetKeyDown(KeyCode.E) && !GameManager.Instance.gotMedicine)
        {
            ToggleMenu();
        }

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveParser("right");
        }

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveParser("left");
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

    private void CloseMenu()
    {
        AudioManager.Instance.Play("mousedown");
        if (selectingTop)
        {
            GameManager.Instance.isUiOpen = false;
            gameObject.SetActive(false);
        }
        else
        {
            selectionPanel.SetActive(false);
            curSelectedBottom = 0;
            UpdateBottomIndicator();
            selectingTop = !selectingTop;
        }
    }

    private void ToggleMenu()
    {
        if (selectingTop)
        {
            if (curSelected % 5 == 4)
            {
                CheckSolution();
                return;
            }
            else if (!occupied.Contains(curSelected / 5))
            {
                selectionPanel.SetActive(true);
                errorText.SetActive(false);
                selectingTop = !selectingTop;
                AudioManager.Instance.Play("mousedown");
                return;
            }
        }
        if (!selectingTop)
        {
            if (IsAvailable(curSelectedBottom))
            {
                boxes[curSelected].GetComponent<BoxScript>().ToggleSelection(curSelectedBottom);
                if (curSelectedBottom == 4)
                {
                    errorText.GetComponent<BoxScript>().SetText("Ingredient cleared!");
                }
                else
                {
                    errorText.GetComponent<BoxScript>().SetText("Ingredient added!");
                }
                AudioManager.Instance.Play("positive");
            }
            else
            {
                errorText.GetComponent<BoxScript>().SetText("Not enough ingredient!");
                AudioManager.Instance.Play("negative");
            }
            selectionPanel.SetActive(false);
            errorText.SetActive(true);
            curSelectedBottom = 0;
            UpdateBottomIndicator();
            selectingTop = !selectingTop;
            return;
        }
    }

    private void CheckSolution()
    {
        if (occupied.Contains(curSelected / 5))
        {
            ResetSolution();
            return;
        }
        chosen = new int[4];
        Array.Copy(GameManager.Instance.solution, lst, 4);
        var start = curActive - 4;
        for (int i = 0; i < 4; i++)
        {
            var selected = boxes[i + start].GetComponent<BoxScript>().current;
            if (selected == 4)
            {
                AudioManager.Instance.Play("negative");
                errorText.GetComponent<BoxScript>().SetText("Not all ingredients filled!");
                return;
            }
            else
            {
                chosen[i] = selected;
            }
        }

        for (int i = 0; i < 4; i++)
        {
            if (chosen[i] == lst[i])
            {
                boxes[i + start].GetComponent<BoxScript>().ToggleColor("green");
                lst[i] = -1;
                chosen[i] = -1;
            }
            else
            {
                boxes[i + start].GetComponent<BoxScript>().ToggleColor("red");
            }
        }
        for (int i = 0; i < 4; i++)
        {
            if (chosen[i] != -1 && lst.Contains(chosen[i]))
            {
                lst[Array.IndexOf(lst, chosen[i])] = -1;
                boxes[i + start].GetComponent<BoxScript>().ToggleColor("yellow");
            }
        }
        AudioManager.Instance.Play("positive");
        occupied.Add(curSelected / 5);
        boxes[curSelected].GetComponent<BoxScript>().SetText("Reset");
        if (CheckWin(chosen))
        {
            GameManager.Instance.gotMedicine = true;
            errorText.GetComponent<BoxScript>().SetText("Medicine successfully made!");
        }
        else
        {
            errorText.GetComponent<BoxScript>().SetText("The recipe doesn't seem quite right...");
        }
    }

    private bool CheckWin(int[] check)
    {
        for (int i = 0; i < check.Length; i++)
        {
            if (check[i] != -1)
            {
                return false;
            }
        }
        return true;
    }

    private void ResetSolution()
    {
        AudioManager.Instance.Play("neutral");
        var start = curActive - 4;
        for (int i = 0; i < 4; i++)
        {
            boxes[i + start].GetComponent<BoxScript>().ResetBox();
        }
        occupied.Remove(curSelected / 5);
        boxes[curSelected].GetComponent<BoxScript>().SetText("Mix");
        errorText.GetComponent<BoxScript>().SetText("Reseted!");
    }

    private void MoveParser(string dir)
    {
        AudioManager.Instance.Play("mouseover");
        if (!selectingTop)
        {
            MoveParserBottom(dir);
            return;
        }
        if (dir == "right")
        {
            if (curSelected % 5 == 4)
            {
                curSelected = curSelected - 4;
            }
            else
            {
                curSelected++;
            }
        }

        if (dir == "left")
        {
            if (curSelected % 5 == 0)
            {
                curSelected = curSelected + 4;
            }
            else
            {
                curSelected--;
            }
        }

        if (dir == "up")
        {
            if (curSelected / 5 == 0)
            {
                curSelected = curSelected + 15;
            }
            else
            {
                curSelected -= 5;
            }
        }

        if (dir == "down")
        {
            if (curSelected / 5 == 3)
            {
                curSelected = curSelected - 15;
            }
            else
            {
                curSelected += 5;
            }
        }
        UpdateIndicator();
    }

    private void MoveParserBottom(string dir)
    {
        if (dir == "right")
        {
            if (curSelectedBottom == 4)
            {
                curSelectedBottom = 0;
            }
            else if (curSelectedBottom != 1)
            {
                curSelectedBottom++;
            }
            else
            {
                curSelectedBottom = 4;
            }
        }

        if (dir == "left")
        {
            if (curSelectedBottom != 0 && curSelectedBottom != 2)
            {
                curSelectedBottom--;
            }
            else
            {
                curSelectedBottom = 4;
            }
        }

        if (dir == "up" || dir == "down")
        {
            if (curSelectedBottom == 4)
            {
                return;
            }
            else if (curSelectedBottom < 2)
            {
                curSelectedBottom += 2;
            }
            else if (curSelectedBottom > 1)
            {
                curSelectedBottom -= 2;
            }
        }
        UpdateBottomIndicator();
    }

    private void UpdateIndicator()
    {
        boxes[curActive].GetComponent<BoxScript>().ToggleSelector();
        boxes[curSelected].GetComponent<BoxScript>().ToggleSelector();
        curActive = curSelected;
    }

    private void UpdateBottomIndicator()
    {
        selections[curActiveBottom].GetComponent<BoxScript>().ToggleSelector();
        selections[curSelectedBottom].GetComponent<BoxScript>().ToggleSelector();
        curActiveBottom = curSelectedBottom;
    }

     private bool IsAvailable(int current)
    {
        switch (current)
            {
                case 0:
                    return GameManager.Instance.corn > 0;
                case 1:
                    return GameManager.Instance.tomato > 0;
                case 2:
                    return GameManager.Instance.fish > 0;
                case 3:
                    return GameManager.Instance.apple > 0;
                default:
                    return true;
            }        
    }
}

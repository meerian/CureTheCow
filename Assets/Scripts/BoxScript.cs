using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BoxScript : MonoBehaviour
{
    private GameObject selector;

    private GameObject image;

    public TextMeshProUGUI curText;

    public int current;

    public Sprite corn;
    public Sprite tomato;
    public Sprite fish;
    public Sprite apple;
    
    private void Start()
    {
        var trans = transform.Find("Selector");
        if (trans != null)
        {
            selector = trans.gameObject;
        }

        trans = transform.Find("Image");
        if (trans != null)
        {
            image = trans.gameObject;
        }

        current = 4;
    }

    public void ResetBox()
    {
        image.SetActive(false);
        current = 4;
        ToggleColor("original");
    }

    public void ToggleSelector()
    {
        selector.SetActive(!selector.activeSelf);
    }

    public void SetText(string str)
    {
        if (curText != null)
        {
            curText.text = str;
        }
    }

    public void ToggleColor(string colour)
    {
        switch(colour)
        {
            case "red":
                GetComponent<Image>().color = new Color32(240,100,100,150);
                return;
            case "green":
                GetComponent<Image>().color = new Color32(100,240,100,150);
                return;
            case "yellow":
                GetComponent<Image>().color = new Color32(240,240,100,150);
                return;
            case "original":
                GetComponent<Image>().color = new Color32(255,255,255,150);
                return;
        }
    }

    public void ToggleSelection(int selected)
    {
        if (current == selected)
        {
            return;
        }

        RemoveSelection(current);
        if (selected == 4)
        {
            image.SetActive(false);
            
        }
        else
        {
            image.SetActive(true);
            switch (selected)
            {
                case 0:
                    image.GetComponent<Image>().sprite = corn;
                    GameManager.Instance.corn--;
                    break;
                case 1:
                    image.GetComponent<Image>().sprite = tomato;
                    GameManager.Instance.tomato--;
                    break;
                case 2:
                    image.GetComponent<Image>().sprite = fish;
                    GameManager.Instance.fish--;
                    break;
                case 3:
                    image.GetComponent<Image>().sprite = apple;
                    GameManager.Instance.apple--;
                    break;
            }
            current = selected;
        }
    }

    private void RemoveSelection(int current)
    {
        switch (current)
            {
                case 0:
                    GameManager.Instance.corn++;
                    break;
                case 1:
                    GameManager.Instance.tomato++;
                    break;
                case 2:
                    GameManager.Instance.fish++;
                    break;
                case 3:
                    GameManager.Instance.apple++;
                    break;
            }
    }
}

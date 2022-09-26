using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CowPanel : MonoBehaviour
{
    public TextMeshProUGUI cowSubtext;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.E))
        {
            NextText();
            return;
        }
    }

    private void NextText()
    {
        AudioManager.Instance.Play("mousedown");
        if (!GameManager.Instance.gotMedicine)
        {
            GameManager.Instance.isUiOpen = false;
            gameObject.SetActive(false);
        }
        else
        {
            UIManager.Instance.ActivateWinPanel();
            gameObject.SetActive(false);
        }
    }

    public void UpdateText()
    {
        if (GameManager.Instance.gotMedicine)
        {
            cowSubtext.text = "You fed the cow the medicine!";
        }
    }
}

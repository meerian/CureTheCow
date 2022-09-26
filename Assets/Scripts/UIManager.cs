using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject plantingPanel;

    public GameObject plantingResult;

    public GameObject fishingPanel;

    public GameObject medicinePanel;

    public GameObject harvestSlider;

    public GameObject pauseMenu;

    public GameObject cowPanel;

    public GameObject winPanel;

    public TextMeshProUGUI cornVal;
    public TextMeshProUGUI tomatoVal;
    public TextMeshProUGUI fishVal;
    public TextMeshProUGUI appleVal;

    void Start()
    {   
        if (Instance == null)
        {
            Instance = this;
        }
    } 

    private void Update()
    {
        cornVal.text = GameManager.Instance.corn.ToString();
        tomatoVal.text = GameManager.Instance.tomato.ToString();
        fishVal.text = GameManager.Instance.fish.ToString();
        appleVal.text = GameManager.Instance.apple.ToString();
    }

    public void PauseGame()
    {
        AudioManager.Instance.Play("mousedown");
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void UnpauseGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void ActivatePlantPanel()
    {
        plantingPanel.SetActive(true);
    }

    public void ActivateWinPanel()
    {
        Time.timeScale = 0f;
        winPanel.SetActive(true);
    }

    public void ActivateCowPanel()
    {
        cowPanel.SetActive(true);
        cowPanel.GetComponent<CowPanel>().UpdateText();
    }

    public void ActivateMedicinePanel()
    {
        medicinePanel.SetActive(true);
    }

    public void ActivatePlantResult(string food)
    {
        plantingResult.SetActive(true);
        plantingResult.GetComponent<PlantingResult>().DisplayText(food);
    }

    public void ActivateHarvestSlider()
    {
        harvestSlider.SetActive(true);
        harvestSlider.GetComponent<HarvestSlider>().StartCooldown();
    }

    public void ActivateFishPanel(bool isCaught)
    {
        fishingPanel.SetActive(true);
        fishingPanel.GetComponent<FishingManager>().DisplayText(isCaught);
    }
}

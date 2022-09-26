using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HarvestSlider : MonoBehaviour
{
    public Slider slider;
    public float gain;
    public GameObject harvestPanel;

    private bool canClose = false;

    void Update()
    {
        if (canClose && (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Mouse0)))
        {
            Harvest();
            return;
        }
        slider.value += gain;
        if (slider.value >= slider.maxValue || slider.value <= 0)
        {
            gain = -gain;
        }
    }

    void Harvest()
    {
        AudioManager.Instance.Stop("treeharvest");
        harvestPanel.SetActive(true);
        if (slider.value >= 80)
        {
            AudioManager.Instance.Play("positive");
            harvestPanel.GetComponent<HarvestManager>().DisplayText(true);
        }
        else
        {
            AudioManager.Instance.Play("negative");
            harvestPanel.GetComponent<HarvestManager>().DisplayText(false);
        }
        slider.value = 0;
        canClose = false;
        gameObject.SetActive(false);
    }

    public void StartCooldown()
    {
        StartCoroutine("Cooldown");
    }

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(0.5f);
        canClose = true;
    }
}

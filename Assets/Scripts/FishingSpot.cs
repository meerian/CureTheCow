using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingSpot : Interactable
{
    public int index;

    private bool fishCaught = false;
    private bool canFish = true;

    public override void Interact()
    {
        if (canFish)
        {
            AudioManager.Instance.Play("castrod");
            GameManager.Instance.isUiOpen = true;
            GameObject.FindWithTag("Player").GetComponent<PlayerController>().ToggleFishing();
            GameObject.FindWithTag("Player").GetComponent<PlayerController>().CloseInteractableIcon();
            StartCoroutine("FishTimer");
        }
    }

    public override bool CanInteract()
    {
        return canFish;
    }

    private void Update()
    {
        if (fishCaught)
        {
            if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Mouse0))
            {  
                FishCaught();
            }
        }
    }

    private void FishCaught()
    {
        AudioManager.Instance.Play("positive");
        fishCaught = false;
        GameManager.Instance.fish++;
        UIManager.Instance.ActivateFishPanel(true);
    }

    public void Exit()
    {
        GameObject.FindWithTag("Player").GetComponent<PlayerController>().ToggleFishing();
        canFish = false;
        GameManager.Instance.isUiOpen = false;
        StartCoroutine("Cooldown");
    }

    IEnumerator FishTimer()
    {
        int randInt = Random.Range(3, 7);
        yield return new WaitForSeconds(randInt);
        GameObject.FindWithTag("Player").GetComponent<PlayerController>().OpenInteractableIcon();
        fishCaught = true;
        yield return new WaitForSeconds(0.75f);
        if (fishCaught)
        {
            AudioManager.Instance.Play("negative");
            fishCaught = false;
            UIManager.Instance.ActivateFishPanel(false);
        }
    }

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(0.5f);
        canFish = true;
    }
}


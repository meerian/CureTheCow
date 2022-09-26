using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeController : Interactable
{
    public SpriteRenderer sr;
    public Sprite treeUngrown;
    public Sprite treeGrown;

    public int index;

    private bool canHarvest = true;

    public override void Interact()
    {
        if (canHarvest)
        {
            AudioManager.Instance.Play("treeharvest");
            canHarvest = false;
            GameManager.Instance.curTree = index;
            GameManager.Instance.isUiOpen = true;
            GameObject.FindWithTag("Player").GetComponent<PlayerController>().CloseInteractableIcon();
            UIManager.Instance.ActivateHarvestSlider();
            return;
        }
    }

    public override bool CanInteract()
    {
        return canHarvest;
    }

    public void Harvest(bool passed)
    {
        if (passed)
        {
            GameManager.Instance.apple++;
        }
        Exit();
    }

    public void Exit()
    {
        GameManager.Instance.isUiOpen = false;
        StartCoroutine("GrowTimer");
    }

    IEnumerator GrowTimer()
    {
        sr.sprite = treeUngrown;
        yield return new WaitForSeconds(10);
        canHarvest = true;
        sr.sprite = treeGrown;
    }
}

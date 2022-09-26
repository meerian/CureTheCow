using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedicineSpot : Interactable
{
    public override void Interact()
    {
        AudioManager.Instance.Play("mousedown");
        GameManager.Instance.isUiOpen = true;
        UIManager.Instance.ActivateMedicinePanel();
    }

    public override bool CanInteract()
    {
        return true;
    }

    public void Exit()
    {
        GameManager.Instance.isUiOpen = false;
    }
}


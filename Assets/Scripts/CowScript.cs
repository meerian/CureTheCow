using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowScript : Interactable
{
    public override void Interact()
    {
            AudioManager.Instance.Play("moo");
            GameManager.Instance.isUiOpen = true;
            UIManager.Instance.ActivateCowPanel();
    }

    public override bool CanInteract()
    {
        return true;
    }
}

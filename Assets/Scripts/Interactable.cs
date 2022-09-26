using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]

public abstract class Interactable : MonoBehaviour
{
    private void Reset()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;
    }
    public abstract void Interact();

    public abstract bool CanInteract();

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && CanInteract())
        {
            other.GetComponent<PlayerController>().OpenInteractableIcon();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().CloseInteractableIcon();
        }
    }

}

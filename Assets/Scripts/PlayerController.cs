using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField]

    public Rigidbody2D rb;

    public GameObject InteractableIcon;

    private float movementSpeed = 5f;

    private Vector2 movement;

    private Vector2 boxSize = new Vector2(1f, 1f);

    public Animator animator;

    // Update is called once per frame
    private void Update()
    {
        if (GameManager.Instance.isUiOpen)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.E) && Time.timeScale != 0)
        {
            CheckInteraction();
        }
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = 0;
        if (movement.x == 0)
        {
            movement.y = Input.GetAxisRaw("Vertical");
        }
        if (movement != Vector2.zero)
        {
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
        }
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        AudioManager.Instance.Play("impact");
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.isUiOpen)
        {
            return;
        }
        rb.MovePosition(rb.position + movement * movementSpeed * Time.fixedDeltaTime);
    }

    public void ToggleFishing()
    {
        animator.SetBool("isFishing", !animator.GetBool("isFishing"));
    }

    public void ToggleGrowing()
    {
        animator.SetBool("isGrowing", !animator.GetBool("isGrowing"));
    }

    public void OpenInteractableIcon()
    {
        AudioManager.Instance.Play("caninteract");
        InteractableIcon.SetActive(true);
    }

    public void CloseInteractableIcon()
    {
        InteractableIcon.SetActive(false);
    }

    private void CheckInteraction()
    {
        RaycastHit2D[] hits = Physics2D.BoxCastAll(transform.position, boxSize, 0, Vector2.zero);

        if (hits.Length > 0)
        {
            foreach (RaycastHit2D rc in hits)
            {
                if (rc.transform.GetComponent<Interactable>())
                {
                    rc.transform.GetComponent<Interactable>().Interact();
                    return;
                }
            }
        }
    }

    private void FarmSound()
    {
        AudioManager.Instance.Play("hoehit");
    }

    private void WalkSound()
    {
        AudioManager.Instance.Play("walk");
    }
}

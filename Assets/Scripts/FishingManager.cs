using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingManager : MonoBehaviour
{

    public GameObject successText;
    public GameObject failText;

    public GameObject fishingSpot;

    private bool canClose = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey && canClose)
        {
            fishingSpot.GetComponent<FishingSpot>().Exit();
            gameObject.SetActive(false);
        }
    }

    public void DisplayText(bool isCaught)
    {
        if (isCaught)
        {
            failText.SetActive(false);
            successText.SetActive(true);
        }
        else
        {
            failText.SetActive(true);
            successText.SetActive(false);
        }
        canClose = false;
        StartCoroutine("Cooldown");
    }

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(0.5f);
        canClose = true;
    }
}

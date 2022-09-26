using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarvestManager : MonoBehaviour
{
    public GameObject[] trees;

    public GameObject successText;
    public GameObject failText;

    private bool isHarvested;
    private bool canClose = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey && canClose)
        {
            trees[GameManager.Instance.curTree].GetComponent<TreeController>().Harvest(isHarvested);
            gameObject.SetActive(false);
        }
    }

    public void DisplayText(bool passed)
    {

        if (passed)
        {
            failText.SetActive(false);
            successText.SetActive(true);
        }
        else
        {
            failText.SetActive(true);
            successText.SetActive(false);
        }
        isHarvested = passed;
        canClose = false;
        StartCoroutine("Cooldown");
    }

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(0.5f);
        canClose = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantingResult : MonoBehaviour
{
    public GameObject cornText;
    public GameObject tomatoText;
    public GameObject[] plots;

    private bool canClose = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey && canClose)
        {
            plots[GameManager.Instance.curPlot].GetComponent<Plot>().Exit();
            gameObject.SetActive(false);
        }
    }

    public void DisplayText(string food)
    {
        if (food == "corn")
        {
            tomatoText.SetActive(false);
            cornText.SetActive(true);
        }
        if (food == "tomato")
        {
            tomatoText.SetActive(true);
            cornText.SetActive(false);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plot : Interactable
{
    public GameObject CornPrefab;
    public GameObject TomatoPrefab;

    public int index;

    private bool isDone = false;
    private bool isOccupied = false;
    private GameObject curPlant;
    private string food;

    public override void Interact()
    {
        if (isDone)
        {
            AudioManager.Instance.Play("positive");
            GameManager.Instance.curPlot = index;
            GameManager.Instance.isUiOpen = true;
            UIManager.Instance.ActivatePlantResult(food);
            Destroy(curPlant);
            isDone = false;
            isOccupied = false;
            return;

        }
        if (!isOccupied)
        {
            AudioManager.Instance.Play("mousedown");
            Vector3 position = transform.position + new Vector3(-0.5f, 0.3f, 0);
            GameObject.FindWithTag("Player").transform.position = position;
            GameObject.FindWithTag("Player").GetComponent<PlayerController>().ToggleGrowing();
            GameManager.Instance.curPlot = index;
            GameManager.Instance.isUiOpen = true;
            UIManager.Instance.ActivatePlantPanel();
            return;
        }
    }

    public override bool CanInteract()
    {
        return !isOccupied || isDone;
    }

    public void GrowCorn()
    {
        food = "corn";
        curPlant = Instantiate(CornPrefab, transform.position + new Vector3(0, 0.2f, 0), Quaternion.identity);
        isOccupied = true;
        GameManager.Instance.isUiOpen = false;
        StartCoroutine("GrowTimer");
    }

    public void GrowBeet()
    {
        food = "tomato";
        curPlant = Instantiate(TomatoPrefab, transform.position + new Vector3(-0.1f, 0.2f, 0), Quaternion.identity);
        isOccupied = true;
        GameManager.Instance.isUiOpen = false;
        StartCoroutine("GrowTimer");
    }

    public void Exit()
    {
        GameManager.Instance.isUiOpen = false;
    }

    IEnumerator GrowTimer()
    {
        yield return new WaitForSeconds(30);
        isDone = true;
    }
}

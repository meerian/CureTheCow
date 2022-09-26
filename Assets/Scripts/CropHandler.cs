using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropHandler : MonoBehaviour
{
    public string Name;
    public SpriteRenderer sr;
    public Sprite grown1;
    public Sprite grown2;
    public Sprite grown3;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("StartGrowing");
    }

    IEnumerator StartGrowing()
    {
        yield return new WaitForSeconds(10);
        sr.sprite = grown1;
        yield return new WaitForSeconds(10);
        sr.sprite = grown2;
        yield return new WaitForSeconds(10);
        sr.sprite = grown3;
    }

    private void OnDestroy()
    {
        if (Name == "Corn")
        {
            GameManager.Instance.corn++;
        }
        if (Name == "Tomato")
        {
            GameManager.Instance.tomato++;
        }
    }
}

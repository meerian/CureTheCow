using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour 
{
    public static GameManager Instance;

    public int timeTaken = 0;

    public bool isUiOpen = false;

    public bool gotMedicine = false;

    public int curPlot = 0;
    public int curTree = 0;

    public int corn = 0;
    public int tomato = 0;
    public int fish = 0;
    public int apple = 0;

    public int[] solution;
    
    void Awake() 
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        //DontDestroyOnLoad(transform.gameObject);
    }

    void Start()
    {   
        if (Instance == null)
        {
            Instance = this;
        }
        solution = new int[4];
        for (int i = 0; i < solution.Length; i++)
        {
            int randInt = Random.Range(0, 4);
            solution[i] = randInt;
        }
        StartCoroutine("Timer");
    } 

    public void DeleteInstance()
    {
        Instance = null;
        Destroy(this);
    }

    private IEnumerator Timer()
    {
        while (true)
        {
            TimeCount();
            yield return new WaitForSeconds(1);
        }
    }

    private void TimeCount()
    {
        timeTaken++;
    }
}

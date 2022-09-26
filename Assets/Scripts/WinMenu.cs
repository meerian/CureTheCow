using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class WinMenu : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    public Animator transition;
    public CanvasGroup cg;

    private bool canClose = false;

    private void OnEnable()
    {
        AudioManager.Instance.Pause("gamemusic");
        AudioManager.Instance.Play("gamewin");
        var timespan = TimeSpan.FromSeconds(GameManager.Instance.timeTaken);
        timeText.text = timespan.ToString(@"mm\:ss");
        StartCoroutine("StartCooldown");
    }

    // Update is called once per frame
    void Update()
    {
        if (canClose && Input.GetKeyDown(KeyCode.E))
        {
            
            StartCoroutine("LoadLevel", 0);
        }
    }

    IEnumerator LoadLevel(int levelindex)
    {
        Time.timeScale = 1;
        cg.alpha = 0;
        transition.SetTrigger("Start");

        yield return new WaitForSecondsRealtime(1f);

        SceneManager.LoadScene(levelindex);
    }

    IEnumerator StartCooldown()
    {
        yield return new WaitForSecondsRealtime(1f);
        canClose = true;
        AudioManager.Instance.Play("gamemusic");
    }
}

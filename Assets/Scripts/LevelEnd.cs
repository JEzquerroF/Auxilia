using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEnd : MonoBehaviour
{
    public float time;
    public int level;
    TextMeshProUGUI timer;
    TextMeshProUGUI bestTime;

    private void Start()
    {
        time = 0;
        timer = GameObject.Find("Time").GetComponent<TextMeshProUGUI>();
        bestTime = GameObject.Find("BestTime").GetComponent<TextMeshProUGUI>();

        switch (level)
        {
            case 1:
                bestTime.text = "Best: " + TimeSpan.FromSeconds(PlayerPrefs.GetFloat("Level1")).ToString("mm':'ss':'ms");
                break;
            case 2:
                bestTime.text = "Best: " + TimeSpan.FromSeconds(PlayerPrefs.GetFloat("Level2")).ToString("mm':'ss':'ms");
                break;
            case 3:
                bestTime.text = "Best: " + TimeSpan.FromSeconds(PlayerPrefs.GetFloat("Level3")).ToString("mm':'ss':'ms");
                break;
            case 4:
                bestTime.text = "Best: " + TimeSpan.FromSeconds(PlayerPrefs.GetFloat("Level4")).ToString("mm':'ss':'ms");
                break;
            case 5:
                bestTime.text = "Best: " + TimeSpan.FromSeconds(PlayerPrefs.GetFloat("Level5")).ToString("mm':'ss':'ms");
                break;
        }
    }

    private void Update()
    {
        time += Time.deltaTime;
        timer.text = TimeSpan.FromSeconds(time).ToString("mm':'ss");
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            switch (level)
            {
                case 1:
                if (time < PlayerPrefs.GetFloat("Level1") || PlayerPrefs.GetFloat("Level1") == 0)
                    PlayerPrefs.SetFloat("Level1", time);
                break;
                case 2:
                if (time < PlayerPrefs.GetFloat("Level2") || PlayerPrefs.GetFloat("Level2") == 0)
                    PlayerPrefs.SetFloat("Level2", time);
                break;
                case 3:
                if (time < PlayerPrefs.GetFloat("Level3") || PlayerPrefs.GetFloat("Level3") == 0)
                    PlayerPrefs.SetFloat("Level3", time);
                break;
                case 4:
                if (time < PlayerPrefs.GetFloat("Level4") || PlayerPrefs.GetFloat("Level4") == 0)
                    PlayerPrefs.SetFloat("Level4", time);
                break;
            }

            PauseMenu pause = GameObject.Find("/Player/PauseCanvas").GetComponent<PauseMenu>();
            pause.Pause();
            pause.canBeUnpaused = false;
            GameObject.Find("/Player/PauseCanvas/PauseMenu/ResumeButton").SetActive(false);
            GameObject.Find("/Player/PauseCanvas/PauseMenu/OptionsButton").SetActive(false);
            GameObject.Find("/Player/PauseCanvas/PauseMenu/QuitButton").SetActive(false);
        }
    }

    public void EnemyRush()
    {
        if (time > PlayerPrefs.GetFloat("Level5") || PlayerPrefs.GetFloat("Level5") == 0)
            PlayerPrefs.SetFloat("Level5", time);
    }
}

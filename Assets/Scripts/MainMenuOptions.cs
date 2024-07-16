using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainMenuOptions : MonoBehaviour
{
    public GameObject optionsUi;
    public GameObject mainMenuUi;
    public GameObject levelSelecionUi;

    public TextMeshProUGUI HS1;
    public TextMeshProUGUI HS2;
    public TextMeshProUGUI HS3;
    public TextMeshProUGUI HS4;
    public TextMeshProUGUI HS5;

    void Start()
    {
        if (PlayerPrefs.GetFloat("Level1") != 0)
            HS1.text = "Best: " + TimeSpan.FromSeconds(PlayerPrefs.GetFloat("Level1")).ToString("mm':'ss':'ms");
        if (PlayerPrefs.GetFloat("Level2") != 0)
            HS2.text = "Best: " + TimeSpan.FromSeconds(PlayerPrefs.GetFloat("Level2")).ToString("mm':'ss':'ms");
        if (PlayerPrefs.GetFloat("Level3") != 0)
            HS3.text = "Best: " + TimeSpan.FromSeconds(PlayerPrefs.GetFloat("Level3")).ToString("mm':'ss':'ms");
        if (PlayerPrefs.GetFloat("Level4") != 0)
            HS4.text = "Best: " + TimeSpan.FromSeconds(PlayerPrefs.GetFloat("Level4")).ToString("mm':'ss':'ms");
        if (PlayerPrefs.GetFloat("Level5") != 0)
            HS5.text = "Best: " + TimeSpan.FromSeconds(PlayerPrefs.GetFloat("Level5")).ToString("mm':'ss':'ms");
        AudioManager.PlayRandomMusic();
    }

    public void Options()
    {
        optionsUi.SetActive(true);
        mainMenuUi.SetActive(false);
    }

    public void LevelSelection()
    {
        optionsUi.SetActive(false);
        mainMenuUi.SetActive(false);
        levelSelecionUi.SetActive(true);

    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void GoBackFromOptions()
    {
        optionsUi.SetActive(false);
        mainMenuUi.SetActive(true);
    }
    public void GoBackFromLevel()
    {
        levelSelecionUi.SetActive(false);
        mainMenuUi.SetActive(true);
    }
}

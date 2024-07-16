using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool isGamePaused = false;
    public bool canBeUnpaused = true;

    public GameObject pauseMenuUi;
    public GameObject optionsUi;

    private void Start()
    {
        Resume();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isGamePaused && canBeUnpaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUi.SetActive(false);
        optionsUi.SetActive(false);
        Time.timeScale = 1.0f;
        isGamePaused = false;
    }

    public void Pause()
    {
        /* al pausar queremos mostrar el menu de pausa
         * pausar el tiempo de juego
         * cambiar variable isGamePaused a true
        */
        optionsUi.SetActive(false);
        pauseMenuUi.SetActive(true);
        Time.timeScale = 0f;
        isGamePaused = true;

    }
    public void LoadMenu()
    {
        Time.timeScale = 1.0f;
        isGamePaused=false;
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void Options()
    {
        pauseMenuUi.SetActive(false);
        optionsUi.SetActive(true);
    }

    public void Restart()
    {
        Resume();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}


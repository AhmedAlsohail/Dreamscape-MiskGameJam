using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pauseManager : MonoBehaviour
{
    private bool isPaused = false;
    public GameObject pausePanel;

    private void Start()
    {
        Time.timeScale = 1f;
        isPaused = false;
        pausePanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Pause") || Input.GetButtonDown("Info"))
        {
            TogglePause();
        }

    }

    public void TogglePause()
    {
        if (isPaused)
        {
            Time.timeScale = 1f;
            isPaused = false;
            pausePanel.SetActive(false);
        }
        else
        {
            Time.timeScale = 0f;
            isPaused = true;
            pausePanel.SetActive(true);

        }
    }
}

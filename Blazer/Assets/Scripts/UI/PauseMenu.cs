using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {


    public GameObject pausePanel;


    public void Initialize() {

    }

    public void TogglePause() {
        if (!GameManager.GamePaused) {
            GameManager.GamePaused = true;
            pausePanel.SetActive(true);
            Time.timeScale = 0f;
        }
        else {
            OnResumeClick();
        }


    }


    public void OnResumeClick() {
        Time.timeScale = 1f;
        GameManager.GamePaused = false;
        pausePanel.SetActive(false);
    }

    public void OnQuitClick() {
        Application.OpenURL("https://docs.google.com/forms/d/e/1FAIpQLSe7j2wWKBCb7pEC0UM2nSHIBhP5ZUSNxQAATtdEDz-lMFzpeg/viewform?usp=sf_link");
        Application.Quit();
    }


}

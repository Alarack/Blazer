using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public List<string> availableLevels = new List<string>();


    void Start() {

    }

    void Update() {

    }

    public string LevelPicker(List<string> possibleChoices)
    {
        string scenePicked;
        scenePicked = possibleChoices[Random.Range(0, possibleChoices.Count)];
        
        return scenePicked;
    }

    public void PlayGame() {
        SceneManager.LoadScene(LevelPicker(availableLevels));
    }

    public void QuitGame() {
        Application.OpenURL("https://docs.google.com/forms/d/e/1FAIpQLSe7j2wWKBCb7pEC0UM2nSHIBhP5ZUSNxQAATtdEDz-lMFzpeg/viewform?usp=sf_link");
        Application.Quit();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public string mainScene;
    public GameObject itemInfoPanel;
    public InfoGatherer infoGatherer;

	void Start () {
		
	}

	void Update () {
		
	}


    public void PlayGame() {
        SceneManager.LoadScene(mainScene);
    }

    public void QuitGame() {
        Application.OpenURL("https://docs.google.com/forms/d/e/1FAIpQLSe7j2wWKBCb7pEC0UM2nSHIBhP5ZUSNxQAATtdEDz-lMFzpeg/viewform?usp=sf_link");
        Application.Quit();
    }

    public void OpenItemInfoPanel() {
        itemInfoPanel.SetActive(true);
        infoGatherer.Open();
        
    }
}

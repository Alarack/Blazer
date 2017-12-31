using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public string mainScene;

	void Start () {
		
	}

	void Update () {
		
	}


    public void PlayGame() {
        SceneManager.LoadScene(mainScene);
    }

    public void QuitGame() {
        Application.Quit();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableAfterTime : MonoBehaviour {


    public float duration;

    private Timer disableTimer;

    void Start () {
        disableTimer = new Timer("Disable Timer", duration, false, DisableMe);
	}

	void Update () {
        if (disableTimer != null)
            disableTimer.UpdateClock();
	}

    private void DisableMe() {
        if (gameObject.activeSelf) {
            gameObject.SetActive(false);
        }
    }
}

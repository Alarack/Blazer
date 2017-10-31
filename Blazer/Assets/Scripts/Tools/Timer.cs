using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Timer {

    public string timerName;

    public float Ratio { get { return _timer / duration; } }

    private float duration;
    private bool resetTimerOnComplete;

    private Action completionCallback;

    private float _timer;


    public Timer(string timerName, float duration, bool resetOnComplete = false, Action completionCallback = null) {
        this.timerName = timerName;
        this.duration = duration;
        this.resetTimerOnComplete = resetOnComplete;

        if (completionCallback != null)
            this.completionCallback += completionCallback;
    }

    public void ModifyDuration(float mod) {
        duration += mod;

        if (duration <= 0f) {
            duration = 0f;
        }
    }

    public void UpdateClock() {
        if (_timer <= duration) {
            _timer += Time.deltaTime;

            if (_timer >= duration) {

                if (completionCallback != null)
                    completionCallback();

                if (resetTimerOnComplete) {
                    ResetTimer();
                }
            }
        }
    }

    public void ResetTimer() {
        _timer = 0f;
        if (timerName == "SequenceTimer")
            Debug.Log(timerName + " has been reset");
    }


}

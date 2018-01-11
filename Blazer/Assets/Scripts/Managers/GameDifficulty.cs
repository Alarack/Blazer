using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDifficulty : MonoBehaviour {

    public enum DifficultyLevel {
        Easy = 0,
        Normal = 1,
        Hard = 2,
        VeryHard = 3,
        Insane = 4,
        DearGod = 5,
        ThereIsNoGod = 6,
    }

    public DifficultyLevel Difficulty { get; private set; }
    public DifficultyData difficultyData;
    public float incrementInterval;

    private Timer incrementTimer;
    private int difficultyCounter;

    public void Initialize() {
        incrementTimer = new Timer("Difficulty Timer", incrementInterval, true, IncreaseDifficulty);
    }


    public void ManagedUpdate() {
        if (!GameManager.GamePaused) {
            if (incrementTimer != null)
                incrementTimer.UpdateClock();
        }
    }

    private void IncreaseDifficulty() {
        int currentDifficulty = (int)Difficulty;

        currentDifficulty++;

        if(currentDifficulty < System.Enum.GetValues(typeof(DifficultyLevel)).Length) {
            Difficulty = (DifficultyLevel)currentDifficulty;

            EventData data = new EventData();
            data.AddInt("DifficultyValue", (int)Difficulty);

            Grid.EventManager.SendEvent(Constants.GameEvent.DifficultyChange, data);

            Debug.Log("Difficulty Increased to: " + Difficulty);
        }
        else {
            Debug.Log("Max Difficulty Reached");
        }
    }

    public float GetDifficultySpawnValue() {
        int count = difficultyData.difficultyEntries.Count;

        for(int i = 0; i < count; i++) {
            if(difficultyData.difficultyEntries[i].difficultyLevel == Difficulty) {
                return difficultyData.difficultyEntries[i].spawnRateIncrease;
            }
        }

        return 0f;
    }


}

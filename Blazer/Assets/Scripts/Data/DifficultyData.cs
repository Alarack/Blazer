using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Difficulty Data")]
public class DifficultyData : ScriptableObject {

    public List<DifficultyEntry> difficultyEntries = new List<DifficultyEntry>();


    [System.Serializable]
    public class DifficultyEntry {
        public GameDifficulty.DifficultyLevel difficultyLevel;
        public float spawnRateIncrease;
    }

}

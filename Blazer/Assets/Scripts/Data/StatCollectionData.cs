using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BaseStatType = Constants.BaseStatType;

[CreateAssetMenu(menuName = "Stat Set")]
[System.Serializable]
public class StatCollectionData : ScriptableObject {

    public string collectionName;

    public List<StatDisplay> stats = new List<StatDisplay>();




    [System.Serializable]
    public class StatDisplay {
        public BaseStatType stat;
        public float maxValue;
    }

}

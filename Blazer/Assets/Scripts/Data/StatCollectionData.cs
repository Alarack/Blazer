using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using EntityStat = Constants.EntityStat;

[CreateAssetMenu(menuName = "Stat Set")]
[System.Serializable]
public class StatCollectionData : ScriptableObject {

    public string collectionName;

    public List<StatDisplay> stats = new List<StatDisplay>();




    [System.Serializable]
    public class StatDisplay {
        public EntityStat stat;
        public float maxValue;
    }

}

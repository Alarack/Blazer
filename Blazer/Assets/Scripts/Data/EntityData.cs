using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EntityData : ScriptableObject {

    public string entityName;
    public List<SpecialAbility> specialAbilities = new List<SpecialAbility>();


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AbilityManager : MonoBehaviour {




    public List<SpecialAbilityData> abilityData = new List<SpecialAbilityData>();


    protected Entity source;
    protected List<SpecialAbility> abilities = new List<SpecialAbility>();


    public virtual void Initialize(Entity source) {
        this.source = source;

        for(int i = 0; i < abilityData.Count; i++) {
            SpecialAbility newAbility = new SpecialAbility();
            newAbility.Initialize(source, abilityData[i]);
            abilities.Add(newAbility);
        }

    }

    protected virtual void Update() {
        for(int i = 0; i < abilities.Count; i++) {
            abilities[i].ManagedUpdate();
        }

    }


}

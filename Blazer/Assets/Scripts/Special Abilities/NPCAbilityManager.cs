using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAbilityManager : AbilityManager {

    public List<float> abilityMaxRanges = new List<float>();
    public List<float> abilityMinRanges = new List<float>();
    public List<Detector> abilityDetector = new List<Detector>();

    public List<NPCAbilityContainer> npcAbiliites = new List<NPCAbilityContainer>();

    public override void Initialize(Entity source) {
        base.Initialize(source);

        for(int i = 0; i < abilities.Count; i++) {
            try {
                NPCAbilityContainer newAbility = new NPCAbilityContainer(abilities[i], abilityMaxRanges[i], abilityMinRanges[i]);
                npcAbiliites.Add(newAbility);
                //Debug.Log("Adding" + newAbility.ability.abilityName + " to list");
            }
            catch (System.IndexOutOfRangeException) {
                Debug.LogError("Differing number of entires");
                throw new System.IndexOutOfRangeException();
                
            }
            //playerAbilities.Add(newAbility);
        }
    }


    public void ActivateAbility() {
        List<NPCAbilityContainer> possibleAbilities = new List<NPCAbilityContainer>();
        SpecialAbility targetAbility = null;

        //Debug.Log("Trying to activate");

        for(int i = 0; i < npcAbiliites.Count; i++) {
            //Debug.Log("Checking " + npcAbiliites[i].ability.abilityName + " for readiness");

            if(!IsAbilityInUse() /*|| npcAbiliites[i].ability.overrideOtherAbilities*/) {
                if(!possibleAbilities.Contains(npcAbiliites[i]) && npcAbiliites[i].ability.Recovery.Ready)
                    possibleAbilities.Add(npcAbiliites[i]);

                //Debug.Log("Checking " + npcAbiliites[i].ability.abilityName + " is ready");
            }
            //else {
            //    Debug.Log(npcAbiliites[i].ability.abilityName + " is in use: " + npcAbiliites[i].ability.InUse);
            //}
        }


        if(possibleAbilities.Count > 0)
            targetAbility = possibleAbilities[0].ability;

        if (targetAbility != null) {
            targetAbility.Activate();
            //Debug.Log( targetAbility.source.entityName + " " + targetAbility.source.SessionID + " is activating an ability");
            //targetAbility = null;
        }
    }

    [System.Serializable]
    public class NPCAbilityContainer {
        public SpecialAbility ability;
        public float maxUseRange;
        public float minUseRange;

        public NPCAbilityContainer(SpecialAbility ability, float maxUseRange, float minUseRange) {
            this.ability = ability;
            this.maxUseRange = maxUseRange;
            this.minUseRange = minUseRange;
        }


    }


}

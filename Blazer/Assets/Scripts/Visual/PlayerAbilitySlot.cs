using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAbilitySlot : MonoBehaviour {



    public Image dimmer;
    public Image icon;

    private PlayerQuickBar parent;
    private SpecialAbility ability;
    private SpecialAbilityRecovery recovery;

    public void Initialize(PlayerQuickBar parent, SpecialAbility ability) {
        this.parent = parent;
        this.ability = ability;

        recovery = ability.Recovery;

        SetIcon();
    }



    private void SetIcon() {
        icon.sprite = ability.abilityIcon;
    }

    private void Update() {
        if(recovery != null)
            UpdateCooldown();

    }

    public void UpdateCooldown() {
        if (!recovery.Ready) {
            switch (recovery.recoveryType) {
                case Constants.SpecialAbilityRecoveryType.Timed:
                    dimmer.fillAmount = Mathf.Abs( ((RecoveryCooldown)recovery).RatioOfRecovery - 1);


                    break;
            }
        }

        if (recovery.Ready && dimmer.fillAmount != 0) {
            dimmer.fillAmount = 0;
        }

    }



}

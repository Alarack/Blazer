using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainHud : MonoBehaviour {

    public static MainHud mainHud;
    public PlayerQuickBar quickBar;


    private void Awake() {
        if (mainHud == null)
            mainHud = this;
        else
            Destroy(this);
    }

    public static void SetPlayerSlot(SpecialAbility ability, int index) {
        if(mainHud != null){
            mainHud.quickBar.SetQuickBarSlot(ability, index);
        }


    }


}

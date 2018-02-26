using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameResourceDisplay : MonoBehaviour {

    public Text moneyText;
    public Text keysText;

    private Entity player;

    public void Initialize() {
        player = GameManager.GetPlayer();

        if(player == null) {
            Debug.LogError("[GameResourceDisplay] Player not found");
            return;
        }

        moneyText.text = "Money: " + "$" + player.stats.GetStatModifiedValue(Constants.BaseStatType.Money).ToString();
        keysText.text = "Keys: " + player.stats.GetStatModifiedValue(Constants.BaseStatType.Keys).ToString();

        RegisterEventListeners();
    }


    private void RegisterEventListeners() {
        EventGrid.EventManager.RegisterListener(Constants.GameEvent.StatChanged, OnStatChanged);
    }


    private void OnStatChanged(EventData data) {
        Entity target = data.GetMonoBehaviour("Target") as Entity;
        if (target != player)
            return;

        Constants.BaseStatType stat = (Constants.BaseStatType)data.GetInt("Stat");

        if (stat != Constants.BaseStatType.Money && stat != Constants.BaseStatType.Keys)
            return;


        switch (stat) {
            case Constants.BaseStatType.Money:
                moneyText.text = "Money: " + "$" + player.stats.GetStatModifiedValue(Constants.BaseStatType.Money).ToString();
                break;

            case Constants.BaseStatType.Keys:
                keysText.text = "Keys: " + player.stats.GetStatModifiedValue(Constants.BaseStatType.Keys).ToString();
                break;
        }


    }
    

}

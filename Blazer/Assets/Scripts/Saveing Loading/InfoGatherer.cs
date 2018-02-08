using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoGatherer : MonoBehaviour {

    public GameObject panel;
    public GameObject textTemplate;
    public RectTransform textHolder;


    public ItemData[] allItems;

    private List<GameObject> textEntries = new List<GameObject>();



    private void Start() {
        GetItemData();
    }

    private void GetItemData() {
        allItems = Resources.LoadAll<ItemData>("ItemData");
    }

    public void Open() {
        ClearList();
        PopulateList();
    }

    public void Close() {
        panel.SetActive(false);
    }

    private void PopulateList() {
        int count = allItems.Length;

        for(int i = 0; i < count; i++) {
            ItemData item = allItems[i];

            GameObject entry = Instantiate(textTemplate) as GameObject;
            entry.transform.SetParent(textHolder, false);
            Text entryText = entry.GetComponentInChildren<Text>();

            string lockedStatus = "Locked";
            if (item.unlocked)
                lockedStatus = "Unlocked";

            entryText.text = item.itemName + " : " + lockedStatus;
            textEntries.Add(entry);

        }
    }

    private void ClearList() {
        int count = textEntries.Count;

        for(int i = 0; i < count; i++) {
            Destroy(textEntries[i]);
        }

        textEntries.Clear();
    }

}

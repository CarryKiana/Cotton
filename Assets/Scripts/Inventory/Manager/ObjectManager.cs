using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    private Dictionary<ItemName, bool> itemAvailableDict = new Dictionary<ItemName, bool>();

    private void OnEnable() {
        EventHandler.BeforeSceneUnloadEvent += OnBeforeSceneUnloadEvent;
        EventHandler.AfterSceneLoadEvent += OnAfterSceneLoadEvent;
        EventHandler.UpdateUIEvent += OnUpdadeUIEvent;
    }

    private void OnDisable() {
        EventHandler.BeforeSceneUnloadEvent -= OnBeforeSceneUnloadEvent;
        EventHandler.AfterSceneLoadEvent -= OnAfterSceneLoadEvent;
        EventHandler.UpdateUIEvent -= OnUpdadeUIEvent;
    }

    private void OnBeforeSceneUnloadEvent() {
        foreach(var item in FindObjectsOfType<Item>()) {
            if (!itemAvailableDict.ContainsKey(item.itemName)) {
                itemAvailableDict.Add(item.itemName, true);
            }
        }
    }
    private void OnAfterSceneLoadEvent() {
        foreach(var item in FindObjectsOfType<Item>()) {
            if (!itemAvailableDict.ContainsKey(item.itemName)) {
                itemAvailableDict.Add(item.itemName, true);
            } else {
                item.gameObject.SetActive(itemAvailableDict[item.itemName]);
            }
        }
    }

    private void OnUpdadeUIEvent(ItemDetails itemDetails, int index) {
        if (itemDetails != null) {
            itemAvailableDict[itemDetails.itemName] = false;
        }
    }
}

using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour,ISaveable
{
    private Dictionary<ItemName, bool> itemAvailableDict = new Dictionary<ItemName, bool>();
    private Dictionary<string, bool> interactiveStateDict = new Dictionary<string, bool>();

    private void OnEnable() {
        EventHandler.BeforeSceneUnloadEvent += OnBeforeSceneUnloadEvent;
        EventHandler.AfterSceneLoadEvent += OnAfterSceneLoadEvent;
        EventHandler.UpdateUIEvent += OnUpdadeUIEvent;
        EventHandler.StartNewGameEvent += OnStartNewGameEvent;
    }

    private void OnDisable() {
        EventHandler.BeforeSceneUnloadEvent -= OnBeforeSceneUnloadEvent;
        EventHandler.AfterSceneLoadEvent -= OnAfterSceneLoadEvent;
        EventHandler.UpdateUIEvent -= OnUpdadeUIEvent;
        EventHandler.StartNewGameEvent -= OnStartNewGameEvent;
    }
    private void Start()
    {
        ISaveable saveable = this;
        saveable.SaveableRegister();
    }
    private void OnStartNewGameEvent (int obj)
    {
        itemAvailableDict.Clear();
        interactiveStateDict.Clear();
    }

    private void OnBeforeSceneUnloadEvent() {
        foreach(var item in FindObjectsOfType<Item>()) {
            if (!itemAvailableDict.ContainsKey(item.itemName)) {
                itemAvailableDict.Add(item.itemName, true);
            }
        }
        foreach(var item in FindObjectsOfType<Interactive>())
        {
            if(interactiveStateDict.ContainsKey(item.name))
            {
                interactiveStateDict[item.name] = item.isDone;
            }
            else
            {
                interactiveStateDict.Add(item.name, item.isDone);
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
        foreach(var item in FindObjectsOfType<Interactive>())
        {
            if(interactiveStateDict.ContainsKey(item.name))
            {
                item.isDone = interactiveStateDict[item.name];
            }
            else
            {
                interactiveStateDict.Add(item.name, item.isDone);
            }
        }
    }

    private void OnUpdadeUIEvent(ItemDetails itemDetails, int index) {
        if (itemDetails != null) {
            itemAvailableDict[itemDetails.itemName] = false;
        }
    }

  public GameSaveData GenerateSaveData()
  {
    GameSaveData saveData = new GameSaveData();
    saveData.itemAvailableDict = this.itemAvailableDict;
    saveData.interactiveStateDict = this.interactiveStateDict;
    return saveData;
  }

  public void RestoreGameData(GameSaveData saveData)
  {
    this.itemAvailableDict = saveData.itemAvailableDict;
    this.interactiveStateDict = saveData.interactiveStateDict;
  }
}

using System.Collections.Generic;

public class GameSaveData
{
    public int gameweek;
    public string currentScene;
    public Dictionary<string, bool> miniGameStateDict;
    public Dictionary<ItemName, bool> itemAvailableDict = new Dictionary<ItemName, bool>();
    public Dictionary<string, bool> interactiveStateDict = new Dictionary<string, bool>();
    public List<ItemName> itemList;
}

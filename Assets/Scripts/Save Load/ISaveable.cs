public interface ISaveable
{
    void SaveableRegister()
    {
        SaveLoadManager._instance.Register(this);
    }
    GameSaveData GenerateSaveData();

    void RestoreGameData(GameSaveData saveData);
}

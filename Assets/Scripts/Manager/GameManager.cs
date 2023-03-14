using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private Dictionary<string, bool> miniGameStateDict = new Dictionary<string, bool>();

    private void OnEnable ()
    {
        EventHandler.AfterSceneLoadEvent += OnAfterSceneLoadEvent;
        EventHandler.GamePassEvent += OnGamePassEvent;
    }

    private void OnDisable ()
    {
        EventHandler.AfterSceneLoadEvent -= OnAfterSceneLoadEvent;
        EventHandler.GamePassEvent -= OnGamePassEvent;
    }

    void Start()
    {
        SceneManager.LoadScene("Menu", LoadSceneMode.Additive);
        EventHandler.CallGameStateChangeEvent(GameState.GamePlay);
    }
    
    private void OnAfterSceneLoadEvent ()
    {
        foreach (var miniGame in FindObjectsOfType<MiniGame>())
        {
            if (miniGameStateDict.TryGetValue(miniGame.gameName, out bool isPass))
            {
                miniGame.isPass = isPass;
                miniGame.UpdateMiniGameState();
            }
        }
    }
    private void OnGamePassEvent (string gameName)
    {
        miniGameStateDict[gameName] = true;
    }
}

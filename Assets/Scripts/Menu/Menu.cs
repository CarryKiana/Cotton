using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void QuitGame ()
    {
        Application.Quit();
    }

    public void ContinueGame ()
    {
        // 加载游戏进度
    }

    public void GoBackToMenu ()
    {
        var currentScene = SceneManager.GetActiveScene().name;
        TransitionManager._instance.Transtion(currentScene, "Menu");
        // 保存游戏进度
    }
}

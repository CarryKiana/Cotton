using UnityEngine;

public class Teleport : MonoBehaviour
{
    public string sceneFrom;
    public string sceneToGo;
    public void TeleportToScene() {
        TransitionManager._instance.Transtion(sceneFrom, sceneToGo);
    }
}

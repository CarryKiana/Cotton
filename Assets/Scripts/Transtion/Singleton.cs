using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
   public static T _instance;
   protected virtual void Awake() {
    if (_instance == null) {
        _instance = (T)this;
    }
   }
}

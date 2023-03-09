using UnityEngine;
using DG.Tweening;

public class H2AReset : Interactive
{
    private Transform gearSprite;

    private void Awake ()
    {
        gearSprite = transform.GetChild(0);
    }

  public override void EmptyClicked()
  {
    // 重置游戏
    GameController._instance.ResetGame();
    gearSprite.DOPunchRotation(Vector3.forward*180,1,1,0);
  }
}

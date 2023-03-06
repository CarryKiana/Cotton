using UnityEngine;

public class GameController : MonoBehaviour
{
   public GameH2A_SO gameData;
   public GameObject LineParent;
   public LineRenderer linePrefab;
   public Ball ballPrefab;
   public Transform[] holdersTramsforms;

   public void Start()
   {
        DrawLine();
        CreateBall();
   }
   public void DrawLine()
   {
        foreach(var connections in gameData.lineConnections)
        {
            var line = Instantiate(linePrefab, LineParent.transform);
            line.SetPosition(0, holdersTramsforms[connections.from].position);
            line.SetPosition(1, holdersTramsforms[connections.to].position);
        }
   }
   public void CreateBall()
   {
        for(int i = 0; i < gameData.startBallOrder.Count; i++)
        {
            if(gameData.startBallOrder[i] == BallName.None)
            {
                holdersTramsforms[i].GetComponent<Holder>().isEmpty = true;
                continue;
            } 
            Ball ball = Instantiate(ballPrefab, holdersTramsforms[i]);
            holdersTramsforms[i].GetComponent<Holder>().isEmpty = false;
            ball.SetupBall(gameData.GetBallDetails(gameData.startBallOrder[i]));
        }
   }
}

using UnityEngine;
using UnityEngine.Events;
public class GameController : Singleton<GameController>
{
   public UnityEvent OnFinish;
   [Header("游戏数据")]
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
   private void OnEnable ()
   {
        EventHandler.CheckGameStateEvent += OnCheckGameStateEvent;
   }
   private void OnDisable ()
   {
        EventHandler.CheckGameStateEvent -= OnCheckGameStateEvent;
   }
   public void DrawLine()
   {
        foreach(var connections in gameData.lineConnections)
        {
            var line = Instantiate(linePrefab, LineParent.transform);
            line.SetPosition(0, holdersTramsforms[connections.from].position);
            line.SetPosition(1, holdersTramsforms[connections.to].position);

            // 创建每个Holder的连线关系
            holdersTramsforms[connections.from].GetComponent<Holder>().linkHolders.Add(holdersTramsforms[connections.to].GetComponent<Holder>());
            holdersTramsforms[connections.to].GetComponent<Holder>().linkHolders.Add(holdersTramsforms[connections.from].GetComponent<Holder>());
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
            holdersTramsforms[i].GetComponent<Holder>().CheckBall(ball);
            holdersTramsforms[i].GetComponent<Holder>().isEmpty = false;
            ball.SetupBall(gameData.GetBallDetails(gameData.startBallOrder[i]));
        }
   }
   private void OnCheckGameStateEvent ()
   {
        foreach (var ball in FindObjectsOfType<Ball>())
        {
            if (!ball.isMatch)
            return;
        }
        Debug.Log("游戏结束");
        foreach (var holder in holdersTramsforms)
        {
          holder.GetComponent<Collider2D>().enabled = false;
        }
        OnFinish?.Invoke();
   }
   public void ResetGame()
   {
     for (int i = 0; i < LineParent.transform.childCount; i++)
     {
          Destroy(LineParent.transform.GetChild(i).gameObject);
     }
     foreach (var holder in holdersTramsforms)
     {
          if(holder.childCount > 0)
          {
               Destroy(holder.GetChild(0).gameObject);
          }
     }
     DrawLine();
     CreateBall();
   }
}

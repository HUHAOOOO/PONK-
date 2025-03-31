using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

public class BotArea : CoreMonoBehaviour
{
    private static BotArea instance;
    public static BotArea Instance => instance;

    [SerializeField] protected BallCtrl ballCtrl;
    [SerializeField] protected Transform GOCoreWordBot;


    [SerializeField] protected Transform posBall;
    [SerializeField] protected WorldAreaType currentArea = WorldAreaType.noAreaType;
    [SerializeField] protected WorldAreaType previousArea = WorldAreaType.noAreaType;
    [SerializeField] protected WorldAreaType oldArea = WorldAreaType.noAreaType;

    [SerializeField] protected bool topPoint;
    [SerializeField] protected bool rightPoint;
    [SerializeField] protected bool bottomPoint;
    [SerializeField] protected bool leftPoint;

    [SerializeField] protected bool isStartNewArea;

    [SerializeField] protected bool isClockwise = true;// khi bi speed bong +(false) or -(true)

    public WorldAreaType CurrentArea => currentArea;
    public WorldAreaType PreviousArea => previousArea;
    public bool IsClockwise => isClockwise;

    public bool TopPoint => topPoint;
    public bool RightPoint => rightPoint;
    public bool BottomPoint => bottomPoint;
    public bool LeftPoint => leftPoint;

    protected override void Awake()
    {
        if (instance != null) Debug.LogError("only allow 1 BotArea | Singleton");
        BotArea.instance = this;

        //[ ]
        //InitGame();
    }

    private void Update()
    {
        GetPosBall();
        AreaPreCur();
        RotatePoints();
        IsClocwiseUpdate();

    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadBallCtrl();
        //
        LoadCoreWordBot();
    }

    protected virtual void LoadBallCtrl()
    {
        if (this.ballCtrl != null) return;
        ballCtrl = FindAnyObjectByType<BallCtrl>();
        Debug.LogWarning(transform.name + ": LoadBallCtrl", gameObject);
    }

    protected virtual void LoadCoreWordBot()
    {
        if (this.GOCoreWordBot != null) return;
        GOCoreWordBot = transform.Find("CoreWordBot").GetComponent<Transform>();
        Debug.LogWarning(transform.name + ": LoadCoreWordBot", gameObject);

    }
    private void GetPosBall()
    {
        posBall = ballCtrl.CurrentBall.transform;
    }

    private void IsClocwiseUpdate()
    {
        if (ballCtrl.BallRotate.SpeedRotate >= 0)
        {
            isClockwise = false;
        }
        else if (ballCtrl.BallRotate.SpeedRotate < 0)
        {
            isClockwise = true;
        }
    }

    public virtual WorldAreaType GetAreaBall()
    {
        Vector3 localPos = this.GOCoreWordBot.transform.InverseTransformPoint(posBall.transform.position);

        if (localPos.x > 0 && localPos.y > 0)
        {
            //Debug.Log("arrea1");
            return WorldAreaType.Area1;
        }
        else if (localPos.x > 0 && localPos.y < 0)
        {
            //Debug.Log("arrea2");
            return WorldAreaType.Area2;
        }
        else if (localPos.x < 0 && localPos.y < 0)
        {
            //Debug.Log("arrea3");
            return WorldAreaType.Area3;
        }
        else if (localPos.x < 0 && localPos.y > 0)
        {
            //Debug.Log("arrea4");
            return WorldAreaType.Area4;
        }

        return WorldAreaType.noAreaType;
    }

    protected virtual void AreaPreCur()
    {
        WorldAreaType areaType = GetAreaBall();
        if (currentArea == areaType) return;
        currentArea = areaType;

        previousArea = GetRotatedEnum(currentArea, isClockwise);
    }

    protected virtual void RotatePoints()
    {
        topPoint = (currentArea == (isClockwise ? WorldAreaType.Area1 : WorldAreaType.Area4)) &&
               (previousArea == (isClockwise ? WorldAreaType.Area4 : WorldAreaType.Area1));

        rightPoint = (currentArea == (isClockwise ? WorldAreaType.Area2 : WorldAreaType.Area1)) &&
                     (previousArea == (isClockwise ? WorldAreaType.Area1 : WorldAreaType.Area2));

        bottomPoint = (currentArea == (isClockwise ? WorldAreaType.Area3 : WorldAreaType.Area2)) &&
                      (previousArea == (isClockwise ? WorldAreaType.Area2 : WorldAreaType.Area3));

        leftPoint = (currentArea == (isClockwise ? WorldAreaType.Area4 : WorldAreaType.Area3)) &&
                    (previousArea == (isClockwise ? WorldAreaType.Area3 : WorldAreaType.Area4));

    }
    public static WorldAreaType GetRotatedEnum(WorldAreaType current, bool clockwise)
    {
        WorldAreaType[] values = (WorldAreaType[])Enum.GetValues(typeof(WorldAreaType));
        List<WorldAreaType> valuesList = values.ToList();
        valuesList.RemoveAt(0);

        int index = valuesList.IndexOf(current);

        index += clockwise ? -1 : 1;// clockwise(true) => -1 , clockwise(false) => 1

        if (index < 0) index = valuesList.Count - 1;
        if (index >= valuesList.Count) index = 0;

        return valuesList[index];
    }


    public bool IsStartNewArea()
    {
        if (oldArea == this.currentArea)
        {
            isStartNewArea = false; 
            return false;
        }
        else
        {
            oldArea = this.currentArea;
            isStartNewArea = true;
            return true;
        }
    }
}

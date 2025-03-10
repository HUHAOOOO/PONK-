using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class GameManager : CoreMonoBehaviour
{

    private static GameManager instance;
    public static GameManager Instance => instance;

    [SerializeField] protected BallCtrl ballCtrl;


    [SerializeField] protected Vector3 posBall;
    [SerializeField] protected WorldAreaType currentArea = WorldAreaType.noAreaType;
    [SerializeField] protected WorldAreaType previousArea = WorldAreaType.noAreaType;

    [SerializeField] protected bool topPoint;
    [SerializeField] protected bool rightPoint;
    [SerializeField] protected bool bottomPoint;
    [SerializeField] protected bool leftPoint;

    [SerializeField] protected bool isClockwise = true;// khi bi speed bong +(false) or -(true)

    [SerializeField] protected List<Transform> pos4Players = new();
    [SerializeField] protected List<Transform> pos4GO = new();
    [SerializeField] protected List<CharCtrl> players = new();
    [SerializeField] protected int indexPos;

    public WorldAreaType CurrentArea => currentArea;
    public WorldAreaType PreviousArea => previousArea;
    public bool IsClockwise => isClockwise;

    public bool TopPoint => topPoint;
    public bool RightPoint => rightPoint;
    public bool BottomPoint => bottomPoint;
    public bool LeftPoint => leftPoint;

    protected override void Awake()
    {
        if (instance != null) Debug.LogError("only allow 1 GameManager | Singleton");
        GameManager.instance = this;

        //[ ]
        //InitGame();
    }

    protected virtual void InitGame()
    {
        int indexRandom = UnityEngine.Random.Range(0, pos4GO.Count);
        ballCtrl.CurrentBall.position = pos4GO[indexRandom].position;
        //ballCtrl.Ball.rotation = pos4GO[indexRandom].rotation;
    }

    private void Update()
    {
        GetPosBall();
        AreaPreCur();
        RotatePoints();
        IsClocwiseUpdate();

        //PlayerIsCanOverlapCircleMeleeAttack();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadBallCtrl();
        LoadPos4Players();
        LoadPlayers();
        LoadPos4GO();
        //
        indexPos = pos4Players.Count;
        SetPosForPlayer();
    }

    protected virtual void LoadBallCtrl()
    {
        if (this.ballCtrl != null) return;
        ballCtrl = FindAnyObjectByType<BallCtrl>();
        Debug.LogWarning(transform.name + ": LoadBallCtrl", gameObject);
    }
    protected virtual void LoadPos4Players()
    {
        if (this.pos4Players.Count > 0) return;
        Transform poss = transform.Find("Position4Player");
        Debug.LogWarning(transform.name + ": LoadPos4Players", gameObject);

        foreach (Transform child in poss)
        {
            pos4Players.Add(child);
            Debug.Log($"pos4Players.Add ({child.name})");
        }
    }


    protected virtual void LoadPos4GO()
    {
        if (this.pos4GO.Count > 0) return;
        Transform poss = transform.Find("Pos4GO");
        Debug.LogWarning(transform.name + ": LoadPos4GO", gameObject);

        foreach (Transform child in poss)
        {
            pos4GO.Add(child);
            Debug.Log($"pos4GO.Add ({child.name})");
        }
    }

    protected virtual void LoadPlayers()
    {
        if (this.players.Count > 0) return;
        CharCtrl[] listPlayers = FindObjectsByType<CharCtrl>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
        //FindObjectsInactive.Exclude // GO active
        //FindObjectsInactive.Include // GO inactive
        this.players = listPlayers.ToList();
        Debug.LogWarning(transform.name + ": LoadPlayers", gameObject);
    }

    private void GetPosBall()
    {
        posBall = ballCtrl.CurrentBall.transform.position;
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
        if (posBall.x > 0 && posBall.y > 0)
        {
            //Debug.Log("arrea1");
            return WorldAreaType.Area1;
        }
        else if (posBall.x > 0 && posBall.y < 0)
        {
            //Debug.Log("arrea2");
            return WorldAreaType.Area2;
        }
        else if (posBall.x < 0 && posBall.y < 0)
        {
            //Debug.Log("arrea3");
            return WorldAreaType.Area3;
        }
        else if (posBall.x < 0 && posBall.y > 0)
        {
            //Debug.Log("arrea4");
            return WorldAreaType.Area4;
        }

        return WorldAreaType.noAreaType;
    }


    public virtual void SetPosForPlayer()
    {
        players[0].SetPosChar(PosAvailable());
        players[1].SetPosChar(PosAvailable());
        players[2].SetPosChar(PosAvailable());
        players[3].SetPosChar(PosAvailable());
    }

    public virtual Transform PosAvailable()
    {
        if (indexPos <= 0) return null;
        indexPos--;
        return pos4Players[indexPos];
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

        ////chieu kim dong ho
        //if (isClockwise)
        //{
        //    topPoint = (currentArea == WorldAreaType.Area1 && previousArea == WorldAreaType.Area4);
        //    rightPoint = (currentArea == WorldAreaType.Area2 && previousArea == WorldAreaType.Area1);
        //    bottomPoint = (currentArea == WorldAreaType.Area3 && previousArea == WorldAreaType.Area2);
        //    leftPoint = (currentArea == WorldAreaType.Area4 && previousArea == WorldAreaType.Area3);
        //}
        //// nguoc chieu kim dong ho
        //else if (!isClockwise)
        //{
        //    topPoint = (currentArea == WorldAreaType.Area4 && previousArea == WorldAreaType.Area1);
        //    rightPoint = (currentArea == WorldAreaType.Area1 && previousArea == WorldAreaType.Area2);
        //    bottomPoint = (currentArea == WorldAreaType.Area2 && previousArea == WorldAreaType.Area3);
        //    leftPoint = (currentArea == WorldAreaType.Area3 && previousArea == WorldAreaType.Area4);
        //}
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

    //private void PlayerIsCanOverlapCircleMeleeAttack()
    //{
    //    // if isFireBall => can not overlap circle melee attack
    //    if (ballCtrl.CurrentBall == ballCtrl.BallsModel[2])
    //    {
    //        foreach (CharCtrl charCtrl in players)
    //        {
    //            charCtrl.CharMeleeAttack.IsCanOverlapCircleMeleeAttack = false;
    //            Debug.Log("charCtrl.CharMeleeAttack.IsCanOverlapCircleMeleeAttack = false;");
    //        }
    //    }
    //    else
    //    {
    //        foreach (CharCtrl charCtrl in players)
    //        {
    //            charCtrl.CharMeleeAttack.IsCanOverlapCircleMeleeAttack = true;
    //            Debug.Log("charCtrl.CharMeleeAttack.IsCanOverlapCircleMeleeAttack = true;");
    //        }
    //    }

    //}

    public void SetPlayerIsCanOverlapCircleMeleeAttack(bool isCanOverlapCircleMeleeAttack)
    {
        foreach (CharCtrl charCtrl in players)
        {
            charCtrl.CharMeleeAttack.IsCanOverlapCircleMeleeAttack = isCanOverlapCircleMeleeAttack;
        }
    }
}

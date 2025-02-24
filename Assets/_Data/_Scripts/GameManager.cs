using Mono.Cecil;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using static Unity.VisualScripting.Metadata;
using static UnityEditor.Experimental.GraphView.GraphView;
using Object = System.Object;

public class GameManager : CoreMonoBehaviour
{

    private static GameManager instance;
    public static GameManager Instance => instance;

    [SerializeField] protected BallCtrl ballCtrl;


    [SerializeField] protected Vector3 posBall;
    [SerializeField] protected WorldAreaType currentArea = WorldAreaType.noAreaType;
    [SerializeField] protected WorldAreaType previousArea = WorldAreaType.noAreaType;

    [SerializeField] protected Transform cubeCorePoint;


    [SerializeField] protected List<Transform> pos4Players = new();
    [SerializeField] protected List<CharCtrl> players = new();
    [SerializeField] protected int indexPos;

    public WorldAreaType CurrentArea => currentArea;
    public WorldAreaType PreviousArea => previousArea;
    public List<Transform> Pos4Players => pos4Players;
    protected override void Awake()
    {
        if (instance != null) Debug.LogError("only allow 1 GameManager | Singleton");
        GameManager.instance = this;
    }
    private void Update()
    {
        GetPosBall();

        AreaPreCur();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadBallCtrl();
        LoadCubeCorePoint();
        LoadPos4Players();
        LoadPlayers();

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

    protected virtual void LoadCubeCorePoint()
    {
        if (this.cubeCorePoint != null) return;
        cubeCorePoint = transform.Find("CubeCorePoint");
        Debug.LogWarning(transform.name + ": LoadCubeCorePoint", gameObject);
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
    protected virtual void LoadPlayers()
    {
        if (this.players.Count > 0) return;
        CharCtrl[] listPlayers = FindObjectsByType<CharCtrl>(FindObjectsInactive.Exclude,FindObjectsSortMode.None);
        //FindObjectsInactive.Exclude // GO active
        //FindObjectsInactive.Include // GO inactive
        this.players = listPlayers.ToList();
        Debug.LogWarning(transform.name + ": LoadPlayers", gameObject);
    }

    private void GetPosBall()
    {
        posBall = ballCtrl.Ball.transform.position;
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
    // PosAvaiable(); // chi cho 1 vi tri va cho r thi het ko cho nx
    // [ ] todo xu li nhieu (4) player thi sao ? ... 
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
        previousArea = GameManager.GetPreviousEnum_RotateKimDongHo(currentArea);
    }


    //enum
    public static WorldAreaType GetPreviousEnum_RotateKimDongHo(WorldAreaType current)
    {
        // lay all gia tri Enum kieu WorldAreaType -> object[] -> ep lai (WorldAreaType[])
        WorldAreaType[] values = (WorldAreaType[])Enum.GetValues(typeof(WorldAreaType));

        List<WorldAreaType> valuesList = values.ToList();
        //Debug.Log("valuesList.RemoveAt(0); : " + valuesList[0]);
        valuesList.RemoveAt(0);
        int index = valuesList.IndexOf(current) - 1;

        if (index < 0) index = valuesList.Count - 1;
        //Debug.Log("valuesList[index] : " + index +"=>" + valuesList[index]);
        return valuesList[index];
    }
    // [ ] to do lam sao ghep duoc 2 -> 1 ta
    //public static WorldAreaType GetPreviousEnum_RotateNguocKimDongHo(WorldAreaType current)
    //{
    //    // lay all gia tri Enum kieu WorldAreaType -> object[] -> ep lai (WorldAreaType[])
    //    WorldAreaType[] values = (WorldAreaType[])Enum.GetValues(typeof(WorldAreaType));

    //    // tim vi tri current trong mang values
    //    // -1 : lay gia tri truoc current
    //    // +1 lay gia tri sau current
    //    int index = Array.IndexOf(values, current) + 1;//+1

    //    //return (index < values.Length) ? values[index] : values[4]; // Neu het thi quay lai dau
    //    return (index < values.Length) ? values[index] : values[0]; // Neu het thi quay lai dau
    //}

}

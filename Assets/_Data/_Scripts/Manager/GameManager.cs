using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using UnityEngine;
using static UnityEngine.EventSystems.StandaloneInputModule;


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
    //[SerializeField] protected int indexPos;

    [SerializeField] protected bool isP0Die = false;
    [SerializeField] protected bool isP1Die = false;
    [SerializeField] protected bool isP2Die = false;
    [SerializeField] protected bool isP3Die = false;
    [SerializeField] protected int countDie = 0;
    [SerializeField] protected int countToEndGame = 0;


    public WorldAreaType CurrentArea => currentArea;
    public WorldAreaType PreviousArea => previousArea;
    public bool IsClockwise => isClockwise;

    public bool TopPoint => topPoint;
    public bool RightPoint => rightPoint;
    public bool BottomPoint => bottomPoint;
    public bool LeftPoint => leftPoint;
    public List<CharCtrl> Players => players;


    public bool IsP0Die { get => isP0Die; set => isP0Die = value; }
    public bool IsP1Die { get => isP1Die; set => isP1Die = value; }
    public bool IsP2Die { get => isP2Die; set => isP2Die = value; }
    public bool IsP3Die { get => isP3Die; set => isP3Die = value; }

    protected override void Awake()
    {
        if (instance != null) Debug.LogError("only allow 1 GameManager | Singleton");
        GameManager.instance = this;

        //InitGame();
    }
    protected override void OnEnable()
    {
        SetDefaultData();
    }
    // Caanf goi lai khi start lai game moi 

    public void SetDefaultData()
    {
        isP0Die = false;
        isP1Die = false;
        isP2Die = false;
        isP3Die = false;
        countDie = 0;
    }

    public void InitGame()
    {
        int indexRandom = UnityEngine.Random.Range(0, pos4GO.Count);

        //Vector3 newPos = pos4GO[indexRandom].position;
        //Vector3 newPos = pos4GO[indexRandom].localPosition;
        //if (newPos.x > 0) newPos.x = 12;
        //else if (newPos.x < 0) newPos.x = -12;
        //newPos.y = 0;

        //ballCtrl.CurrentBall.transform.parent.localPosition = newPos;
        //ballCtrl.CurrentBall.transform.parent.localPosition = pos4GO[indexRandom].localPosition; // ko chinh xac vi tri dc spawn TOAN spawn gan sat player ?  
        ballCtrl.CurrentBall.transform.parent.position = pos4GO[indexRandom].position;
        //ballCtrl.CurrentBall.transform.parent.rotation = pos4GO[indexRandom].rotation * Quaternion.Euler(0, 0, 90f); //+ Z 90 do


        //Quaternion baseRot = pos4GO[indexRandom].rotation;
        //Vector3 euler = baseRot.eulerAngles;
        //euler.z += 90f;
        //ballCtrl.CurrentBall.transform.parent.rotation = Quaternion.Euler(euler);

        //ballCtrl.CurrentBall.transform.parent.position = newPos;
        ballCtrl.BallRotate.InitRotate();
        //Vector3 newPos = ballCtrl.CurrentBall.transform.parent.position;
        //newPos.x = pos4GO[indexRandom].position.x;
        //newPos.y = pos4GO[indexRandom].position.y;
        //newPos.y = 0;
        //newPos.z = 0;
        //ballCtrl.CurrentBall.transform.parent.transform.position = newPos;

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

    protected override void Start()
    {
        DataCHarIntoGame();
    }
    // Vao game phai cap nhat lai NAME do 
    public void DataCHarIntoGame()
    {
        SOInfoPlayer newSOInfoPlayer = ScriptableObject.CreateInstance<SOInfoPlayer>();
        for (int i = 0; i < players.Count; i++)
        {
            newSOInfoPlayer = SaveLoadManager.Instance.GetDataByIndex(i);

            players[i].NamePlayer.text = newSOInfoPlayer.nameP;
        }
    }

    protected override void LoadComponents()
    {
        GameManager.instance = this;

        base.LoadComponents();
        LoadBallCtrl();
        LoadPos4Players();
        LoadPlayers();
        LoadPos4GO();
        //
        //indexPos = pos4Players.Count;
        SetPosForPlayer();
        SetPlayerIndexTypeForPlayer();
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
        //this.players = listPlayers.ToList();
        this.players = listPlayers.OrderBy(p => p.transform.name).ToList();
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
        for (int i = 0; i < players.Count; i++)
        {
            players[i].SetPosChar(PosAvailable(i), i);
        }
    }
    public virtual void SetPlayerIndexTypeForPlayer()
    {
        for (int i = 0; i < players.Count; i++)
        {
            players[i].PlayerIndexType = PlayerIndexTypeExtensions.IndexToPlayerIndexType(i);
        }
    }

    public virtual Transform PosAvailable(int indexPos)
    {
        //SetPosPiIsOn(indexPos);
        return pos4Players[indexPos];
    }
    //public virtual void SetPosPiIsOn(int indexPosIsOn)
    //{
    //    if (indexPosIsOn == 0)
    //    {
    //        posP1IsOn = true;
    //    }
    //    else if (indexPosIsOn == 1)
    //    {
    //        posP2IsOn = true;
    //    }
    //    else if (indexPosIsOn == 2)
    //    {
    //        posP3IsOn = true;
    //    }
    //    else if (indexPosIsOn == 3)
    //    {
    //        posP4IsOn = true;
    //    }
    //}
    //// Khi player Dead goi lai 
    //public virtual void SetPosPiOff(int indexPosOff)
    //{
    //    if (indexPosOff == 0)
    //    {
    //        posP1IsOn = false;
    //    }
    //    else if (indexPosOff == 1)
    //    {
    //        posP2IsOn = false;
    //    }
    //    else if (indexPosOff == 2)
    //    {
    //        posP3IsOn = false;
    //    }
    //    else if (indexPosOff == 3)
    //    {
    //        posP4IsOn = false;
    //    }
    //}

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

    public void SetPlayerIsCanOverlapCircleMeleeAttack(bool isCanOverlapCircleMeleeAttack)
    {
        foreach (CharCtrl charCtrl in players)
        {
            //Debug.Log("GameManager : " + charCtrl.transform.name + " : " + isCanOverlapCircleMeleeAttack);

            charCtrl.CharMeleeAttack.IsCanOverlapCircleMeleeAttack = isCanOverlapCircleMeleeAttack;
        }
    }


    public void SetDiePlayerByPlayerIndexType(PlayerIndexType playerIndexType)
    {
        if (playerIndexType == PlayerIndexType.P0)
        {
            isP0Die = true;
        }
        if (playerIndexType == PlayerIndexType.P1)
        {
            isP1Die = true;
        }
        if (playerIndexType == PlayerIndexType.P2)
        {
            isP2Die = true;
        }
        if (playerIndexType == PlayerIndexType.P3)
        {
            isP3Die = true;
        }

        countDie++;
        if (countDie == countToEndGame)
        {
            // End Game
            // Show name Player win
            //Time.timeScale = 0;
            PlayerIndexType playerIndexTypeWINER = GetIndexPlayerWin();
            string nameWINER = SaveLoadManager.Instance.GetDataByPlayerIndexType(playerIndexTypeWINER).nameP;
            CANVAS_CTRL.Instance.EndGame(nameWINER);
        }
    }

    private PlayerIndexType GetIndexPlayerWin()
    {
        if (isP0Die == false) return PlayerIndexType.P0;
        else if (isP1Die == false) return PlayerIndexType.P1;
        else if (isP2Die == false) return PlayerIndexType.P2;
        else if (isP3Die == false) return PlayerIndexType.P3;
        return PlayerIndexType.None;
    }





    // 0 1 2 3 

    public void SetActivePlayer(int soluong)
    {
        if (soluong == 1)
        {
            players[0].gameObject.SetActive(true);
            countToEndGame = 1000;//no end
            players[1].gameObject.SetActive(false);
            players[2].gameObject.SetActive(false);
            players[3].gameObject.SetActive(false);
        }
        else if (soluong == 2)
        {
            players[0].gameObject.SetActive(true);
            players[2].gameObject.SetActive(true);
            countToEndGame = 1;
            players[1].gameObject.SetActive(false);
            players[3].gameObject.SetActive(false);

        }
        if (soluong == 3)
        {
            players[0].gameObject.SetActive(true);
            players[1].gameObject.SetActive(true);
            players[2].gameObject.SetActive(true);
            countToEndGame = 2;
            players[3].gameObject.SetActive(false);
        }
        if (soluong == 4)
        {
            players[0].gameObject.SetActive(true);
            players[1].gameObject.SetActive(true);
            players[2].gameObject.SetActive(true);
            players[3].gameObject.SetActive(true);
            countToEndGame = 3;
        }
    }

}

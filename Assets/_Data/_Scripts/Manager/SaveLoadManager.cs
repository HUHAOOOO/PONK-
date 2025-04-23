using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SaveLoadManager : CoreMonoBehaviour
{
    [SerializeField] protected List<SOInfoPlayer> soDefaultInfoPlayers;
    [SerializeField] protected List<SOInfoPlayer> soNewInfoPlayers;

    public Image spriteCharTest;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadSODefaultInfoPlayers();
        LoadSONewInfoPlayers();

        //
        Save();
        Load();
    }
    protected virtual void LoadSODefaultInfoPlayers()
    {
        if (this.soDefaultInfoPlayers.Count > 0) return;
        SOInfoPlayer[] soDefaultInfoPlayers = Resources.LoadAll<SOInfoPlayer>("SO/DefaultInFoSO");

        this.soDefaultInfoPlayers = soDefaultInfoPlayers.ToList();
        Debug.LogWarning(transform.name + ": LoadSODefaultInfoPlayers", gameObject);
    }
    protected virtual void LoadSONewInfoPlayers()
    {
        if (this.soNewInfoPlayers.Count > 0) return;
        SOInfoPlayer[] soDefaultInfoPlayers = Resources.LoadAll<SOInfoPlayer>("SO/NewInFoSO");

        this.soNewInfoPlayers = soDefaultInfoPlayers.ToList();
        //soNewInfoPlayers = (List<SOInfoPlayer>)soDefaultInfoPlayers.Clone();
        Debug.LogWarning(transform.name + ": LoadSONewInfoPlayers", gameObject);
    }

    protected override void Awake()
    {
        SaveSystem.Init();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Save();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            Load();
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            LoadTestSprite();
        }
    }
    private void LoadTestSprite()
    {
        spriteCharTest.sprite = soNewInfoPlayers[0].spriteP;
        // ok load dc . NHUNG anh bi vo 
    }
    private void Save()
    {
        // v1
        //InForPlayerDummy inForPlayerDummyData = new InForPlayerDummy();
        //inForPlayerDummyData.playerIndexType = soDefaultInfoPlayers[0].playerIndexType;
        //inForPlayerDummyData.nameP = soDefaultInfoPlayers[0].nameP;
        //inForPlayerDummyData.keyPairP = soDefaultInfoPlayers[0].keyPairP;
        // v2 Save one piece of data to JSON
        //InForPlayerDummy inForPlayerDummyData = soDefaultInfoPlayers[0].ToData();
        //string jsonStringData = JsonUtility.ToJson(inForPlayerDummyData, true);
        //SaveSystem.Save(jsonStringData);
        //Debug.Log("Da save jsonString tu inForPlayerDummyData[0]");


        ////v3 Save multiple pieces of data
        //SOInfoPlayerList soInfoPlayerList = new();
        //int i = 0;
        //foreach (SOInfoPlayer so in soDefaultInfoPlayers)
        //{
        //    soInfoPlayerList.data.Add(soDefaultInfoPlayers[i]);
        //    i++;
        //}

        InForPlayerDummyList inForPlayerDummyList = new();
        //// foreach
        //int i = 0;
        //foreach (SOInfoPlayer so in soDefaultInfoPlayers)
        //{
        //    inForPlayerDummyList.data.Add(so.ToData());
        //    i++;
        //}
        //// for
        for (int i = 0; i < soDefaultInfoPlayers.Count; i++)
        {
            Debug.Log("soDefaultInfoPlayers : " + i);

            inForPlayerDummyList.data.Add(soDefaultInfoPlayers[i].ToData());
        }

        string jsonStringData = JsonUtility.ToJson(inForPlayerDummyList, true);
        SaveSystem.Save(jsonStringData);

        Debug.Log("Da save jsonString xuong");
    }

    private void Load()
    {
        ////v3
        string saveDataString = SaveSystem.Load();
        if (saveDataString == null) return;
        InForPlayerDummyList inForPlayerDummyList = JsonUtility.FromJson<InForPlayerDummyList>(saveDataString);
        
        ///// for
        for (int i = 0; i < soNewInfoPlayers.Count; i++)
        {
            soNewInfoPlayers[i].LoadFromData(inForPlayerDummyList.data[i]);
        }

        ///// foreach
        //int i = 0;
        //foreach (InForPlayerDummy inForPlayerDummy in inForPlayerDummyList.data)
        //{
        //    soNewInfoPlayers[i].LoadFromData(inForPlayerDummy);
        //    i++;
        //    //Debug.Log($"inForPlayerDummy : " + inForPlayerDummy.nameP);
        //}

        ////v2
        //string saveDataString = SaveSystem.Load();
        //if (saveDataString == null) return;
        //InForPlayerDummy inForPlayerDummyData = JsonUtility.FromJson<InForPlayerDummy>(saveDataString);

        //soNewInfoPlayers[0].LoadFromData(inForPlayerDummyData);

        ////v1
        //soNewInfoPlayers[0].playerIndexType = inForPlayerDummyData.playerIndexType;
        //soNewInfoPlayers[0].nameP = inForPlayerDummyData.nameP;
        //soNewInfoPlayers[0].keyPairP = inForPlayerDummyData.keyPairP;
        //Debug.Log("Da load saveDataString vao soNewInfoPlayers[0]");

        Debug.Log("Da load saveDataString len");
    }

}

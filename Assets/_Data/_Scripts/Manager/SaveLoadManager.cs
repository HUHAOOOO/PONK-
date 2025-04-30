using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Unity.VisualScripting;
using UnityEditor.U2D.Animation;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;
using static UnityEngine.Rendering.VirtualTexturing.Debugging;

public class SaveLoadManager : CoreMonoBehaviour
{
    private static SaveLoadManager instance;
    public static SaveLoadManager Instance => instance;

    [SerializeField] protected List<SOInfoPlayer> soDefaultInfoPlayers;
    [SerializeField] protected List<SOInfoPlayer> soNewInfoPlayers;

    public List<SOInfoPlayer> SODefaultInfoPlayers => soDefaultInfoPlayers;
    public List<SOInfoPlayer> SONewInfoPlayers => soNewInfoPlayers;

    protected override void LoadComponents()
    {
        SaveLoadManager.instance = this;

        base.LoadComponents();
        LoadSODefaultInfoPlayers();
        LoadSONewInfoPlayers();

        ////
        //Save();
        //Load();
    }

    protected virtual void LoadSODefaultInfoPlayers()
    {

        string pathAddressables;// = "Assets/_Data/_Scripts/Resources_moved/SO/DefaultInFoSO/InforP0.asset";

        for (int i = 0; i < 4; i++)
        {
            pathAddressables = "Assets/_Data/_Scripts/Resources_moved/SO/DefaultInFoSO/InforP" + i + ".asset";
            AsyncOperationHandle<IList<SOInfoPlayer>> handle = Addressables.LoadAssetsAsync<SOInfoPlayer>(
                pathAddressables,
                (so) =>
            {
                soDefaultInfoPlayers.Add(so);
                Debug.Log($"Loaded SO: {so.name}");
            });

            handle.Completed += (op) =>
            {
                // Sau khi load xong và dùng xong thì release
                Addressables.Release(op);
                Debug.Log("release handle sau khi xong.");
            };
        }
        soDefaultInfoPlayers.OrderBy(p => p.name);


        //// Resources
        //if (this.soDefaultInfoPlayers.Count > 0) return;
        //SOInfoPlayer[] soDefaultInfoPlayers = Resources.LoadAll<SOInfoPlayer>("SO/DefaultInFoSO");

        //this.soDefaultInfoPlayers = soDefaultInfoPlayers.ToList();
        //Debug.LogWarning(transform.name + ": LoadSODefaultInfoPlayers", gameObject);
    }
    protected virtual void LoadSONewInfoPlayers()
    {
        string pathAddressables;// = "Assets/_Data/_Scripts/Resources_moved/SO/DefaultInFoSO/InforP0.asset";

        for (int i = 0; i < 4; i++)
        {
            pathAddressables = "Assets/_Data/_Scripts/Resources_moved/SO/NewInFoSO/InforP" + i + ".asset";
            AsyncOperationHandle<IList<SOInfoPlayer>> handle = Addressables.LoadAssetsAsync<SOInfoPlayer>(pathAddressables, (so) =>
            {
                soNewInfoPlayers.Add(so);
                Debug.Log($"Loaded SO: {so.name}");
            });

            handle.Completed += (op) =>
            {
                // Sau khi load xong và dùng xong thì release
                Addressables.Release(op);
                Debug.Log("release handle sau khi xong.");
            };
        }
        soNewInfoPlayers.OrderBy(p => p.name);



        //// Resources
        //if (this.soNewInfoPlayers.Count > 0) return;
        //SOInfoPlayer[] soDefaultInfoPlayers = Resources.LoadAll<SOInfoPlayer>("SO/NewInFoSO");

        //this.soNewInfoPlayers = soDefaultInfoPlayers.ToList();
        ////soNewInfoPlayers = (List<SOInfoPlayer>)soDefaultInfoPlayers.Clone();
        //Debug.LogWarning(transform.name + ": LoadSONewInfoPlayers", gameObject);
    }

    protected override void Awake()
    {
        if (instance != null) Debug.LogError("only allow 1 SaveLoadManager | Singleton");
        SaveLoadManager.instance = this;

        SaveSystem.Init();
    }
    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.S))
        //{
        //    Save();
        //}

        //if (Input.GetKeyDown(KeyCode.L))
        //{
        //    Load();
        //}
    }

    protected override void OnEnable()
    {
        //Save();
        Load();
    }
    protected override void OnDisable()
    {
        SaveEndNewSO();
    }
    private void Save() // Save default -> Json 
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
            Debug.Log("soDefaultInfoPlayers : " + i, gameObject);

            inForPlayerDummyList.data.Add(soDefaultInfoPlayers[i].ToData());
        }

        string jsonStringData = JsonUtility.ToJson(inForPlayerDummyList, true);
        SaveSystem.Save(jsonStringData);

        Debug.Log("Da save jsonString xuong", gameObject);
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

        Debug.Log("Da load saveDataString vao soNewInfoPlayers");
    }


    public void ResetDefaultInfoPlayer()
    {
        for(int i= 0; i < soNewInfoPlayers.Count; i++)
        {
            soNewInfoPlayers[i].CopyDataFromAnotherSO(soDefaultInfoPlayers[i]);
        }
    }


    public void SaveNewInfoToSO(PlayerIndexType playerIndexType,string nameP , KeyPair keyPair)
    {
        int index = playerIndexType.ToIndex();

        soNewInfoPlayers[index].nameP = nameP;

        SaveNewInfoKeyToSO(playerIndexType, keyPair);
    }
    public void SaveNewInfoKeyToSO(PlayerIndexType playerIndexType, KeyPair keyPair)
    {
        int index = playerIndexType.ToIndex();

        soNewInfoPlayers[index].keyPairP = keyPair;

        InputManager.Instance.UpdateKey4Pkayer();
    }

    public SOInfoPlayer GetDataByPlayerIndexType(PlayerIndexType playerIndexType)
    {
        if (playerIndexType == PlayerIndexType.None) return null;
        int index = playerIndexType.ToIndex();
        return soNewInfoPlayers[index];
    }

    public SOInfoPlayer GetDataByIndex(int index)
    {
        return soNewInfoPlayers[index];
    }

    //XXX
    public SOInfoPlayer GetDataDefaultByPlayerIndexType(PlayerIndexType playerIndexType)
    {
        if (playerIndexType == PlayerIndexType.None) return null;
        int index = playerIndexType.ToIndex();
        return soDefaultInfoPlayers[index];
    }




    public void SaveEndNewSO()
    {
        InForPlayerDummyList inForPlayerDummyList = new();

        for (int i = 0; i < soNewInfoPlayers.Count; i++)
        {
            //Debug.Log("soNewInfoPlayers : " + i, gameObject);

            inForPlayerDummyList.data.Add(soNewInfoPlayers[i].ToData());
        }

        string jsonStringData = JsonUtility.ToJson(inForPlayerDummyList, true);
        SaveSystem.Save(jsonStringData);

        Debug.Log("Da save jsonString xuong", gameObject);
        InputManager.Instance.ListKeyCodeForPlayer();
    }
}

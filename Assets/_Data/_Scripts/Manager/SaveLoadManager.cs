using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;
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
    }

    protected virtual void LoadSODefaultInfoPlayers()
    {

        string pathAddressables;

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
                // Sau khi load xong v� d�ng xong th� release
                Addressables.Release(op);
                Debug.Log("release handle sau khi xong.");
            };
        }
        soDefaultInfoPlayers.OrderBy(p => p.name);
    }
    protected virtual void LoadSONewInfoPlayers()
    {
        string pathAddressables;

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
                Addressables.Release(op);
                Debug.Log("release handle sau khi xong.");
            };
        }
        soNewInfoPlayers.OrderBy(p => p.name);
    }

    protected override void Awake()
    {
        if (instance != null) Debug.LogError("only allow 1 SaveLoadManager | Singleton");
        SaveLoadManager.instance = this;

        //SaveSystem.Init();
    }

    /// <summary>
    /// ///////////////////////////////////////////////
    /// </summary>
    [DllImport("__Internal")]
    private static extern void SetupBeforeUnload();

    protected override void Start()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        SetupBeforeUnload();
#endif
    }

    // Ham duoc goi tu JS khi tap sap dong
    public void OnTabClose()
    {
        Debug.Log("Tab is closing! Saving game...");
        SaveEndNewSO();
    }
    /// <summary>
    /// ////////////////////////////////////////////
    /// </summary>
    protected override void OnEnable()
    {
        Load();
    }
    void OnApplicationQuit()
    {
        Debug.Log("App quitting . SaveEndNewSO !");
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


    // Load Json to SO 
    private void Load()
    {
        //v3
        string saveDataString = SaveSystem.Load();
        if (saveDataString == null)
        {
            Debug.Log("Khong tim thay du lieu de Load");
            return;
        }
        InForPlayerDummyList inForPlayerDummyList = JsonUtility.FromJson<InForPlayerDummyList>(saveDataString);

        ///// for
        SOInfoPlayer newSOInfoPlayerClone = ScriptableObject.CreateInstance<SOInfoPlayer>();
        for (int i = 0; i < soNewInfoPlayers.Count; i++)
        {
            //newSOInfoPlayerClone.LoadFromData(inForPlayerDummyList.data[i]);
            //soNewInfoPlayers[i].CopyDataFromAnotherSO(newSOInfoPlayerClone);

            soNewInfoPlayers[i].LoadFromData(inForPlayerDummyList.data[i]);
        }
        Debug.Log("Da load saveDataString vao soNewInfoPlayers");
    }


    //// v1 run on Unity Editor OK
    //private void Load()
    //{
    //    ////v3
    //    string saveDataString = SaveSystem.Load();
    //    if (saveDataString == null)
    //    {
    //        Debug.Log("Khong tim thay du lieu de Load");
    //        return;
    //    }
    //    InForPlayerDummyList inForPlayerDummyList = JsonUtility.FromJson<InForPlayerDummyList>(saveDataString);

    //    //InForPlayerDummyList inForPlayerDummyList;
    //    //inForPlayerDummyList.data = JsonUtility.FromJson<InForPlayerDummyList>(saveDataString);

    //    ///// for
    //    SOInfoPlayer newSOInfoPlayerClone = ScriptableObject.CreateInstance<SOInfoPlayer>();
    //    for (int i = 0; i < soNewInfoPlayers.Count; i++)
    //    {
    //        //newSOInfoPlayerClone.LoadFromData(inForPlayerDummyList.data[i]);
    //        ////soNewInfoPlayers[i] = newSOInfoPlayerClone;
    //        //soNewInfoPlayers[i].CopyDataFromAnotherSO(newSOInfoPlayerClone);

    //        soNewInfoPlayers[i].LoadFromData(inForPlayerDummyList.data[i]);
    //    }

    //    ///// foreach
    //    //int i = 0;
    //    //foreach (InForPlayerDummy inForPlayerDummy in inForPlayerDummyList.data)
    //    //{
    //    //    soNewInfoPlayers[i].LoadFromData(inForPlayerDummy);
    //    //    i++;
    //    //    //Debug.Log($"inForPlayerDummy : " + inForPlayerDummy.nameP);
    //    //}

    //    ////v2
    //    //string saveDataString = SaveSystem.Load();
    //    //if (saveDataString == null) return;
    //    //InForPlayerDummy inForPlayerDummyData = JsonUtility.FromJson<InForPlayerDummy>(saveDataString);

    //    //soNewInfoPlayers[0].LoadFromData(inForPlayerDummyData);

    //    ////v1
    //    //soNewInfoPlayers[0].playerIndexType = inForPlayerDummyData.playerIndexType;
    //    //soNewInfoPlayers[0].nameP = inForPlayerDummyData.nameP;
    //    //soNewInfoPlayers[0].keyPairP = inForPlayerDummyData.keyPairP;
    //    //Debug.Log("Da load saveDataString vao soNewInfoPlayers[0]");

    //    Debug.Log("Da load saveDataString vao soNewInfoPlayers");
    //}


    public void ResetDefaultInfoPlayer()
    {
        for (int i = 0; i < soNewInfoPlayers.Count; i++)
        {
            soNewInfoPlayers[i].CopyDataFromAnotherSO(soDefaultInfoPlayers[i]);
        }
    }


    public void SaveNewInfoToSO(PlayerIndexType playerIndexType, string nameP, KeyPair keyPair)
    {
        int index = playerIndexType.ToIndex();

        soNewInfoPlayers[index].nameP = nameP;

        SaveNewInfoKeyToSO(playerIndexType, keyPair);
    }
    public void SaveNewInfoKeyToSO(PlayerIndexType playerIndexType, KeyPair keyPair)
    {
        int index = playerIndexType.ToIndex();

        soNewInfoPlayers[index].keyPairP = keyPair.Clone();
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
            inForPlayerDummyList.data.Add(soNewInfoPlayers[i].ToData());
        }

        string jsonStringData = JsonUtility.ToJson(inForPlayerDummyList, true);
        SaveSystem.Save(jsonStringData);

        Debug.Log("Da save jsonString xuong", gameObject);
        InputManager.Instance.ListKeyCodeForPlayer();
    }
}

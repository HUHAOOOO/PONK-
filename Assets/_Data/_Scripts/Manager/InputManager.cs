using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InputManager : CoreMonoBehaviour
{
    private static InputManager instance;
    public static InputManager Instance => instance;

    [SerializeField] protected List<KeyPair> playerKC;
    [SerializeField] protected List<CharCtrl> players = new();

    public List<KeyPair> PlayerKC => playerKC;


    protected override void Awake()
    {
        if (instance != null) Debug.LogError("only allow 1 InputManager | Singleton");
        InputManager.instance = this;
    }
    protected override void LoadComponents()
    {
        InputManager.instance = this;

        base.LoadComponents();
        GetListPlayer();
        //LoadPlayers();
        //
        ListKeyCodeForPlayer();
        SetInputForPlayer();
    }
    protected override void OnEnable()
    {
        ListKeyCodeForPlayer();
        //Debug.LogWarning("ListKeyCodeForPlayer new ! ");
    }

    private void GetListPlayer()
    {
        if (GameManager.Instance == null)
        {
            Debug.LogWarning("GameManager chua co Instance kia Reset lai di:v");
            return;
        }
        players = GameManager.Instance.Players;

    }
    //protected virtual void LoadPlayers()
    //{
    //    if (this.players.Count > 0) return;
    //    CharCtrl[] listPlayers = FindObjectsByType<CharCtrl>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
    //    //FindObjectsInactive.Exclude // GO active
    //    //FindObjectsInactive.Include // GO inactive
    //    //this.players = listPlayers.ToList();
    //    this.players = listPlayers.OrderBy(p => p.transform.name).ToList();
    //    Debug.LogWarning(transform.name + ": LoadPlayers", gameObject);
    //}
    public void ListKeyCodeForPlayer()
    {
        playerKC.Clear();
        //KeyCode Deault for player
        for (int i = 0; i < 4; i++)
        {
            SOInfoPlayer newSOInfoPlayer = ScriptableObject.CreateInstance<SOInfoPlayer>();
            newSOInfoPlayer = SaveLoadManager.Instance.SONewInfoPlayers[i];

            playerKC.Add(new KeyPair(newSOInfoPlayer.keyPairP.keyAttack, newSOInfoPlayer.keyPairP.keyDodge));
        }
        //playerKC.Add(new KeyPair(KeyCode.X, KeyCode.C));
        //playerKC.Add(new KeyPair(KeyCode.B, KeyCode.N));
        //playerKC.Add(new KeyPair(KeyCode.O, KeyCode.P));
    }


    public virtual void SetInputForPlayer()
    {
        KeyPair newKeyPair;
        for (int i = 0; i < players.Count; i++)
        {
            newKeyPair = playerKC[i].Clone();
            players[i].CharInput.KeyAttack = newKeyPair.keyAttack;
            players[i].CharInput.KeyDodge = newKeyPair.keyDodge;
        }
    }

    public void UpdateKey4Pkayer()
    {
        ListKeyCodeForPlayer();
        SetInputForPlayer();
    }

}

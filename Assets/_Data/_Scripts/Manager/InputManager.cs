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
    public void ListKeyCodeForPlayer()
    {
        playerKC.Clear();
        for (int i = 0; i < 4; i++)
        {
            SOInfoPlayer newSOInfoPlayer = ScriptableObject.CreateInstance<SOInfoPlayer>();
            newSOInfoPlayer = SaveLoadManager.Instance.SONewInfoPlayers[i];

            playerKC.Add(new KeyPair(newSOInfoPlayer.keyPairP.keyAttack, newSOInfoPlayer.keyPairP.keyDodge));
        }
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

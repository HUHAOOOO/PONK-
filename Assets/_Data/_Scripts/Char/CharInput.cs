using UnityEngine;

public class CharInput : CoreMonoBehaviour
{
    [SerializeField] protected bool _isAttack;
    [SerializeField] protected bool _isDodge;
    [SerializeField] protected bool _isDie;

    //private static CharInput instance;
    //public static CharInput Instance { get => instance; }

    public bool IsAttack { get => _isAttack; }
    public bool IsDodge { get => _isDodge; }
    public bool IsDie { get => _isDie; }


    //protected override void Awake()
    //{
    //    base.Awake();
    //    if (CharInput.instance != null) Debug.LogError("Only 1 CharInput allow to exist");
    //    CharInput.instance = this;
    //}


    void Update()
    {
        GetInput();
    }


    // [ ] todo : cho Input ra 1 GO class rieng
    protected virtual void GetInput()
    {
        //INPUT
        if (Input.GetKeyDown(KeyCode.A))
        {
            _isAttack = true;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            _isDodge = true;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            _isDie = true;
        }
    }

    public virtual void SetFalseSomeThing() // hoi kif
    {
        _isAttack = false;
        _isDodge = false;
        _isDie = false;
        //...more
    }


}

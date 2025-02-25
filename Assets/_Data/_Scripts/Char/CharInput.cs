using UnityEngine;

public class CharInput : CoreMonoBehaviour
{
    [SerializeField] protected bool _isAttack;
    [SerializeField] protected bool _isDodge;
    [SerializeField] protected bool _isDie;

    public bool IsAttack { get => _isAttack; }
    public bool IsDodge { get => _isDodge; }
    public bool IsDie { get => _isDie; }

    void Update()
    {
        GetInput();
    }
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

    public virtual void SetFalseSomeThing()
    {
        _isAttack = false;
        _isDodge = false;
        _isDie = false;
        //...more
    }
}

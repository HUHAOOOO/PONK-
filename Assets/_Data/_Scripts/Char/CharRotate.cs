using System.Collections;
using UnityEngine;

public class CharRotate : CoreMonoBehaviour
{
    [SerializeField] protected CharCtrl _charCtrl;
    [SerializeField] protected WorldAreaType curentWorldAreaType;
    [SerializeField] protected float timerRotate = 0;
    [SerializeField] protected float timeDelayRotate = 0.1f;
    [SerializeField] protected bool canRotate;


    public CharCtrl CharCtrl
    {
        get => _charCtrl;
        private set => _charCtrl = value;
    }
    //protected override void Start()
    //{
    //    StartCoroutine(InitRotateAfterUpdate());
    //}
    protected override void OnEnable()
    {
        SetDefaultRotate();
        StartCoroutine(InitRotateAfterUpdate());
    }
    private void SetDefaultRotate()
    {
        if(_charCtrl.PlayerIndexType == PlayerIndexType.P0)
        {
            _charCtrl.transform.rotation = Quaternion.identity;
            //_charCtrl.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        if (_charCtrl.PlayerIndexType == PlayerIndexType.P1)
        {
            _charCtrl.transform.rotation = Quaternion.Euler(0, 0, -90);
        }
        if (_charCtrl.PlayerIndexType == PlayerIndexType.P2)
        {
            _charCtrl.transform.rotation = Quaternion.Euler(0, 0, 180);
        }
        if (_charCtrl.PlayerIndexType == PlayerIndexType.P3)
        {
            _charCtrl.transform.rotation = Quaternion.Euler(0, 0, 90);
        }

    }

    IEnumerator InitRotateAfterUpdate()
    {
        yield return null; // Wait next frame 
        InitRotate();

        curentWorldAreaType = GameManager.Instance.CurrentArea;
    }
    protected override void LoadComponents()
    {
        LoadCharCtrl();
    }

    private void Update()
    {
        if (_charCtrl.DamReceive.IsDie == true) return;
        RotateChar();
    }

    protected virtual void LoadCharCtrl()
    {
        if (this._charCtrl != null) return;
        _charCtrl = transform.parent.GetComponent<CharCtrl>();
        Debug.LogWarning(transform.name + ": LoadCharCtrl", gameObject);
    }
    protected virtual void Rotate()
    {
        _charCtrl.transform.Rotate(0, 180, 0);
        //Debug.Log(_charCtrl.name + "huha Rotate !",gameObject);
    }

    //protected virtual void InitRotate()
    //{
    //    WorldAreaType newCurrentArea = GameManager.Instance.CurrentArea;
    //    if (newCurrentArea == WorldAreaType.Area1)
    //    {
    //        if (_charCtrl.CurrentPos.name == "Pos_1")
    //        {
    //            Rotate();
    //        }
    //        else if (_charCtrl.CurrentPos.name == "Pos_2")
    //        {
    //            Rotate();
    //        }
    //    }

    //    if (newCurrentArea == WorldAreaType.Area2)
    //    {
    //        if (_charCtrl.CurrentPos.name == "Pos_2")
    //        {
    //            Rotate();
    //        }
    //        if (_charCtrl.CurrentPos.name == "Pos_3")
    //        {
    //            Rotate();
    //        }
    //    }

    //    if (newCurrentArea == WorldAreaType.Area3)
    //    {
    //        if (_charCtrl.CurrentPos.name == "Pos_3")
    //        {
    //            Rotate();
    //        }
    //        if (_charCtrl.CurrentPos.name == "Pos_4")
    //        {
    //            Rotate();
    //        }
    //    }

    //    if (newCurrentArea == WorldAreaType.Area4)
    //    {
    //        if (_charCtrl.CurrentPos.name == "Pos_4")
    //        {
    //            Rotate();
    //        }
    //        if (_charCtrl.CurrentPos.name == "Pos_1")
    //        {
    //            Rotate();
    //        }
    //    }
    //}
    //
    protected virtual void InitRotate()
    {
        WorldAreaType newCurrentArea = GameManager.Instance.CurrentArea;


        if (newCurrentArea == WorldAreaType.Area1)
        {
            if (_charCtrl.CurrentPos.name == "Pos_2")
            {
                Rotate();
            }
            else if (_charCtrl.CurrentPos.name == "Pos_3")
            {
                Rotate();
            }
        }

        if (newCurrentArea == WorldAreaType.Area2)
        {
            if (_charCtrl.CurrentPos.name == "Pos_3")
            {
                Rotate();
            }
            if (_charCtrl.CurrentPos.name == "Pos_4")
            {
                Rotate();
            }
        }

        if (newCurrentArea == WorldAreaType.Area3)
        {
            if (_charCtrl.CurrentPos.name == "Pos_4")
            {
                Rotate();
            }
            if (_charCtrl.CurrentPos.name == "Pos_1")
            {
                Rotate();
            }
        }

        if (newCurrentArea == WorldAreaType.Area4)
        {
            if (_charCtrl.CurrentPos.name == "Pos_1")
            {
                Rotate();
            }
            if (_charCtrl.CurrentPos.name == "Pos_2")
            {
                Rotate();
            }
        }
    }
    //
    protected virtual void RotateChar()
    {
        if (!CanRotate()) return;
        canRotate = false;

        WorldAreaType newCurrentArea = GameManager.Instance.CurrentArea;

        bool topPoint = GameManager.Instance.TopPoint;
        bool rightPoint = GameManager.Instance.RightPoint;
        bool bottomPoint = GameManager.Instance.BottomPoint;
        bool leftPoint = GameManager.Instance.LeftPoint;

        //if (_charCtrl.CurrentPos.name == "Pos_1" || _charCtrl.CurrentPos.name == "Pos_3")
        //{
        //    if (leftPoint || rightPoint)
        //    {
        //        this.Rotate();
        //    }
        //}
        //else if (_charCtrl.CurrentPos.name == "Pos_2" || _charCtrl.CurrentPos.name == "Pos_4")
        //{
        //    if (topPoint || bottomPoint)
        //    {
        //        this.Rotate();
        //    }
        //}
        if (_charCtrl.CurrentPos.name == "Pos_1" || _charCtrl.CurrentPos.name == "Pos_3")
        {
            //if (leftPoint || rightPoint)
            //{
            //    this.Rotate();
            //}
            if (topPoint || bottomPoint)
            {
                this.Rotate();
            }
        }
        else if (_charCtrl.CurrentPos.name == "Pos_2" || _charCtrl.CurrentPos.name == "Pos_4")
        {
            //if (topPoint || bottomPoint)
            //{
            //    this.Rotate();
            //}
            if (leftPoint || rightPoint)
            {
                this.Rotate();
            }
        }
        curentWorldAreaType = newCurrentArea;
    }
    protected virtual bool CanRotate()
    {
        timerRotate += Time.deltaTime;
        if (timerRotate < timeDelayRotate) return false;
        timerRotate = 0;

        WorldAreaType newCurrentArea = GameManager.Instance.CurrentArea;
        if (curentWorldAreaType == newCurrentArea) return false;

        canRotate = true;
        return true;
    }
}

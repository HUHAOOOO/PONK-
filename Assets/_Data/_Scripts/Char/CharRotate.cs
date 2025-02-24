using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UIElements;

public class CharRotate : CoreMonoBehaviour
{
    [SerializeField] protected CharCtrl _charCtrl;
    [SerializeField] protected WorldAreaType curentWorldAreaType;

    public CharCtrl CharCtrl
    {
        get => _charCtrl;
        private set => _charCtrl = value;
    }
    protected override void Start()
    {
        StartCoroutine(InitRotateAfterUpdate());
        
    }
    IEnumerator InitRotateAfterUpdate()
    {
        yield return null; // Wait next frame 
        InitRotate();
        //yield return new WaitForEndOfFrame();// Wait ket thuc frame 
        Debug.Log("InitializeAfterUpdate : InitRotate");

        curentWorldAreaType = GameManager.Instance.CurrentArea;// L1 bo qua ?? dc ko 
    }
    protected override void LoadComponents()
    {
        LoadCharCtrl();
    }

    private void LateUpdate()
    {
        //[ ] TODO 
        Invoke(nameof(RotateRealTime), 1f);
        //RotateRealTime(); // chay cham hon chut dc ko
        //RotateRealTime();
    }

    protected virtual void LoadCharCtrl()
    {
        if (this._charCtrl != null) return;
        _charCtrl = transform.parent.GetComponent<CharCtrl>();
        Debug.LogWarning(transform.name + ": LoadCharCtrl", gameObject);
    }
    protected virtual void Rotate()
    {
        //if (canRotate == false) return;
        _charCtrl.transform.Rotate(0, 180, 0);
        Debug.Log(_charCtrl.name + " => Rotate !",gameObject);
    }

    protected virtual void InitRotate()
    {
        Debug.Log("InitRotate");
        
        WorldAreaType newCurrentArea = GameManager.Instance.CurrentArea;//WorldAreaType.Area2;
        //Debug.Log("newCurrentArea" + newCurrentArea);
        if (newCurrentArea == WorldAreaType.Area1)
        {
            if (_charCtrl.CurrentPos.name == "Pos_1")
            {
                Rotate();
            }
            else if (_charCtrl.CurrentPos.name == "Pos_2")
            {
                Rotate();
            }
        }

        if (newCurrentArea == WorldAreaType.Area2)
        {
            Debug.Log("newCurrentArea == WorldAreaType.Area2");
            if (_charCtrl.CurrentPos.name == "Pos_2")
            {
                Rotate();
            }
            if (_charCtrl.CurrentPos.name == "Pos_3")
            {
                Rotate();
            }
            
        }

        if (newCurrentArea == WorldAreaType.Area3)
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

        if (newCurrentArea == WorldAreaType.Area4)
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
    }

    protected virtual void RotateRealTime()
    {
        //[ ] TODO l1 ko rotate:v

        WorldAreaType newCurrentArea = GameManager.Instance.CurrentArea;
        if (curentWorldAreaType == newCurrentArea) return;
        WorldAreaType newPreviousArea = GameManager.Instance.PreviousArea;

        //Debug.Log("_charCtrl.CurrentPos.name : " + _charCtrl.CurrentPos.name);

        bool topPoint = (newCurrentArea == WorldAreaType.Area1 && newPreviousArea == WorldAreaType.Area4);
        bool rightPoint = (newCurrentArea == WorldAreaType.Area2 && newPreviousArea == WorldAreaType.Area1);
        bool bottomPoint = (newCurrentArea == WorldAreaType.Area3 && newPreviousArea == WorldAreaType.Area2);
        bool leftPoint = (newCurrentArea == WorldAreaType.Area4 && newPreviousArea == WorldAreaType.Area3);

        Debug.Log("topPoint : " + topPoint);
        Debug.Log("leftPoint : " + leftPoint);
        Debug.Log("bottomPoint : " + bottomPoint);
        Debug.Log("rightPoint : " + rightPoint);

        if (_charCtrl.CurrentPos.name == "Pos_1")
        {
            if (leftPoint || rightPoint)
            {
                this.Rotate();
                curentWorldAreaType = newCurrentArea;
            }
        }

        if (_charCtrl.CurrentPos.name == "Pos_2")
        {
            if (topPoint || bottomPoint)
            {
                this.Rotate();
                curentWorldAreaType = newCurrentArea;
            }
        }

        if (_charCtrl.CurrentPos.name == "Pos_3")
        {
            if (leftPoint || rightPoint)
            {
                this.Rotate();
                curentWorldAreaType = newCurrentArea;
            }
        }

        if (_charCtrl.CurrentPos.name == "Pos_4")
        {
            if (topPoint || bottomPoint)
            {
                this.Rotate();
                curentWorldAreaType = newCurrentArea;
            }
        }
    }

}

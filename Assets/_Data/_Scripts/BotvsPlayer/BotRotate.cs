using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UIElements;

public class BotRotate : CoreMonoBehaviour
{
    [SerializeField] protected BotCtrl botCtrl;
    [SerializeField] protected WorldAreaType curentWorldAreaType;
    [SerializeField] protected float timerRotate = 0;
    [SerializeField] protected float timeDelayRotate = 0.5f;
    [SerializeField] protected bool canRotate;


    public BotCtrl BotCtrl
    {
        get => botCtrl;
        private set => botCtrl = value;
    }
    protected override void OnEnable()
    {
        StartCoroutine(InitRotateAfterUpdate());
    }
    protected override void OnDisable()
    {
        this.transform.parent.rotation = Quaternion.Euler(0, 0, 0);
    }
    IEnumerator InitRotateAfterUpdate()
    {
        yield return null; // Wait next frame 
        InitRotate();
        //Debug.LogWarning("InitRotate();", gameObject);

        curentWorldAreaType = BotArea.Instance.CurrentArea;
    }
    protected override void LoadComponents()
    {
        LoadBotCtrl();
    }

    private void Update()
    {
        RotateBot();
    }

    protected virtual void LoadBotCtrl()
    {
        if (this.botCtrl != null) return;
        botCtrl = transform.parent.GetComponent<BotCtrl>();
        Debug.LogWarning(transform.name + ": LoadBotCtrl", gameObject);
    }
    protected virtual void Rotate()
    {
        botCtrl.transform.Rotate(0, 180, 0);
        //Debug.Log(_charCtrl.name + "huha Rotate !",gameObject);
    }

    protected virtual void InitRotate()
    {
        WorldAreaType newCurrentArea = BotArea.Instance.CurrentArea;


        if (newCurrentArea == WorldAreaType.Area1)
        {
            if (botCtrl.CurrentPos.name == "Pos_2")
            {
                Rotate();
            }
            else if (botCtrl.CurrentPos.name == "Pos_3")
            {
                Rotate();
            }
        }

        if (newCurrentArea == WorldAreaType.Area2)
        {
            if (botCtrl.CurrentPos.name == "Pos_3")
            {
                Rotate();
            }
            if (botCtrl.CurrentPos.name == "Pos_4")
            {
                Rotate();
            }
        }

        if (newCurrentArea == WorldAreaType.Area3)
        {
            if (botCtrl.CurrentPos.name == "Pos_4")
            {
                Rotate();
            }
            if (botCtrl.CurrentPos.name == "Pos_1")
            {
                Rotate();
            }
        }

        if (newCurrentArea == WorldAreaType.Area4)
        {
            if (botCtrl.CurrentPos.name == "Pos_1")
            {
                Rotate();
            }
            if (botCtrl.CurrentPos.name == "Pos_2")
            {
                Rotate();
            }
        }
    }

    protected virtual void RotateBot()
    {
        if (!CanRotate()) return;
        canRotate = false;

        WorldAreaType newCurrentArea = BotArea.Instance.CurrentArea;

        bool topPoint = BotArea.Instance.TopPoint;
        bool rightPoint = BotArea.Instance.RightPoint;
        bool bottomPoint = BotArea.Instance.BottomPoint;
        bool leftPoint = BotArea.Instance.LeftPoint;

        if (botCtrl.CurrentPos.name == "Pos_1" || botCtrl.CurrentPos.name == "Pos_3")
        {
            if (topPoint || bottomPoint)
            {
                this.Rotate();
            }
        }
        else if (botCtrl.CurrentPos.name == "Pos_2" || botCtrl.CurrentPos.name == "Pos_4")
        {
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

        WorldAreaType newCurrentArea = BotArea.Instance.CurrentArea;
        if (curentWorldAreaType == newCurrentArea) return false;

        canRotate = true;
        return true;
    }
}

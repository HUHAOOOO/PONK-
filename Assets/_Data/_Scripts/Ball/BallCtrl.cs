using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class BallCtrl : CoreMonoBehaviour
{
    [SerializeField] protected Transform currentBall;
    [SerializeField] protected BallRotate ballRotate;
    [SerializeField] protected Transform balls;
    [SerializeField] protected List<Transform> ballsModel = new();

    public Transform CurrentBall => currentBall;
    public BallRotate BallRotate => ballRotate;
    public Transform Balls => balls;
    public List<Transform> BallsModel => ballsModel;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadBallRotate();
        LoadModel();
    }
    protected virtual void LoadBallRotate()
    {
        if (this.ballRotate != null) return;
        ballRotate = GetComponentInChildren<BallRotate>();
        Debug.LogWarning(transform.name + ": LoadBallRotate", gameObject);
    }
    protected virtual void LoadModel()
    {
        if (this.balls != null ) return;
        balls = transform.Find("BALLS").GetComponent<Transform>();
        Debug.LogWarning(transform.name + ": LoadModel", gameObject);

        LoadBallsModel();
        //SetActiveBalls(0);
        SetActiveBalls(GetBallByType(TypeBall.DeffaultBall));


        SetCurrentBall(ballsModel[0]);
    }
    protected virtual void LoadBallsModel()
    {
        if (this.balls == null) return;

        if (this.ballsModel.Count > 0) return;

        foreach (Transform ball in balls)
        {
            ballsModel.Add(ball);
        }
    }
    //public void SetActiveBallsByTime(int index , float timeReset)
    //{
    //    SetActiveBalls(index);
    //    Invoke(nameof(SetDefaultSpriteBall), timeReset);
    //}

    //public void SetActiveBalls(int index)
    //{
    //    foreach (Transform ball in ballsModel)
    //    {
    //        ball.gameObject.SetActive(false);
    //    }


    //    ballsModel[index].gameObject.SetActive(true);
    //    SetCurrentBall(ballsModel[index].transform);
    //    ballRotate.InitRotate();
    //}
    public void SetActiveBallsByTime(TypeBall typeBall, float timeReset)
    {
        SetActiveBalls(GetBallByType(typeBall));
        Invoke(nameof(SetDefaultSpriteBall), timeReset);
    }
    public void SetActiveBalls(Transform newBall)
    {
        foreach (Transform ball in ballsModel)
        {
            ball.gameObject.SetActive(false);
        }


        newBall.gameObject.SetActive(true);
        SetCurrentBall(newBall);
        ballRotate.InitRotate();
    }
    public void SetDefaultSpriteBall()
    {
        SetActiveBalls(GetBallByType(TypeBall.DeffaultBall));
    }

    public void SetCurrentBall(Transform currentBall)
    {
        this.currentBall = currentBall;
        //ballRotate.InitRotate();
    }

    private Transform GetBallByType(TypeBall typeBall)
    {
        if(typeBall == TypeBall.DeffaultBall)
        {
            return ballsModel[0];
        }
        else if (typeBall == TypeBall.LightningBall)
        {
            return ballsModel[1];
        }
        else if (typeBall == TypeBall.FireBall)
        {
            return ballsModel[2];
        }
        else return null;
    }
}

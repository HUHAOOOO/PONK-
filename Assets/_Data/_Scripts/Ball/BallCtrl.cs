using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class BallCtrl : CoreMonoBehaviour
{
    [SerializeField] protected BallRotate ballRotate;
    [SerializeField] protected Transform model;
    [SerializeField] protected List<Transform> ballsModel = new();

    public Transform Ball => ballsModel[0];
    public BallRotate BallRotate => ballRotate;
    public Transform Model => model;
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
        if (this.model != null ) return;
        model = transform.Find("Model").GetComponent<Transform>();
        Debug.LogWarning(transform.name + ": LoadModel", gameObject);

        LoadBallsModel();
        SetActiveBalls(0);
    }
    protected virtual void LoadBallsModel()
    {
        if (this.model == null) return;

        if (this.ballsModel.Count > 0) return;

        foreach (Transform ball in model)
        {
            ballsModel.Add(ball);
        }
    }
    public void SetActiveBallsByTime(int index , float timeReset)
    {
        SetActiveBalls(index);
        Invoke(nameof(SetDefaultSpriteBall), timeReset);
    }
    public void SetActiveBalls(int index)
    {
        foreach (Transform ball in ballsModel)
        {
            ball.gameObject.SetActive(false);
        }
        ballsModel[index].gameObject.SetActive(true);
    }
    public void SetDefaultSpriteBall()
    {
        SetActiveBalls(0);
    }

}

using UnityEngine;

public class BallCtrl : CoreMonoBehaviour
{
    [SerializeField] protected Transform ball;
    //[SerializeField] protected Transform model;

    public Transform Ball => ball;
    //public Transform Model => model;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadBall();
        //LoadModel();
    }

    protected virtual void LoadBall()
    {
        if (this.ball != null) return;
        ball = GameObject.Find("Ball").transform;
        Debug.LogWarning(transform.name + ": LoadBall", gameObject);
    }

    //protected virtual void LoadModel()
    //{
    //    if (this.model != null) return;
    //    model = GameObject.Find("Model").transform;
    //    Debug.LogWarning(transform.name + ": LoadModel", gameObject);
    //}
}

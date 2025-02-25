using UnityEngine;

public class BallCtrl : CoreMonoBehaviour
{
    [SerializeField] protected Transform ball;
    [SerializeField] protected BallRotate ballRotate;

    public Transform Ball => ball;
    public BallRotate BallRotate => ballRotate;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadBall();
        LoadBallRotate();
    }

    protected virtual void LoadBall()
    {
        if (this.ball != null) return;
        ball = GameObject.Find("Ball").transform;
        Debug.LogWarning(transform.name + ": LoadBall", gameObject);
    }

    protected virtual void LoadBallRotate()
    {
        if (this.ballRotate != null) return;
        ballRotate = GetComponentInChildren<BallRotate>();
        Debug.LogWarning(transform.name + ": LoadBallRotate", gameObject);
    }
}

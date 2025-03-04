using UnityEngine;

public class BallRotate : GORotateParent
{
    [Header("Ball Rotale")]
    [SerializeField] protected BallCtrl ballCtrl;

    [SerializeField] protected int upSpeedPoint = 10;
    [SerializeField] protected int defaultSpeed = 100;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadBallCtrl();

        InitRotate();
    }

    protected virtual void LoadBallCtrl()
    {
        if (this.ballCtrl != null) return;
        ballCtrl = transform.parent.GetComponent<BallCtrl>();
        Debug.LogWarning(transform.name + ": LoadBallCtrl", gameObject);
    }
    protected override void Reset()
    {
        base.Reset();
        speedRotate = 100;
        typeRorate = TypeRotate.z;
    }
    protected virtual void InitRotate()
    {
        if(speedRotate <= 0)//(GameManager.Instance.IsClockwise)
        {
            ballCtrl.Model.transform.Rotate(0, 0, 0);
        }
        else
        {
            ballCtrl.Model.transform.Rotate(180, 0, 0);
        }
    }

    public void SpeedSpecialBall(int speed,float timeReset)
    {
        AddSpeedRotate(speed);
        Invoke(nameof(SetSpeed), timeReset);
    }
    public void SetSpeed()
    {
        AddSpeedRotate(-50);
    }
    public void AddSpeedRotate(int speed)
    {
        if (speedRotate >= 0)
        {
            speedRotate += speed;
        }
        else if (speedRotate < 0)
        {
            speedRotate -= speed;
        }

        if(speedRotate == 0)
        {
            speedRotate = 50;
        }
    }

    public void SetSpeedRotate(int speed)
    {
        speedRotate = speed;
    }
    public void UpSpeed()
    {
        if (speedRotate >= 0)
        {
            speedRotate += upSpeedPoint;
        }
        else speedRotate -= upSpeedPoint;
    }

    // Player goi khi danh trung
    public void ChangeDirection(int minus = -1)
    {
        speedRotate *= minus;
        UpSpeed();
        ChangeRotateSprite();
    }

    public void SetDefaultSpeed()//moi lan Ponk Player se set default
    {

        if (speedRotate >= 0)
        {
            speedRotate = defaultSpeed;
        }
        else if (speedRotate < 0)
        {
            speedRotate = -defaultSpeed;
        }
    }
    public void ChangeRotateSprite()
    {
        ballCtrl.Model.transform.Rotate(180, 0, 0);
    }

 
}
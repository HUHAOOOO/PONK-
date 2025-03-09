using UnityEngine;

public class BallRotate : GORotateParent
{
    [Header("Ball Rotale")]
    [SerializeField] protected BallCtrl ballCtrl;

    [SerializeField] protected int upSpeedPoint = 10;
    [SerializeField] protected int defaultSpeed = 100;
    [SerializeField] protected int newSpeedAdd;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadBallCtrl();
    }
    protected override void ResetValue()
    {
        speedRotate = -defaultSpeed;
        typeRorate = TypeRotate.z;

        InitRotate();
    }
    protected virtual void LoadBallCtrl()
    {
        if (this.ballCtrl != null) return;
        ballCtrl = transform.parent.GetComponent<BallCtrl>();
        Debug.LogWarning(transform.name + ": LoadBallCtrl", gameObject);
    }
    public void InitRotate()
    {
        if(speedRotate <= 0)//(GameManager.Instance.IsClockwise)
        {
            //ballCtrl.CurrentBall.transform.rotation = Quaternion.Euler(0, 0, 0);
            ballCtrl.CurrentBall.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else if(speedRotate > 0)
        {
            //ballCtrl.CurrentBall.transform.rotation = Quaternion.Euler(180,0,0);
            ballCtrl.CurrentBall.transform.localRotation = Quaternion.Euler(180,0,0);
        }
    }

    public void SpeedSpecialBall(int speed,float timeReset)
    {
        newSpeedAdd = speed;
        AddSpeedRotate(speed);
        Invoke(nameof(SetPreviousSpeed), timeReset);
    }
    public void SetPreviousSpeed()
    {
        AddSpeedRotate(-newSpeedAdd);
    }
    public void AddSpeedRotate(int speed)
    {
        if (speedRotate > 0)
        {
            speedRotate += speed;
        }
        else if (speedRotate < 0)
        {
            speedRotate -= speed;
        }
        else if(speedRotate == 0)
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

    public void ChangeDirection(int minus = -1)
    {
        speedRotate *= minus;
        UpSpeed();
        ChangeRotateSprite();
    }

    public void SetDefaultSpeed()
    {
        if (speedRotate >= 0)
        {
            speedRotate = defaultSpeed;
        }
        else if (speedRotate < 0)
        {
            speedRotate = -defaultSpeed;
        }

        newSpeedAdd = 0;
    }
    public void ChangeRotateSprite()
    {
        ballCtrl.CurrentBall.transform.Rotate(180, 0, 0);
    }
}
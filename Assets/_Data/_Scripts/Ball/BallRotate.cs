using UnityEngine;

public class BallRotate : GORotateParent
{
    [Header("Ball Rotale")]
    [SerializeField] protected BallCtrl ballCtrl;

    [SerializeField] protected int upSpeedPoint = 5;
    [SerializeField] protected int defaultSpeed = 40;
    [SerializeField] protected int newSpeedAdd;
    [SerializeField] protected int maxSpeed = 100;
    [SerializeField] protected int minSpeed = -100;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadBallCtrl();
    }
    protected override void ResetValue()
    {
        speedRotate = +defaultSpeed;
        typeRorate = TypeRotate.z;
        InitRotate();
    }
    protected override void OnEnable()
    {

        int x = Random.Range(0, 2);
        if (x == 0)
            speedRotate = -defaultSpeed;
        else if (x == 1)
            speedRotate = +defaultSpeed;
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
        if (speedRotate <= 0)//(GameManager.Instance.IsClockwise)
        {
            ballCtrl.CurrentBall.transform.parent.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else if (speedRotate > 0)
        {
            ballCtrl.CurrentBall.transform.parent.transform.localRotation = Quaternion.Euler(180, 0, 0);
        }
    }
    public void SpeedSpecialBall(int speed, float timeReset)
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
        else if (speedRotate == 0)
        {
            speedRotate = defaultSpeed;
        }
        OkSpeed();
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

        OkSpeed();
    }
    private void OkSpeed()
    {
        if (speedRotate > 0)
            if (speedRotate > maxSpeed) speedRotate = maxSpeed;
        if (speedRotate < 0)
            if (speedRotate < minSpeed) speedRotate = minSpeed;
    }
    public void ChangeDirection(int minus = -1)
    {
        AudioManager.Instance.PlaySFX("HitBall");
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
        ballCtrl.CurrentBall.transform.parent.transform.Rotate(180, 0, 0);
    }
}
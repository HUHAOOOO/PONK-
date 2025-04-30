using UnityEngine;

public class GORotateParent : CoreMonoBehaviour
{
    [Header("GO Rotate Parent")]
    [SerializeField] protected float speedRotate = 0.1f;
    [SerializeField] protected TypeRotate typeRorate = TypeRotate.None;
    [SerializeField] protected bool isRandomRotate;

    public float SpeedRotate => speedRotate;
    void Update()
    {
        switch (typeRorate)
        {
            case TypeRotate.x:
                transform.parent.Rotate(Vector3.right * speedRotate * Time.deltaTime);
                break;

            case TypeRotate.z:
                transform.parent.Rotate(Vector3.forward * speedRotate * Time.deltaTime);
                break;
        }
    }

    protected override void OnEnable()
    {
        RandomRotateParent();
    }
    void RandomRotateParent()
    {
        if (!isRandomRotate) return;
        float randomAngle = Random.Range(0f, 360f);
        transform.parent.rotation = Quaternion.Euler(0f, 0f, randomAngle);
    }
}

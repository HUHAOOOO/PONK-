using UnityEngine;

public class GORotateParent : CoreMonoBehaviour
{
    [Header("GO Rotate Parent")]
    [SerializeField] protected float speedRotate = 0.1f;
    [SerializeField] protected TypeRotate typeRorate = TypeRotate.None;

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
}

public enum TypeRotate
{
    None = 0,
    x = 1,
    y = 2,
    z = 3,

}

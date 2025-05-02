using UnityEngine;

public class IgnoreParentRotation : MonoBehaviour
{
    [SerializeField] protected Quaternion worldRotation;
    void Start()
    {
        worldRotation = transform.rotation;
    }
    void LateUpdate()
    {
        transform.rotation = worldRotation;
    }
}

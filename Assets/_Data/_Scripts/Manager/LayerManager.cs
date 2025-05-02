
using Unity.VisualScripting;
using UnityEngine;

public class LayerManager : MonoBehaviour
{
    private static LayerManager instance;
    public static LayerManager Instance => instance;

    [SerializeField] protected int defaultLayer;
    [SerializeField] protected int ballLayer;
    [SerializeField] protected int ballLayerGetMask;
    public int BallLayer => ballLayer;
    public int BallLayerGetMask => ballLayerGetMask;

    protected virtual void Awake()
    {
        if (instance != null) Debug.LogError("only allow 1 LayerManager | Singleton");
        LayerManager.instance = this;
    }
    private void Reset()
    {
        defaultLayer = LayerMask.NameToLayer("Default");
        ballLayer = LayerMask.NameToLayer("Ball");

        ballLayerGetMask = LayerMask.GetMask("Ball");
    }

    private void Start()
    {
        IgnoreMyLayer();
    }

    protected virtual void IgnoreMyLayer()
    {
        if (defaultLayer == -1 || ballLayer == -1)
        {
            Debug.LogError("Loi : 1 trong cac Layer khong ton tai! kiem tra laij ten Layer trong unity $");
            return;
        }
        Physics2D.IgnoreLayerCollision(defaultLayer, ballLayer, false);
    }
}
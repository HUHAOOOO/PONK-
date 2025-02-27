
using UnityEngine;

public class LayerManager : MonoBehaviour
{
    private void Start()
    {
        int defaultLayer = LayerMask.NameToLayer("Default");
        int BallLayer = LayerMask.NameToLayer("Ball");

        if (defaultLayer == -1 || BallLayer == -1)
        {
            Debug.LogError("Loi : 1 trong cac Layer khong ton tai! kiem tra laij ten Layer trong unity $");
            return;
        }
        // Huy ignore : cho phep va cham
        Physics2D.IgnoreLayerCollision(defaultLayer, BallLayer, false);
    }
}
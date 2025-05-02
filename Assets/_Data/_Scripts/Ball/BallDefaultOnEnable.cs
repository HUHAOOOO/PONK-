using UnityEngine;

public class BallDefaultOnEnable : MonoBehaviour
{
    private void OnEnable()
    {
        if (GameManager.Instance == null) return;
        GameManager.Instance.SetPlayerIsCanOverlapCircleMeleeAttack(true);
    }
}

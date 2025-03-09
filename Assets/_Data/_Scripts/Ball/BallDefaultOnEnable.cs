using UnityEngine;

public class BallDefaultOnEnable : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnEnable()
    {
        if (GameManager.Instance == null) return;
        GameManager.Instance.SetPlayerIsCanOverlapCircleMeleeAttack(true);
    }
}

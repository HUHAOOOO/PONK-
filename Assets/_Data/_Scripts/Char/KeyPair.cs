using UnityEngine;

[System.Serializable]
public class KeyPair
{
    public KeyCode keyAttack;
    public KeyCode keyDodge;

    public KeyPair(KeyCode kAttak, KeyCode kDodge)
    {
        keyAttack = kAttak;
        keyDodge = kDodge;
    }
}
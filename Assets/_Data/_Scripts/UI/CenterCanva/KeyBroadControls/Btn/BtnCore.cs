using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;

public abstract class BtnCore : CoreMonoBehaviour
{
    protected override void Awake()
    {
        base.Awake();
        BtnAddOnClickEvent();
    }
    public abstract void BtnAddOnClickEvent();
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : Spawner
{
    private static ItemSpawner instance;
    public static ItemSpawner Instance => instance;

    public static string FX_1 = "FX_1";
    public static string FX_vetchem1 = "FX_vetchem1";
    public static string FX_vetchem2 = "FX_vetchem2";
    protected override void Awake()
    {
        base.Awake();
        if (ItemSpawner.instance != null) Debug.LogError("Only 1 ItemSpawner allow to exist");
        ItemSpawner.instance = this;
    }
}

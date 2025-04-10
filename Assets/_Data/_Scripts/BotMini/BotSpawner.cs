using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotSpawner : Spawner
{
    private static BotSpawner instance;
    public static BotSpawner Instance => instance;

    //public static string FX_1 = "FX_1";
    //public static string FX_vetchem1 = "FX_vetchem1";
    //public static string FX_vetchem2 = "FX_vetchem2";
    protected override void Awake()
    {
        base.Awake();
        if (BotSpawner.instance != null) Debug.LogError("Only 1 BotSpawner allow to exist");
        BotSpawner.instance = this;
    }
}

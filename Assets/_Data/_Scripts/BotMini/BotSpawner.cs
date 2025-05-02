using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotSpawner : Spawner
{
    private static BotSpawner instance;
    public static BotSpawner Instance => instance;

    protected override void Awake()
    {
        base.Awake();
        if (BotSpawner.instance != null) Debug.LogError("Only 1 BotSpawner allow to exist");
        BotSpawner.instance = this;
    }
}

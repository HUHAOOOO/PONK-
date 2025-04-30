using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotDespawnByTime : DespawnByTime
{
    [SerializeField] protected BotSpawnerRandom botSpawnerRandom;


    private void OnDisabke()
    {
        base.OnEnable();
        botSpawnerRandom.RandomTimer = 0;
    }

    public override void DespawnObject()
    {
        BotSpawner.Instance.Despawn(transform.parent);
    }
}

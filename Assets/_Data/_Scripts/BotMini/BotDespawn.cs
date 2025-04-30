using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotDespawn : Despawn
{
    [SerializeField] protected bool isCanDespawn;
    public bool IsCanDespawn { get => isCanDespawn; set => isCanDespawn = value; }

    protected override void OnEnable()
    {
        isCanDespawn = false;
    }
    protected override void OnDisable()
    {
        BotSpawner.Instance.Despawn(transform.parent);
    }
    public override void DespawnObject()
    {
        BotSpawner.Instance.Despawn(transform.parent);
    }

    protected override bool CanDespawn()
    {
        return isCanDespawn;
    }
}

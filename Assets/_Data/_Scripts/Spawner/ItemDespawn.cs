using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDespawn : Despawn
{
    [SerializeField] protected bool itemCanDespawn;
    public override void DespawnObject()
    {
        ItemSpawner.Instance.Despawn(transform.parent);
        SetItemCanDespawn(false);
    }

    protected override bool CanDespawn()
    {
        return itemCanDespawn;
    }

    public void SetItemCanDespawn(bool boolItemDespawn)
    {
        itemCanDespawn = boolItemDespawn;
    }

}

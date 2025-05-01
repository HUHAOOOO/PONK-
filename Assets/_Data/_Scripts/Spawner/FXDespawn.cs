using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXDespawn : DespawnByTime
{

    public override void DespawnObject()
    {
        FXSpawner.Instance.Despawn(transform.parent);
    }
    //protected override bool CanDespawn()
    //{
    //    //this.timer += Time.fixedDeltaTime;
    //    this.timer += Time.unscaledDeltaTime;
    //    if (this.timer < this.delay) return false;
    //    Destroy(transform.parent.gameObject);
    //    return true;
    //}
}

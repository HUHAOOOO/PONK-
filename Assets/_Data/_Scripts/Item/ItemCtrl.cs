using UnityEngine;

public class ItemCtrl : CoreMonoBehaviour
{

    [SerializeField] protected ItemDespawn itemDespawn;

    public ItemDespawn ItemDespawn => itemDespawn;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadItemDespawn();
    }

    protected virtual void LoadItemDespawn()
    {
        if (this.itemDespawn != null) return;
        itemDespawn = GetComponentInChildren<ItemDespawn>();
        Debug.LogWarning(transform.name + ": LoadItemDespawn", gameObject);
    }

}

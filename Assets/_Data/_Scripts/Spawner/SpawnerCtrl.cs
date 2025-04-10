using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerCtrl : CoreMonoBehaviour
{
    [SerializeField] protected Spawner spawner;
    public Spawner Spawner => spawner;

    [SerializeField] protected SpawnPoints spawnPoints;
    public SpawnPoints SpawnPoints => spawnPoints;


    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadSpawner();
        this.LoadSpawnPoints();
    }

    protected virtual void LoadSpawner()
    {
        if (this.spawner != null) return;
        this.spawner = GetComponentInChildren<Spawner>();
        Debug.Log(transform.name + ": LoadSpawner", gameObject);
    }

    protected virtual void LoadSpawnPoints()
    {
        if (this.spawnPoints != null) return;
        this.spawnPoints = transform.Find("SpawnPoints").GetComponent<SpawnPoints>();
        Debug.Log(transform.name + ": LoadSpawnPoints", gameObject);
    }
}

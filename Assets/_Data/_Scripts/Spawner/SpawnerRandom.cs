using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerRandom : CoreMonoBehaviour
{
    [Header("Spawner Random")]
    [SerializeField] protected SpawnerCtrl spawnerCtrl;
    [SerializeField] protected float randomDelay = 2f;
    [SerializeField] protected float randomTimer = 0f;
    [SerializeField] protected float randomLimit = 2f;
    public float RandomTimer { get => randomTimer; set => randomTimer = value; }
    protected override void OnEnable()
    {
        randomTimer = 0;
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadSpawnerCtrl();
    }

    protected virtual void LoadSpawnerCtrl()
    {
        if (this.spawnerCtrl != null) return;
        this.spawnerCtrl = GetComponent<SpawnerCtrl>();
        Debug.LogWarning(transform.name + ": LoadSpawnerCtrl", gameObject);
    }
    protected override void Start()
    {
        RandomTimeDelaySpawnBot();
    }
    protected virtual void RandomTimeDelaySpawnBot()
    {
        this.randomDelay = Random.Range(10f, 40f);
    }
    protected virtual void FixedUpdate()
    {
        this.GOSpawning();
    }

    protected virtual void GOSpawning()
    {
        if (this.RandomReachLimit()) return;

        this.randomTimer += Time.fixedDeltaTime;
        if (this.randomTimer < this.randomDelay) return;
        this.randomTimer = 0;

        Transform ranPoint = this.spawnerCtrl.SpawnPoints.GetRandom();
        Vector3 pos = ranPoint.position;
        Quaternion rot = ranPoint.rotation;

        Transform prefab = this.spawnerCtrl.Spawner.RandomPrefab();
        Transform obj = this.spawnerCtrl.Spawner.Spawn(prefab, pos, rot);
        obj.gameObject.SetActive(true);


        RandomTimeDelaySpawnBot();
    }

    protected virtual bool RandomReachLimit()
    {
        int currentItemCount = this.spawnerCtrl.Spawner.SpawnedCount;
        return currentItemCount >= this.randomLimit;
    }
}

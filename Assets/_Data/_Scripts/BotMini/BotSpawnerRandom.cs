using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotSpawnerRandom : SpawnerRandom
{
    //[Header("Spawner Random")]
    protected override void GOSpawning()
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

        BotCtrl.Instance.SetPosBot(ranPoint);
        Debug.Log("BotCtrl.Instance.SetPosBot(ranPoint);", ranPoint);
    }
}

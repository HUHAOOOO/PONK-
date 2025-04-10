using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.StandaloneInputModule;

public class BotSpawnerRandom : SpawnerRandom
{
    [Header("Spawner Random")]
    [SerializeField] protected int indexPointSpawn;
    //public int IndexPointSpawn { get => indexPointSpawn; set => indexPointSpawn = value; }
    private void Update()
    {
        UpdateIndexPointSpawn();
    }
    protected override void GOSpawning()
    {
        if (this.RandomReachLimit()) return;
        //if (!BotCtrl.Instance.BotIsCanSpawn()) return;
        if (!BallCtrl.Instance.BallBotCanSpawn()) return;

        this.randomTimer += Time.fixedDeltaTime;
        if (this.randomTimer < this.randomDelay) return;
        if (!BotArea.Instance.IsStartNewArea()) return;
        this.randomTimer = 0;

        //
        //Transform ranPoint = this.spawnerCtrl.SpawnPoints.GetRandom();
        Transform ranPoint = this.spawnerCtrl.SpawnPoints.GetPoint(indexPointSpawn);


        Vector3 pos = ranPoint.position;
        Quaternion rot = ranPoint.rotation;

        Transform prefab = this.spawnerCtrl.Spawner.RandomPrefab();
        Transform obj = this.spawnerCtrl.Spawner.Spawn(prefab, pos, rot);
        obj.gameObject.SetActive(true);

        BotCtrl.Instance.SetPosBot(ranPoint);
    }

    public void UpdateIndexPointSpawn()
    {
        bool isClockwise = GameManager.Instance.IsClockwise;
        WorldAreaType worldAreaTypePlayer = GameManager.Instance.CurrentArea;
        if (isClockwise)
        {
            if (worldAreaTypePlayer == WorldAreaType.Area4)
            {
                indexPointSpawn = 0;//index trong list[0->...]
            }
            else if (worldAreaTypePlayer == WorldAreaType.Area1)
            {
                indexPointSpawn = 1;
            }
            else if (worldAreaTypePlayer == WorldAreaType.Area2)
            {
                indexPointSpawn = 2;
            }
            else if (worldAreaTypePlayer == WorldAreaType.Area3)
            {
                indexPointSpawn = 3;
            }
        }
        else if (!isClockwise)
        {
            if (worldAreaTypePlayer == WorldAreaType.Area4)
            {
                indexPointSpawn = 2;
            }
            else if (worldAreaTypePlayer == WorldAreaType.Area1)
            {
                indexPointSpawn = 3;
            }
            else if (worldAreaTypePlayer == WorldAreaType.Area2)
            {
                indexPointSpawn = 0;
            }
            else if (worldAreaTypePlayer == WorldAreaType.Area3)
            {
                indexPointSpawn = 1;
            }
        }
    }

}

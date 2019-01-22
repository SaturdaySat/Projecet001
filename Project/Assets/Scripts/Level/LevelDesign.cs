using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDesign : MonoBehaviour {

    public string LevelName;

    public SpawnPoint hostSpawnPoint;

	void Start () {

        InitLevel();
	}
	
	void Update () {
		
	}

    public void CreateLevel()
    {
    }

    public void InitLevel()
    {
        GameManager.Instance.curLevel = this;

        SpawnPoint[] spawnPoints = this.transform.GetComponentsInChildren<SpawnPoint>();

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            SpawnPoint spawnPoint = spawnPoints[i];
            if (spawnPoint.isHostActor)
            {
                hostSpawnPoint = spawnPoint;
                Actor actor = spawnPoint.CreateActor();
                if (spawnPoint.isHostActor)
                {
                    GameManager.Instance.hostActor = actor;
                }
            }
            else
            {
                spawnPoint.CreateActor();
            }
        }
    }

    public void SpawnHostActor()
    {
        if (hostSpawnPoint != null)
        {
            Actor actor = hostSpawnPoint.CreateActor();
            GameManager.Instance.hostActor = actor;
            CGameEventManager.GetInstance().SendEvent<Actor>(enGameEvent.ActorSpawnEvent, ref actor);
        }
    }



}

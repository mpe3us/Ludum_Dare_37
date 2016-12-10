using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnersController : MonoBehaviour {

    public static SpawnersController Instance;

    public const float DelayBetweenEnemySpawn = 1.5f;
    public const float DelayBetweenIndWaves = 3f;

    [SerializeField]
    public Spawner[] Spawners;

	// Use this for initialization
	void Awake () {
        Instance = this;
	}	

    public void Spawn(Wave[] wavesToSpawn)
    {
        // Add waves to the spawners
        foreach (Wave w in wavesToSpawn)
        {
            Spawner curSpawner = this.Spawners[0];
            curSpawner.AddWave(w);
        }

        // Start spawning them
        foreach(Spawner s in this.Spawners)
        {
            s.SpawnWaves();
        }
    }

}

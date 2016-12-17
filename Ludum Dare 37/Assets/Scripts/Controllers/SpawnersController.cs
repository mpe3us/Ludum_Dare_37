using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnersController : MonoBehaviour {

    public static SpawnersController Instance;

    public const float DelayBetweenEnemySpawn = 0.9f;
    public const float DelayBetweenIndWaves = 1.2f;

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
            Spawner curSpawner = this.Spawners[Random.Range(0, this.Spawners.Length)];
            curSpawner.AddWave(w);
        }

        // Start spawning them
        foreach(Spawner s in this.Spawners)
        {
            s.SpawnWaves();
        }
    }

}

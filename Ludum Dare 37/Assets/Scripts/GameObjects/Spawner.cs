using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    [SerializeField]
    public GameObject[] WayPoints;

    private Dictionary<int, GameObject> currentEnemies;

    public bool StartSpawning;

    private List<Wave> wavesToSpawn;

    private bool currentlySpawning;

    private float curCDBetweenSpawns;
    private float curCDBetweenWaves;

    void Awake()
    {
        this.wavesToSpawn = new List<Wave>();
        this.currentEnemies = new Dictionary<int, GameObject>();
        this.StartSpawning = false;

        this.currentlySpawning = false;

        this.curCDBetweenSpawns = SpawnersController.DelayBetweenEnemySpawn;
        this.curCDBetweenWaves = SpawnersController.DelayBetweenIndWaves;
    }

    public void AddWave(Wave waveToSpawn)
    {
        this.wavesToSpawn.Add(waveToSpawn);
    }

    private IEnumerator SpawningWaves()
    {
        foreach (Wave waveToSpawn in this.wavesToSpawn)
        {
            float deltaTime;

            foreach (Wave.WaveElement waveElem in waveToSpawn.waveElems)
            {
                for (int i = 0; i < waveElem.quantity; i++)
                {
                    Enemy curEnemy = waveElem.enemy.GetCopy();
                    this.SpawnEnemy(curEnemy);
                    while (this.curCDBetweenSpawns >= 0)
                    {
                        // Wait
                        deltaTime = Time.deltaTime * GameController.GlobalSpeedFactor;
                        this.curCDBetweenSpawns -= deltaTime;
                        yield return null;
                    }
                    this.curCDBetweenSpawns = SpawnersController.DelayBetweenEnemySpawn;
                }
            }

            while (this.curCDBetweenWaves >= 0)
            {
                // Wait
                deltaTime = Time.deltaTime * GameController.GlobalSpeedFactor;
                this.curCDBetweenWaves -= deltaTime;
                yield return null;
            }
            this.curCDBetweenWaves = SpawnersController.DelayBetweenIndWaves;
        }

        this.DoneSpawning();
    }

    private void DoneSpawning()
    {
        // Clear current waves list
        this.wavesToSpawn.Clear();
        this.currentlySpawning = false;
    }

    public void SpawnWaves()
    {
        //this.currentlySpawning = true;
        StartCoroutine(this.SpawningWaves());
    }

    public void SpawnEnemy(Enemy newEnemy)
    {
        GameObject curPrefab = GameController.Instance.EnemyPrefab;
        GameObject curEnemyGO = Instantiate(curPrefab, this.transform.position, Quaternion.identity);
        curEnemyGO.transform.SetParent(this.transform, true);
        curEnemyGO.GetComponent<EnemyController>().SetEnemyData(newEnemy, this);

        SoundController.Instance.OnEnemySpawned();
    }

}

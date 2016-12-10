using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    [SerializeField]
    public GameObject[] WayPoints;

    private Dictionary<int, GameObject> currentEnemies;

    public bool StartSpawning;

    private List<Wave> wavesToSpawn;

    void Awake()
    {
        this.wavesToSpawn = new List<Wave>();
        this.currentEnemies = new Dictionary<int, GameObject>();
        this.StartSpawning = false;
    }

    public void AddWave(Wave waveToSpawn)
    {
        this.wavesToSpawn.Add(waveToSpawn);
    }

    private IEnumerator SpawningWaves()
    {
        foreach (Wave waveToSpawn in this.wavesToSpawn)
        {           

            foreach (Wave.WaveElement waveElem in waveToSpawn.waveElems)
            {
                for (int i = 0; i < waveElem.quantity; i++)
                {
                    Enemy curEnemy = waveElem.enemy.GetCopy();
                    this.SpawnEnemy(curEnemy);
                    yield return new WaitForSeconds(SpawnersController.DelayBetweenEnemySpawn);
                }
            }

            yield return new WaitForSeconds(SpawnersController.DelayBetweenIndWaves);
        }
    }

    public void SpawnWaves()
    {
        StartCoroutine(this.SpawningWaves());
    }

    public void SpawnEnemy(Enemy newEnemy)
    {
        GameObject curPrefab = GameController.Instance.EnemyPrefab;
        GameObject curEnemyGO = Instantiate(curPrefab, this.transform.position, Quaternion.identity);
        curEnemyGO.transform.SetParent(this.transform, true);
        curEnemyGO.GetComponent<EnemyController>().SetEnemyData(newEnemy, this);
    }

}

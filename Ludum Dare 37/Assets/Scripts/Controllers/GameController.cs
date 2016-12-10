using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public static GameController Instance;

    [SerializeField]
    public GameObject HomeBase;

    [SerializeField]
    public GameObject[] TurretPrefabs;

    [SerializeField]
    public GameObject EnemyPrefab;

    public Game GameInstance { get; private set; }
    
    private Dictionary<int, GameObject> currentTurrets;

    public Turret.TurretTypes currentTurretToBuy;

    private float currentWaveDelay;

    // Use this for initialization
    void Awake () {
        Instance = this;

        this.GameInstance = new Game(150, 10);
        this.currentWaveDelay = Game.TimeBetweenWaveSets;

        this.currentTurrets = new Dictionary<int, GameObject>();

        this.currentTurretToBuy = Turret.TurretTypes.BASIC;
	}

    public void BuyTurretAt(GameObject turretBase)
    {
        Turret curTurret = null;            

        switch (this.currentTurretToBuy)
        {
            case Turret.TurretTypes.BASIC:
                curTurret = new BasicTurret();
                break;
            default:
                Debug.Log("Unsupported turret type");
                break;
        }

        if (curTurret.Price > this.GameInstance.Credits)
        {
            // TODO: Inform user of insufficient credits
            return;
        }
        
        if (turretBase.tag == "Empty")
        {            

            GameObject curPrefab = this.TurretPrefabs[(int)this.currentTurretToBuy];
            Vector3 spawnPos = new Vector3(turretBase.transform.position.x, turretBase.transform.position.y + 1.0f, turretBase.transform.position.z);
            GameObject curObject = Instantiate(curPrefab, spawnPos, Quaternion.identity);
            curObject.GetComponent<TurretController>().SetTurretData(curTurret);
            curObject.transform.SetParent(this.transform, true);

            this.ChangeTurretBaseColor(turretBase);

            // Set turretBase as occupied
            turretBase.tag = "Occupied";

            // Update credits
            this.GameInstance.BuyTurret(curTurret);
        }   
        else
        {
            // TODO: Inform user of bad position
        }
    }

    public void EnemyDestroyed(Enemy enemyData)
    {
        this.GameInstance.EnemyDied(enemyData);
    }

    public void EnemyReachedCore(Enemy enemyData)
    {
        this.GameInstance.EnemyReachedCore(enemyData);
    }

    public void ChangeTurretBaseColor(GameObject turretBase)
    {
        Renderer ren = turretBase.GetComponentInChildren<Renderer>();
        Color curColor = ren.material.color;
        ren.material.color = new Color(curColor.r, curColor.g, curColor.b, 1.0f);
    }

    void Update()
    {

        if (this.GameInstance.EnemiesLeftInCurrentWave <= 0)
        {
            this.currentWaveDelay -= Time.deltaTime;

            if (this.currentWaveDelay <= 0f)
            {
                // Spawn waves
                SpawnersController.Instance.Spawn(Waves.FirstSet);

                this.GameInstance.EnemiesLeftInCurrentWave = 9;
                this.currentWaveDelay = Game.TimeBetweenWaveSets;
            }
        }

    }

}

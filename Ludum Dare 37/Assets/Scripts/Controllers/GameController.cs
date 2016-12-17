using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public static GameController Instance;

    public static float GlobalSpeedFactor = 1f;

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

    private List<Wave[]> waveSets;

    public bool GameOver { get; private set; }

    // Use this for initialization
    void Awake () {
        Instance = this;

        this.GameInstance = new Game(140, 10);
        this.currentWaveDelay = Game.TimeBetweenWaveSets;

        this.currentTurrets = new Dictionary<int, GameObject>();

        this.currentTurretToBuy = Turret.TurretTypes.BASIC;

        this.GameOver = false;

        this.CreateWaveSets();
	}

    private void CreateWaveSets()
    {
        this.waveSets = new List<Wave[]>();

        this.waveSets.Add(Waves.FirstSet);
        this.waveSets.Add(Waves.SecondSet);
        this.waveSets.Add(Waves.ThirdSet);
        this.waveSets.Add(Waves.Set4);
        this.waveSets.Add(Waves.Set5);
        this.waveSets.Add(Waves.Set6);
        this.waveSets.Add(Waves.Set7);
        this.waveSets.Add(Waves.Set8);
        this.waveSets.Add(Waves.Set9);
        this.waveSets.Add(Waves.Set10);

        this.GameInstance.TotalWaveSets = this.waveSets.Count;
    }

    public void SetTurretToBuy(GameObject buyTurretButtonGO)
    {
        this.currentTurretToBuy = buyTurretButtonGO.GetComponent<BuyTurretButton>().TurretData.TurretType;
        GameUIController.Instance.ChangePositionOfSelectionIndicator(buyTurretButtonGO);
    }

    public void BuyTurretAt(GameObject turretBase)
    {
        Turret curTurret = null;            

        switch (this.currentTurretToBuy)
        {
            case Turret.TurretTypes.BASIC:
                curTurret = new BasicTurret();
                break;
            case Turret.TurretTypes.SNIPER:
                curTurret = new SniperTurret();
                break;  
            case Turret.TurretTypes.ROCKET:
                curTurret = new RocketTurret();
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

            // Play sound
            SoundController.Instance.OnTurretPlaced();
        }   
        else
        {
            // TODO: Inform user of bad position
        }
    }

    public void EnemyDestroyed(Enemy enemyData)
    {
        this.GameInstance.EnemyDied(enemyData);
        SoundController.Instance.OnEnemyDead();
    }

    public void EnemyReachedCore(Enemy enemyData)
    {
        bool gameIsOver = this.GameInstance.EnemyReachedCore(enemyData);
        if (gameIsOver)
        {
            this.SetGameOver(false);
        }

        SoundController.Instance.OnCoreDamaged();
    }

    public void ChangeTurretBaseColor(GameObject turretBase)
    {
        Renderer ren = turretBase.GetComponentInChildren<Renderer>();
        Color curColor = ren.material.color;
        ren.material.color = new Color(curColor.r, curColor.g, curColor.b, 1.0f);
    }

    private void ChangeSpeedFactor()
    {
        if (Input.GetKeyDown("space"))
        {
            GlobalSpeedFactor = 10f;
        }
        else if (Input.GetKeyUp("space"))
        {
            GlobalSpeedFactor = 1f;
        }
    }

    void Update()
    {

        this.ChangeSpeedFactor();

        if (this.GameInstance.EnemiesLeftInCurrentWave <= 0 && !this.GameOver)
        {
            float deltaTime = Time.deltaTime * GlobalSpeedFactor;
            this.currentWaveDelay -= deltaTime;

            GameUIController.Instance.UpdateTimeToNextWave(this.currentWaveDelay);

            if (this.currentWaveDelay <= 0f)
            {
                GameUIController.Instance.ResetTimeToNextWave();

                Wave[] curWaveSet = this.waveSets[this.GameInstance.CurrentWaveSet - 1];

                // Spawn waves
                SpawnersController.Instance.Spawn(curWaveSet);

                this.GameInstance.EnemiesLeftInCurrentWave = GetEnemiesInWaveSet(curWaveSet);
                this.currentWaveDelay = Game.TimeBetweenWaveSets;                            
            
            }
        }

        if (this.GameInstance.CurrentWaveSet > this.GameInstance.TotalWaveSets)
        {
            //Debug.Log(this.GameInstance.CurrentWaveSet);
            //Debug.Log(this.GameInstance.TotalWaveSets);
            this.SetGameOver(true);
        }
    }

    public static int GetEnemiesInWaveSet(Wave[] waveSet)
    {
        int count = 0;

        foreach (Wave w in waveSet)
        {
            foreach(Wave.WaveElement we in w.waveElems)
            {
                count += we.quantity;
            }
        }

        return count;
    }

    private void SetGameOver(bool gameWon)
    {
        if (this.GameOver)
        {
            return;
        }

        SoundController.Instance.OnGameOver();
        GameUIController.Instance.OnGameOver(gameWon);

        this.GameOver = true;
        //Debug.Log("Game Over!");
    }

}

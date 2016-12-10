using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {


    public Enemy EnemyData { get; private set; }

    public Spawner spawnerData { get; private set; }

    private bool dataSet;

    private GameObject nextWayPoint;
    private int nextWayPointIndex;

    private bool goingTowardsFinalPoint;

    private bool isDead;

    void Awake()
    {
        this.isDead = false;
        dataSet = false;
        this.nextWayPointIndex = 0;
        this.goingTowardsFinalPoint = false;
    }

    public void SetEnemyData(Enemy data, Spawner spawnerData)
    {
        this.EnemyData = data;
        this.spawnerData = spawnerData;

        // Set the first waypoint
        this.SetNextWayPoint();

        this.SetColor();

        this.dataSet = true;
    }

    private void SetColor()
    {
        this.transform.GetChild(0).GetComponent<Renderer>().material.color = this.EnemyData.enemyColor;
    }

    void Update()
    {
        if (!this.dataSet || this.isDead)
        {
            return;
        }

        float deltaTime = GameController.GlobalSpeedFactor * Time.deltaTime;

        Vector3 direction = this.nextWayPoint.transform.position - this.transform.position;
        float distThisFrame = this.EnemyData.MovementSpeed * deltaTime;

        if (direction.magnitude <= distThisFrame)
        {
            // We have reached our target
            this.SetNextWayPoint();
        }
        else {
            // Move towards target
            this.transform.Translate(direction.normalized * distThisFrame, Space.World);
        }
        
    }

    private void SetNextWayPoint()
    {
        int numWaypoints = spawnerData.WayPoints.Length;

        if (this.goingTowardsFinalPoint)
        {
            // Make player lose life
            GameController.Instance.EnemyReachedCore(this.EnemyData);
            this.DestroyMe();
        }
        else if (numWaypoints <= this.nextWayPointIndex)
        {
            // Set HomeBase as final waypoint
            this.goingTowardsFinalPoint = true;
            this.nextWayPoint = GameController.Instance.HomeBase;
        }
        else
        {
            this.nextWayPoint = spawnerData.WayPoints[this.nextWayPointIndex];
            this.nextWayPointIndex++;
        }
    }
    
    public void TakeDamage(Turret turret)
    {
        if (!this.EnemyData.IsAlive)
        {
            return;
        }

        this.EnemyData.HitPoints -= turret.Damage;

        if (this.EnemyData.HitPoints <= 0f)
        {
            GameController.Instance.EnemyDestroyed(this.EnemyData);
            this.DestroyMe();
        }
    }

    private void DestroyMe()
    {
        this.isDead = true;
        this.transform.GetChild(0).gameObject.SetActive(false);
        Destroy(this.gameObject, 5f);
    }

}

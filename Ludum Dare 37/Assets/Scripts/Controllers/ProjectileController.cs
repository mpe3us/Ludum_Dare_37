using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour {

    public Turret TurretData { get; private set; }

    private bool dataSet;

    private GameObject targetGO;

    private GameObject firePoint;

    private bool objectInFront;
    RaycastHit rayHit;
    private bool stopProjectileAtEndOfFrame;
    private int EnemyLayerMask;

    public void SetProjectileData(Turret turret, GameObject target, GameObject firePoint)
    {
        this.TurretData = turret;

        this.targetGO = target;

        this.EnemyLayerMask = 1 << 9;

        this.firePoint = firePoint;

        this.dataSet = true;
    }

    void Update()
    {
        if (!dataSet)
        {
            return;
        }

        if (this.targetGO == null)
        {
            this.DestroyMe();
        }
        else if (!this.targetGO.transform.GetChild(0).gameObject.activeSelf)
        {
            this.DestroyMe();
        }

        Vector3 enemyPos = new Vector3(this.targetGO.transform.position.x, firePoint.transform.position.y, this.targetGO.transform.position.z);
        Vector3 direction = enemyPos - this.transform.position;

        float distThisFrame = this.TurretData.ProjectileSpeed * Time.deltaTime;

        this.objectInFront = Physics.Raycast(this.transform.position, direction, out this.rayHit, distThisFrame, this.EnemyLayerMask);

        // Move towards target
        this.transform.Translate(direction.normalized * distThisFrame, Space.World);

        if (this.objectInFront)
        {
            this.targetGO.GetComponent<EnemyController>().TakeDamage(this.TurretData);
            this.DestroyMe();
        }

    }

    private void DestroyMe()
    {
        Destroy(this.gameObject);
    }


}

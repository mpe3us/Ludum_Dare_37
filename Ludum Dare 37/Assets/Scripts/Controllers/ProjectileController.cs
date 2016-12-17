using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour {

    public Turret TurretData { get; private set; }

    public TurretController TurretControllerData { get; private set; }

    private bool dataSet;

    private GameObject targetGO;

    private GameObject firePoint;

    private bool objectInFront;
    RaycastHit rayHit;
    private bool stopProjectileAtEndOfFrame;
    private int EnemyLayerMask;

    public void SetProjectileData(TurretController turret, GameObject target, GameObject firePoint)
    {

        this.TurretControllerData = turret;
        this.TurretData = turret.TurretData;

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

        float deltaTime = GameController.GlobalSpeedFactor * Time.deltaTime;

        Vector3 enemyPos = new Vector3(this.targetGO.transform.position.x, firePoint.transform.position.y, this.targetGO.transform.position.z);
        Vector3 direction = enemyPos - this.transform.position;

        float orgRot = this.transform.eulerAngles.y;
        float newRot = Quaternion.LookRotation(direction).eulerAngles.y;
        // compute shortest rotation path
        if (orgRot - newRot > 180.0f)
        {
            orgRot -= 360.0f;
        }
        else if (orgRot - newRot < -180.0f)
        {
            orgRot += 360.0f;
        }
        this.transform.rotation = Quaternion.Euler(0, Mathf.SmoothStep(orgRot, newRot, deltaTime * 20f), 0); // Rotation of projectile


        float distThisFrame = this.TurretData.ProjectileSpeed * deltaTime;

        this.objectInFront = Physics.Raycast(this.transform.position, direction, out this.rayHit, distThisFrame, this.EnemyLayerMask);

        // Move towards target
        this.transform.Translate(direction.normalized * distThisFrame, Space.World);

        if (this.objectInFront)
        {
            if (this.TurretData.TurretType == Turret.TurretTypes.ROCKET)
            {
                this.RocketHit();
            }
            else
            {
                this.targetGO.GetComponent<EnemyController>().TakeDamage(this.TurretData);

            }
            
            this.SpawnHitEffect();
            SoundController.Instance.PlaySFX(this.TurretControllerData.HitSFX);
            this.DestroyMe();

        }
    }

    // AOE dmg
    private void RocketHit()
    {

        Collider[] hitColliders = Physics.OverlapSphere(this.targetGO.transform.position, this.TurretData.aoe_radius, this.EnemyLayerMask);

        GameObject curTarget = null;

        foreach (Collider col in hitColliders)
        {
            curTarget = col.gameObject;
            curTarget.transform.parent.GetComponent<EnemyController>().TakeDamage(this.TurretData);
        }        
    }
    
    private void SpawnHitEffect()
    {
        GameObject hitEffect = Instantiate(this.TurretControllerData.HitEffect, this.transform.position, Quaternion.Euler(90f, 0, 0));
        Destroy(hitEffect, 2f);
    }

    private void DestroyMe()
    {
        Destroy(this.gameObject);
    }


}

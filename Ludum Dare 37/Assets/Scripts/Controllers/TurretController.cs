using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour {

    public Turret TurretData { get; private set; }

    [SerializeField]
    public GameObject TurretBase;

    [SerializeField]
    private GameObject ProjectilePrefab;

    [SerializeField]
    private GameObject FirePoint;

    [SerializeField]
    public GameObject HitEffect;

    [SerializeField]
    public AudioClip FireSFX;

    [SerializeField]
    public AudioClip HitSFX;

    private bool dataSet;

    private GameObject currentTarget;

    private int EnemyLayerMask;

    private float curFireCD;

    void Awake()
    {
        this.dataSet = false;
    }

    public void SetTurretData(Turret turret)
    {
        this.TurretData = turret;

        this.EnemyLayerMask = 1 << 9;

        this.curFireCD = 0;

        this.dataSet = true;
    }

    void Update()
    {
        if (!this.dataSet)
        {
            return;
        }

        float deltaTime = GameController.GlobalSpeedFactor * Time.deltaTime;

        this.curFireCD -= deltaTime;

        if (this.currentTarget == null)
        {
            return;
        }

        Vector3 direction = this.currentTarget.transform.position - this.transform.position;
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
        this.transform.rotation = Quaternion.Euler(0, Mathf.SmoothStep(orgRot, newRot, deltaTime * 10f), 0);

        float diff = Mathf.Abs(orgRot - newRot);

        if (this.curFireCD <= 0 && diff <= 10f)
        {
            // Fire projectile
            GameObject curProjectile = Instantiate(this.ProjectilePrefab, this.FirePoint.transform.position, Quaternion.identity);
            curProjectile.transform.SetParent(this.transform, true);
            curProjectile.GetComponent<ProjectileController>().SetProjectileData(this, this.currentTarget, this.FirePoint);

            this.curFireCD = this.TurretData.RateOfFire;

            // Play SFX
            SoundController.Instance.PlaySFX(this.FireSFX);
        }

    }

    void FixedUpdate()
    {
        if (!this.dataSet)
        {
            return;
        }
        
        Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, this.TurretData.Range, this.EnemyLayerMask);

        this.currentTarget = null;

        foreach(Collider col in hitColliders)
        {
           if (this.currentTarget == null)
           {
                this.currentTarget = col.transform.parent.gameObject;
           }
           else
            {
                float curDist = Vector3.Distance(this.currentTarget.transform.position, this.transform.position);
                float newDist = Vector3.Distance(col.transform.parent.position, this.transform.position);
                if (newDist < curDist)
                {
                    this.currentTarget = col.transform.parent.gameObject;
                }
            }
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperTurret : Turret {

    public SniperTurret()
    {
        this.Name = "Sniper Turret";
        this.Description = "Long range turret with high damage but slow fire rate.";

        this.TurretType = TurretTypes.SNIPER;

        this.Price = 120;
        this.Damage = 4f;
        this.Range = 15f;
        this.RateOfFire = 7f;
        this.ProjectileSpeed = 16f;

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicTurret : Turret {

    public BasicTurret()
    {
        this.Name = "Basic Turret";
        this.Description = "The weakest and cheapest turret that fires single standard projectiles.";

        this.TurretType = TurretTypes.BASIC;

        this.Price = 65;
        this.Damage = 2f;
        this.Range = 6f;
        this.RateOfFire = 2f;
        this.ProjectileSpeed = 12f;

    }

}

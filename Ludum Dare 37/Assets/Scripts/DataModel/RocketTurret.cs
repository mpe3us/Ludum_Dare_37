using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketTurret : Turret {

    public RocketTurret()
    {
        this.Name = "Rocket Turret";
        this.Description = "Short range turret that fires rockets dealing damage in a small area.";

        this.TurretType = TurretTypes.ROCKET;

        this.Price = 90;
        this.Damage = 2f;
        this.Range = 4.5f;
        this.RateOfFire = 3f;
        this.ProjectileSpeed = 7.5f;

        this.aoe_radius = 3.5f;
    }
}

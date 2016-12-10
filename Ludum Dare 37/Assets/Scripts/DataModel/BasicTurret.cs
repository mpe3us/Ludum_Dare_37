using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicTurret : Turret {

    public BasicTurret()
    {
        this.Name = "Basic Turret";
        this.Description = "The weakest and cheapest turret that fires single standard projectiles.";

        this.Price = 100;
        this.Damage = 1f;
        this.Range = 5f;
        this.RateOfFire = 2f;
        this.ProjectileSpeed = 10f;

    }

}

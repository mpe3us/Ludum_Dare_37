using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Turret {

    private static int currentID;

    public enum TurretTypes
    {
        BASIC
    }

    // Properties
    public int ID { get; private set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Level { get; set; }
    public TurretTypes TurretType { get; set; }

    // General
    public Vector3 TurretPosition { get; set; }

    // Stats
    public int Price { get; set; }
    public float Damage { get; set; }
    public float Range { get; set; }
    public float RateOfFire { get; set; } // Per second
    public float ProjectileSpeed { get; set; }

    public Turret()
    {
        currentID++;
        ID = currentID;

        Level = 1;
    }

    public void printInfo()
    {
        Debug.Log("----- Turret Info -----");
        Debug.Log("ID: " + ID);
        Debug.Log("Name: " + Name);
        Debug.Log("Description: " + Description);
        Debug.Log("Turret Type: " + TurretType);
        Debug.Log("Level: " + Level);
    }

}

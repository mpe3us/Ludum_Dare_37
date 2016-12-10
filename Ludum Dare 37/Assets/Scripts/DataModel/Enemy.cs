using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy {

    private static int currentID;

    public enum EnemyTypes
    {
        BASIC
    }

    // Properties
    public int ID { get; private set; }
    public EnemyTypes EnemyType { get; private set; }

    // General
    public bool IsAlive { get; set;  }
    public Color enemyColor { get; set; }

    // Stats
    public int CreditsValue { get; set; }
    public float HitPoints { get; set; }
    public float MovementSpeed { get; set; }

    public Enemy(EnemyTypes enemyType, float hp, float movements, int credValue, Color color)
    {
        currentID++;

        this.ID = currentID;

        this.IsAlive = true;

        this.EnemyType = enemyType;

        this.enemyColor = color;

        this.CreditsValue = credValue;
        this.HitPoints = hp;
        this.MovementSpeed = movements;
    }

    public Enemy GetCopy()
    {
        return new Enemy(this.EnemyType, this.HitPoints, this.MovementSpeed, this.CreditsValue, this.enemyColor);
    }

}

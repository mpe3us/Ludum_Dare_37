﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game {

    public int Credits { get; private set; }

    public float HomeBaseHP { get; private set; }

    public const float TimeBetweenWaveSets = 5f;

    public int EnemiesLeftInCurrentWave { get; set; }

    public int TotalWaveSets { get; set; }
    public int CurrentWaveSet { get; set; }

    public int StartingCredits { get; set; }

    public Game(int startCredits, float homeBaseHP)
    {
        this.Credits = startCredits;
        this.HomeBaseHP = homeBaseHP;
        this.CurrentWaveSet = 1;

        this.StartingCredits = startCredits;
    }
	

    public void BuyTurret(Turret turretToBuy)
    {
        this.Credits -= turretToBuy.Price;
    }

    public void EnemyDied(Enemy enemy)
    {
        this.Credits += enemy.CreditsValue;
        this.EnemyRemoved();
    }

    // Returns true on GameOver
    public bool EnemyReachedCore(Enemy enemy)
    {
        bool gameOver = false;

        this.HomeBaseHP -= 1f;
        this.EnemyRemoved();

        if (this.HomeBaseHP <= 0f)
        {
            gameOver = true;
        }
        return gameOver;
    }

    private void EnemyRemoved()
    {
        this.EnemiesLeftInCurrentWave -= 1;
        if (this.EnemiesLeftInCurrentWave <= 0)
        {
            // Gain extra credits for clearing current wave set
            float creditGain = this.StartingCredits / 2f * (1f + this.CurrentWaveSet / 10f);
            this.Credits += Mathf.RoundToInt(creditGain);
            this.CurrentWaveSet++;  
        }
    }

}

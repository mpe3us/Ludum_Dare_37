using System.Collections;
using System.Collections.Generic;

public class Game {

    public int Credits { get; private set; }

    public float HomeBaseHP { get; private set; }

    public const float TimeBetweenWaveSets = 5f;

    public int EnemiesLeftInCurrentWave { get; set; }

    public Game(int startCredits, float homeBaseHP)
    {
        this.Credits = startCredits;
        this.HomeBaseHP = homeBaseHP;
    }
	

    public void BuyTurret(Turret turretToBuy)
    {
        this.Credits -= turretToBuy.Price;
    }

    public void EnemyDied(Enemy enemy)
    {
        this.Credits += enemy.CreditsValue;
        this.EnemiesLeftInCurrentWave -= 1;
    }

    // Returns true on GameOver
    public bool EnemyReachedCore(Enemy enemy)
    {
        bool gameOver = false;

        this.HomeBaseHP -= 1f;
        this.EnemiesLeftInCurrentWave -= 1;

        if (this.HomeBaseHP <= 0f)
        {
            gameOver = true;
        }
        return gameOver;
    }

}

using System.Collections;
using System.Collections.Generic;

public class Game {

    public int Credits { get; private set; }

    public float HomeBaseHP { get; private set; }

    public const float TimeBetweenWaveSets = 5f;

    public int EnemiesLeftInCurrentWave { get; set; }

    public int TotalWaveSets { get; set; }
    public int CurrentWaveSet { get; set; }

    public Game(int startCredits, float homeBaseHP)
    {
        this.Credits = startCredits;
        this.HomeBaseHP = homeBaseHP;
        this.CurrentWaveSet = 1;
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
            this.CurrentWaveSet++;
        }
    }

}

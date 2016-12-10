using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waves {

    public static Enemy BasicEnemyLvl_1 = new Enemy(Enemy.EnemyTypes.BASIC, 2f, 1f, 20, Color.green);
    public static Enemy BasicEnemyLvl_2 = new Enemy(Enemy.EnemyTypes.BASIC, 4f, 1.1f, 30, Color.yellow);
    public static Enemy BasicEnemyLvl_3 = new Enemy(Enemy.EnemyTypes.BASIC, 8f, 1.2f, 40, Color.red);

    public static Wave.WaveElement[] BasicWaveArrLvl_1 =
        new Wave.WaveElement[1] { new Wave.WaveElement(BasicEnemyLvl_1, 3) };
    public static Wave.WaveElement[] BasicWaveArrLvl_2 =
        new Wave.WaveElement[3] { new Wave.WaveElement(BasicEnemyLvl_1, 1), new Wave.WaveElement(BasicEnemyLvl_2, 1), new Wave.WaveElement(BasicEnemyLvl_1, 1) };
    public static Wave.WaveElement[] BasicWaveArrLvl_3 =
    new Wave.WaveElement[3] { new Wave.WaveElement(BasicEnemyLvl_2, 1), new Wave.WaveElement(BasicEnemyLvl_3, 1), new Wave.WaveElement(BasicEnemyLvl_2, 1) };

    public static Wave BasicWaveLvl_1 = new Wave(BasicWaveArrLvl_1);
    public static Wave BasicWaveLvl_2 = new Wave(BasicWaveArrLvl_2);
    public static Wave BasicWaveLvl_3 = new Wave(BasicWaveArrLvl_3);

    public static Wave[] FirstSet = new Wave[2] { BasicWaveLvl_1, BasicWaveLvl_1 };
    public static Wave[] SecondSet = new Wave[3] { BasicWaveLvl_1, BasicWaveLvl_1, BasicWaveLvl_2 };
    public static Wave[] ThirdSet = new Wave[4] { BasicWaveLvl_2, BasicWaveLvl_2, BasicWaveLvl_3, BasicWaveLvl_1 };
}

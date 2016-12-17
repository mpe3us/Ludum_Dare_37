using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waves {

    public static Enemy BasicEnemyLvl_1 = new Enemy(Enemy.EnemyTypes.BASIC, 2f, 1f, 15, Color.green);
    public static Enemy BasicEnemyLvl_2 = new Enemy(Enemy.EnemyTypes.BASIC, 5f, 2f, 20, Color.cyan);
    public static Enemy BasicEnemyLvl_3 = new Enemy(Enemy.EnemyTypes.BASIC, 7f, 3f, 25, Color.blue);
    public static Enemy BasicEnemyLvl_4 = new Enemy(Enemy.EnemyTypes.BASIC, 10f, 4f, 30, Color.white);
    public static Enemy BasicEnemyLvl_5 = new Enemy(Enemy.EnemyTypes.BASIC, 14f, 5f, 35, Color.yellow);
    public static Enemy BasicEnemyLvl_6 = new Enemy(Enemy.EnemyTypes.BASIC, 10f, 8f, 40, Color.magenta);
    public static Enemy BasicEnemyLvl_7 = new Enemy(Enemy.EnemyTypes.BASIC, 20f, 5f, 50, Color.grey);
    public static Enemy BasicEnemyLvl_8 = new Enemy(Enemy.EnemyTypes.BASIC, 25f, 7f, 55, Color.red);
    public static Enemy BasicEnemyLvl_9 = new Enemy(Enemy.EnemyTypes.BASIC, 30f, 5f, 60, Color.black);

    public static Wave.WaveElement[] BasicWaveArrLvl_1 =
        new Wave.WaveElement[1] { new Wave.WaveElement(BasicEnemyLvl_1, 3) };
    public static Wave.WaveElement[] BasicWaveArrLvl_2 =
        new Wave.WaveElement[3] { new Wave.WaveElement(BasicEnemyLvl_1, 1), new Wave.WaveElement(BasicEnemyLvl_2, 1), new Wave.WaveElement(BasicEnemyLvl_1, 1) };
    public static Wave.WaveElement[] BasicWaveArrLvl_3 =
        new Wave.WaveElement[3] { new Wave.WaveElement(BasicEnemyLvl_2, 1), new Wave.WaveElement(BasicEnemyLvl_3, 1), new Wave.WaveElement(BasicEnemyLvl_2, 1) };
    public static Wave.WaveElement[] BasicWaveArrLvl_4 =
        new Wave.WaveElement[1] { new Wave.WaveElement(BasicEnemyLvl_3, 3) };
    public static Wave.WaveElement[] BasicWaveArrLvl_5 =
        new Wave.WaveElement[3] { new Wave.WaveElement(BasicEnemyLvl_3, 2), new Wave.WaveElement(BasicEnemyLvl_4, 1), new Wave.WaveElement(BasicEnemyLvl_2, 2) };
    public static Wave.WaveElement[] BasicWaveArrLvl_6 =
        new Wave.WaveElement[1] { new Wave.WaveElement(BasicEnemyLvl_5, 3) };
    public static Wave.WaveElement[] BasicWaveArrLvl_7 =
        new Wave.WaveElement[1] { new Wave.WaveElement(BasicEnemyLvl_6, 3) };
    public static Wave.WaveElement[] BasicWaveArrLvl_8 =
        new Wave.WaveElement[1] { new Wave.WaveElement(BasicEnemyLvl_7, 3) };
    public static Wave.WaveElement[] BasicWaveArrLvl_9 =
         new Wave.WaveElement[1] { new Wave.WaveElement(BasicEnemyLvl_8, 3) };
    public static Wave.WaveElement[] BasicWaveArrLvl_10 =
     new Wave.WaveElement[1] { new Wave.WaveElement(BasicEnemyLvl_9, 3) };

    public static Wave BasicWaveLvl_1 = new Wave(BasicWaveArrLvl_1);
    public static Wave BasicWaveLvl_2 = new Wave(BasicWaveArrLvl_2);
    public static Wave BasicWaveLvl_3 = new Wave(BasicWaveArrLvl_3);
    public static Wave BasicWaveLvl_4 = new Wave(BasicWaveArrLvl_4);
    public static Wave BasicWaveLvl_5 = new Wave(BasicWaveArrLvl_5);
    public static Wave BasicWaveLvl_6 = new Wave(BasicWaveArrLvl_6);
    public static Wave BasicWaveLvl_7 = new Wave(BasicWaveArrLvl_7);
    public static Wave BasicWaveLvl_8 = new Wave(BasicWaveArrLvl_8);
    public static Wave BasicWaveLvl_9 = new Wave(BasicWaveArrLvl_9);
    public static Wave BasicWaveLvl_10 = new Wave(BasicWaveArrLvl_10);

    public static Wave[] FirstSet = new Wave[2] { BasicWaveLvl_1, BasicWaveLvl_1 };
    public static Wave[] SecondSet = new Wave[3] { BasicWaveLvl_1, BasicWaveLvl_1, BasicWaveLvl_2 };
    public static Wave[] ThirdSet = new Wave[4] { BasicWaveLvl_2, BasicWaveLvl_2, BasicWaveLvl_3, BasicWaveLvl_1 };
    public static Wave[] Set4 = new Wave[4] { BasicWaveLvl_4, BasicWaveLvl_4, BasicWaveLvl_2, BasicWaveLvl_2 };
    public static Wave[] Set5 = new Wave[5] { BasicWaveLvl_4, BasicWaveLvl_4, BasicWaveLvl_5, BasicWaveLvl_5, BasicWaveLvl_1 };
    public static Wave[] Set6 = new Wave[7] { BasicWaveLvl_4, BasicWaveLvl_4, BasicWaveLvl_5, BasicWaveLvl_5, BasicWaveLvl_6, BasicWaveLvl_3, BasicWaveLvl_2 };
    public static Wave[] Set7 = new Wave[9] { BasicWaveLvl_5, BasicWaveLvl_5, BasicWaveLvl_6, BasicWaveLvl_6, BasicWaveLvl_7, BasicWaveLvl_7, BasicWaveLvl_3, BasicWaveLvl_3, BasicWaveLvl_8 };
    public static Wave[] Set8 = new Wave[10] { BasicWaveLvl_5, BasicWaveLvl_5, BasicWaveLvl_6, BasicWaveLvl_6, BasicWaveLvl_7, BasicWaveLvl_7, BasicWaveLvl_3, BasicWaveLvl_3, BasicWaveLvl_8, BasicWaveLvl_9 };
    public static Wave[] Set9 = new Wave[13] { BasicWaveLvl_5, BasicWaveLvl_5, BasicWaveLvl_6, BasicWaveLvl_6, BasicWaveLvl_7, BasicWaveLvl_7, BasicWaveLvl_3, BasicWaveLvl_3, BasicWaveLvl_8, BasicWaveLvl_9, BasicWaveLvl_10, BasicWaveLvl_9, BasicWaveLvl_8 };
    public static Wave[] Set10 = new Wave[14] { BasicWaveLvl_6, BasicWaveLvl_6, BasicWaveLvl_7, BasicWaveLvl_7, BasicWaveLvl_8, BasicWaveLvl_8, BasicWaveLvl_5, BasicWaveLvl_5, BasicWaveLvl_9, BasicWaveLvl_9, BasicWaveLvl_10, BasicWaveLvl_10, BasicWaveLvl_9, BasicWaveLvl_8 };
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waves {

    public static Enemy BasicEnemyLvl_1 = new Enemy(Enemy.EnemyTypes.BASIC, 2f, 1f, 20, Color.clear);

    public static Wave.WaveElement[] BasicWaveArr = new Wave.WaveElement[1] { new Wave.WaveElement(BasicEnemyLvl_1, 3) };
    public static Wave BasicWave = new Wave(BasicWaveArr);

    public static Wave[] FirstSet = new Wave[3] { BasicWave, BasicWave, BasicWave };
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave {

    public class WaveElement
    {
        public Enemy enemy;
        public int quantity;

        public WaveElement(Enemy e, int q)
        {
            enemy = e;
            quantity = q;
        }
    }

    public WaveElement[] waveElems { get; private set; }

    public Wave(WaveElement[] elems)
    {
        this.waveElems = elems;
    }
}

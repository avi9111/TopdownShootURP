using System;

[Serializable]
public class Wave
{
    //当前波的敌人
    public Enemy enemy;
    //敌人数
    public int count;
    //敌人出生间隔
    public float timeBetweenSpawn;
}

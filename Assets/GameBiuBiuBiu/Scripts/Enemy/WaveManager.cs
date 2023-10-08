using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//出生点管理
public class WaveManager : MonoBehaviour
{
    public Transform[] spawnPoints;
    public Wave[] waves;
    //当前波怪物
    private Wave _thisWave;
    //当前波索引
    private int _thisWaveIndex;
    //剩余怪物数量
    public static int _surplusCount = 1;
    //难度
    public static int level;
    //关卡数
    public static int levelNumber;
    //倒计时生怪
    public static int timeBetweenWaves = 3;
    //倒计是否结束
    public static bool isTimeBetweenWaves = false;
    //是否开始倒计时
    public static bool isCountingDown;
    void Start()
    {
        level = 1;
        levelNumber = 1;
        if (spawnPoints.Length == 0)
        {
            Debug.Log("未设置出生点");
        }
        StartCoroutine(NextWaveCoroutine());
    }

    IEnumerator NextWaveCoroutine()
    {
        isCountingDown = true; //这里会显示商店。。。。
        _thisWaveIndex++;
        isTimeBetweenWaves = false;
        yield return new WaitForSeconds(timeBetweenWaves);
        isCountingDown = false;
        isTimeBetweenWaves = true;
        if (_thisWaveIndex - 1 < waves.Length)
        {
            levelNumber++;
            _thisWave = waves[_thisWaveIndex - 1];
            _surplusCount = _thisWave.count;
            for (int i = 0; i < _thisWave.count; i++)
            {
                int spawnIndex = Random.Range(0, spawnPoints.Length);
                Enemy enemy = Instantiate(_thisWave.enemy, spawnPoints[spawnIndex].position, Quaternion.identity);
                yield return new WaitForSeconds(_thisWave.timeBetweenSpawn);
                LevelStageManager.SafeAdd(enemy);
            }
        }
        else
        {
            //重新从第一波开始
            _thisWaveIndex = 0;
            //提升难度
            level++;
        }
    }

    private void Update()
    {
        OnEnableDeath();
    }

    //减少怪物
    public static void OnDeath()
    {
        _surplusCount--;
    }

    //生成下一波怪物
    private void OnEnableDeath()
    {
        if (_surplusCount <= 0 && isTimeBetweenWaves)
        {
            StartCoroutine(NextWaveCoroutine());
        }
    }
}

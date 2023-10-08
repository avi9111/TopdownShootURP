using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStageManager : MonoBehaviour
{
    List<Enemy> enemies = new List<Enemy>();
    public static LevelStageManager Inst;
    private void Awake()
    {
        Inst = this;
    }
    void Start()
    {
        
    }

    public void Add(Enemy enemy) {
        enemies.Add(enemy);
        CullingMananger.Inst.AddSphere(enemy.gameObject);
    }
    public static void SafeAdd(Enemy enemy) {
        if (Inst == null) {
            return;
        }
        Inst.Add(enemy);
    }
    public Enemy FindNearest(Transform trans)
    {
        float dist = 9999.0f;
        Enemy _thisEnemy = null;
        foreach(Enemy enemy in enemies)
        {
            if(enemy == null) { continue; }

            float _thisDist = (enemy.transform.position - trans.position).sqrMagnitude;
            if (_thisDist < dist) {
                dist = _thisDist;
                _thisEnemy = enemy;
            }
        }

        return _thisEnemy;
    }

}

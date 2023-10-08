using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootLine : MonoBehaviour
{
    PlayerGun playerGun;
    //最小和最大攻击数值
    private int minDamage;
    private int maxDamage;
    //暴击加成百分比
    private int criticalMultiplier;
    //暴击率
     private float criticalChance;
     //是否暴击
     private bool isCritical;
     private int damage;//伤害

    void Awake()
    {
        playerGun = GameObject.Find("Gun").GetComponent<PlayerGun>();
    }

    //进入触发器 穿透子弹
    private void OnTriggerEnter2D(Collider2D other)
    {
        minDamage = PlayerStats.instance.minDamage;
        maxDamage = PlayerStats.instance.maxDamage;
        criticalMultiplier = PlayerStats.instance.criticalMultiplier;
        criticalChance = PlayerStats.instance.criticalChance;
        //计算伤害
        isCritical = Random.value < criticalChance;
        damage = Random.Range(minDamage, maxDamage);
        if(isCritical){
            damage *= criticalMultiplier;
        }
        
        if (other.CompareTag("Box") || other.CompareTag("Wall") || other.CompareTag("Enemy"))
        {
            Vector3 pos = other.bounds.ClosestPoint(transform.position);    //这个就是碰撞点
            // Debug.Log("击中");
            var sparks = Instantiate(playerGun.sparks, pos, Quaternion.identity);
            //0，05秒后销毁
            Destroy(sparks, 0.2f);
            Destroy(gameObject);
        }

        if (other.CompareTag("Box"))
        {
            //造成伤害
            Box box = other.GetComponent<Box>();
            box?.TakeDamage(damage);
        }

        if (other.CompareTag("Enemy"))
        {
            Debug.Log(damage);
            Debug.Log(isCritical);
            //造成伤害
            Enemy enemy = other.GetComponent<Enemy>();
            enemy?.TakeDamage(damage, isCritical);
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy : MonoBehaviour
{
    public float startHealth;//开始血量
    public float health;//当前血量
    public bool isDead;

    public float damage = 10;//敌人伤害
    public float hitRate = 1.0f;//攻速

    private float _lastHit;//计时器
    public LayerMask whatToHit;//可以攻击哪个图层
    //攻击距离
    private float hitDistance = 2.0f;

    public ParticleSystem deathParticle;//死亡特效

    [Header("ai导航属性")]
    private AIPath aiPath;
    private Transform target; //目标
    // private WaveManager waveManager;

    private void Start()
    {
        aiPath = GetComponent<AIPath>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        // waveManager = GameObject.FindGameObjectWithTag("Wave").GetComponent<WaveManager>();
        health = startHealth * Mathf.Max(WaveManager.level, 1);
    }

    private void Update()
    {
        if (target == null)
        {
            return;
        }
        aiPath.destination = target.position;
        if (aiPath.reachedDestination)
        {
            //发起攻击
            if (Time.time > _lastHit + 1 / hitRate)
            {
                Hit();
                _lastHit = Time.time;
            }
        }
    }

    //受击
    public void TakeDamage(int damage, bool isCritical)
    {
        //TODO伤害飘字
        ResourceManager.instance.GetDamageText(transform.position, damage, isCritical);
        health -= damage;
        if (health <= 0 || isDead)
        {
            isDead = true;
            Destroy(gameObject);
            //死亡特效
            if (deathParticle == null) { Debug.Log("没有特效"); return; };
            ParticleSystem ps = Instantiate(deathParticle, transform.position, Quaternion.identity);
            Destroy(ps.gameObject, ps.main.duration);
            //怪物数-1
            WaveManager.OnDeath();
            //TODO死亡声音
            ResourceManager.instance.PlayEnemyBlastSound();
            //概率爆金币
            if (Random.value < PlayerStats.instance.getGoldChance)
            {
                for (int i = 0; i < PlayerStats.instance.getGoldNumber; i++)
                {
                    ResourceManager.instance.PlayFallSound();//音效
                    ResourceManagerNew.Instance.GetPooledObject("Prefabs/Gold", transform.position, Quaternion.identity);
                }
            }
        }
    }



    //攻击
    void Hit()
    {
        //Debug.Log(damage);
        //怪物朝向
        Vector3 targetDirection = (target.position - transform.position).normalized;
        //射线
        RaycastHit2D hit2D = Physics2D.Raycast(transform.position, targetDirection, aiPath.endReachedDistance + hitDistance, whatToHit);
        // Debug.DrawLine(transform.position, targetDirection, Color.red);//发射看不见的红色线
        if (hit2D.collider != null)
        {
            Debug.Log("enemy hit mammmmm:" + damage + " to " + hit2D.collider.name) ;
            //造成伤害
            PlayerController playerController = hit2D.collider.GetComponent<PlayerController>();
            playerController?.TakeDamage(damage);
        }

    }
}

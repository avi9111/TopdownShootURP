using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public float startHealth;//开始血量
    public float health;//当前血量
    public bool isDead;

    private void Start()
    {
        health = startHealth;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0 || isDead){
            isDead = true;
            Destroy(gameObject);
        }
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//武器系统
public class PlayerGun : MonoBehaviour
{
    private Camera mainCamera;
    public Transform firePoint;//射击点
    public Transform shootLine;//子弹线
    public Transform muzzleFlame;//开枪火焰

    //武器系统
    public FireMode fireMode = FireMode.Burst;

    private bool isReloading = false;//是否是装弹状态
    public float nextShotTime;//单发开火间隔
    private int shotsNumber;//射击次数
    private int fireRate;//射速
    private float reloadTime;//换子弹时间
    public bool isTriggerReleased;
    public GameObject sparks;//击中特效

    private void Awake()
    {
        fireRate = PlayerStats.instance.fireRate;
        reloadTime = PlayerStats.instance.reloadTime;
        mainCamera = Camera.main;
    }

    private void Update()
    {
        
        ResourceManager.instance.GetBulletsNumberText(PlayerStats.instance.clipSize - shotsNumber);
    }

    //扣动扳机
    public void OnDownFire(Rigidbody2D rb)
    {
        if (isReloading)
        {
            ResourceManager.instance.PlayNoBulletSound();
            return;
        }
        if (shotsNumber >= PlayerStats.instance.clipSize)
        {
            
            OnChangeover();
            return;
        }
        switch (fireMode)
        {
            case FireMode.Single:
                if (isTriggerReleased == false) return;
                Fire(rb);
                isTriggerReleased = false;
                break;
            case FireMode.Burst:
                if (Time.time < nextShotTime) return;
                nextShotTime = Time.time + 1 / (float)fireRate;
                Fire(rb);
                break;
        }
        // Debug.Log("扣动扳机");
    }
    //松开扳机
    public void OnUpFire()
    {
        isTriggerReleased = true;
        // Debug.Log("松开扳机");
    }

    //重新装弹
    public void OnChangeover()
    {
        StartCoroutine(ReloadCoroutine());
    }

    IEnumerator ReloadCoroutine()
    {
        // Debug.Log("重新装弹");
        isReloading = true;
        yield return new WaitForSeconds(reloadTime);
        shotsNumber = 0;
        isReloading = false; 
        ResourceManager.instance.PlayChangeBulletSound();
    }

    //开火
    private void Fire(Rigidbody2D rb)
    {   
        shotsNumber++;
        //音效
        ResourceManager.instance.PlayShootSound();
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        // Vector2 shootDirection = mousePosition - firePoint.position;//开火点向鼠标位置的向量
        Vector2 shootDirection = transform.up;//人物面向向量

        //子弹轨道给个偏移，达到扫射效果
        shootDirection.x += Random.Range(-0.15f, 0.15f);
        shootDirection.y += Random.Range(-0.15f, 0.15f);

        //后座力
        rb.AddRelativeForce(-transform.position*40, ForceMode2D.Force);

        float angle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;
        var rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        Transform line = Instantiate(shootLine, firePoint.position, rotation);
        //0，05秒后销毁
        Destroy(line.gameObject, 0.05f);
        // Debug.DrawLine(firePointPosition, shootDirection * 100, Color.red);//发射看不见的红色线
        GetMuzzleFlame(muzzleFlame, firePoint);
    }


    //枪口火焰
    private void GetMuzzleFlame(Transform muzzleFlame, Transform firePoint)
    {
        Transform flame = Instantiate(muzzleFlame, firePoint.position, firePoint.rotation);
        flame.SetParent(firePoint);
        float randomSize = Random.Range(0.6f, 0.9f);
        flame.localScale = new Vector3(randomSize, randomSize, randomSize);//每次开火让火焰大小不一致
        //0，05秒后销毁
        Destroy(flame.gameObject, 0.05f);
    }
}

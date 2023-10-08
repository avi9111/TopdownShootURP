using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.UI;
using static PlayerController;

public class PlayerController : MonoBehaviour
{
    public enum AttackType
    {
        HandRotate,
        AutoAim,        //瞄准最近的，还是要按射击
        Full            //自动射击
    }
    [SerializeField]
    private Rigidbody2D rb;
    private Vector2 movement;
    // public PlayerGun playerGun;
    private Camera mainCamera;
    private Animator animator;
    public Transform playerBody;

    private float speed;//移动速度
    public bool isDead;
    public ParticleSystem deathParticle;//死亡特效
    public float shakeDuration = 0.5f;
    public float shakeMagnitude = 0.1f;
    private VariableJoystickMove joystickMove;
    private VariableJoystickShoot JoystickShoot;

    //public AttackType attackType  = AttackType.HandRotate;
    public AttackType attackType = AttackType.AutoAim;
    public static PlayerController Inst;
    private void Awake()
    {
        Inst = this;
    }
    void Start()
    {
        Time.timeScale = 1;
        speed = PlayerStats.instance.speed;
        PlayerStats.instance.health = PlayerStats.instance.startHealth;//TODO先设定默认开始血量等于总血

        //if(rb==null)
        rb = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
        animator = GetComponentInChildren<Animator>();
        StartCoroutine(AddHealthCoroutine());
        joystickMove = ResourceManager.instance.joystickMove;
        JoystickShoot = ResourceManager.instance.JoystickShoot;
        ResourceManager.instance.SetGoldNumberText(PlayerStats.instance.GoldNumber);
    }
    void Update()
    {
        //获取虚拟摇杆的位置
        float horizontalInput = joystickMove.Horizontal;
        float verticalInput = joystickMove.Vertical;
        movement = new Vector2(horizontalInput, verticalInput).normalized;
        
        UpdateRotation();
        // UpdateMouse();
        UpdateAnimation(horizontalInput, verticalInput);
        if (isDead)
        {
            Invoke("GameOver", 0.3f); //延迟1秒调用
        }
    }

    
    /// <summary>
    /// 自动回血 
    /// </summary>
    /// <returns></returns>
    IEnumerator AddHealthCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(PlayerStats.instance.addHealthTime);
            if (PlayerStats.instance.health < PlayerStats.instance.startHealth)
            {
                PlayerStats.instance.health = Mathf.Min(PlayerStats.instance.health + PlayerStats.instance.addHealth, PlayerStats.instance.startHealth);
                //加血飘字
                ResourceManager.instance.GetaddHealthText(transform.position, PlayerStats.instance.addHealth);
            }
        }
    }
    /// <summary>
    /// rigidbody 移动方法，AddForce,MovePosition,等可参考：
    /// https://www.youtube.com/watch?v=fcKGqxUuENk
    /// </summary>
    private void FixedUpdate()
    {
        //移动代码
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);

        //移动 rigidbody 测试
        //rb.AddForce(movement * speed * Time.fixedDeltaTime, ForceMode2D.Force);

        //这是肯定可以的。。。
        //Vector2 ve = movement * speed * Time.fixedDeltaTime;
        //transform.position = transform.position+ new Vector3(ve.x, ve.y, 0);
    }

    Vector3 tmpDir;
    Vector3 tmpForward;
    //旋转
    private void UpdateRotation()
    {
        //tmpDir = Vector3.zero;
        //获取虚拟摇杆的位置
        float horizontalInput = JoystickShoot.Horizontal;
        float verticalInput = JoystickShoot.Vertical;
        if (horizontalInput != 0)
        {
            //计算了一个向量与 y 轴正方向之间的夹角
            float angle = Mathf.Atan2(horizontalInput, verticalInput) * Mathf.Rad2Deg;
            
            if (attackType == AttackType.HandRotate)
                //Vector3.forward 表示 z 轴正方向,方法创建了一个绕 z 轴旋转 angle 度的旋转四元数
                playerBody.rotation = Quaternion.AngleAxis(-angle, Vector3.forward);
            else if (attackType == AttackType.AutoAim)
            {
                Enemy enemyInst = LevelStageManager.Inst.FindNearest(transform);
                //Debug.DrawLine(enemyInst.transform.position, transform.position);
                if (enemyInst != null) {
                    //var forward = enemyInst.transform.position - transform.position;
                    var forward = enemyInst.transform.position - new Vector3(transform.position.x, transform.position.y, playerBody.position.z);
                    tmpForward = forward;
                    //计算错误的，必须算 Player 的平面投影
                    //playerBody.rotation = Quaternion.LookRotation(new Vector3(forward.x,forward.y,playerBody.position.z),Vector3.forward);

                    //在当前平面上的投影。。。。。
                    tmpDir = Vector3.ProjectOnPlane(forward, Vector3.forward);
                    float tmpAngle = Mathf.Atan2(tmpDir.x, tmpDir.y) * Mathf.Rad2Deg;
                    playerBody.rotation = Quaternion.AngleAxis(-tmpAngle, Vector3.forward);

                    //playerBody.rotation = Quaternion.LookRotation(tmpDir, Vector3.forward);//这个tmpDir 已经不在原点，所以LookRotation 是按原点算法，这么算不行地！！！1
                    
                    //playerBody.LookAt(playerBody.position + tmpDir);//这样为什么不行呢？？
                }
            }
            //持续按下射击
            animator.SetBool("isShoot", true);
            ResourceManager.instance.playerGun.OnDownFire(rb);
        }
        else
        {
            animator.SetBool("isShoot", false);
            ResourceManager.instance.playerGun.OnUpFire();
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(playerBody.transform.position, playerBody.transform.position + tmpDir);

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(playerBody.transform.position, playerBody.transform.position + tmpForward);
    }


    //玩家面向鼠标方向,如果需要的话可以开启
    private void UpdateMouse()
    {
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition); // 获取鼠标在世界坐标系下的位置
        Vector2 shootDirection = mousePosition - playerBody.position;//鼠标的屏幕坐标-玩家屏幕坐标=玩家面向鼠标位置的向量
        float angle = Mathf.Atan2(shootDirection.x, shootDirection.y) * Mathf.Rad2Deg;
        playerBody.rotation = Quaternion.AngleAxis(-angle, Vector3.forward);
    }

    //播放动画
    private void UpdateAnimation(float horizontalInput, float verticalInput)
    {
        if (movement.x != 0 || movement.y != 0)
        {
            animator.SetBool("isWalk", true);
        }
        else
        {
            animator.SetBool("isWalk", false);
        }
    }

    //受击
    public void TakeDamage(float damage)
    {
        StartCoroutine(Shake());
        PlayerStats.instance.health -= damage;
        if (PlayerStats.instance.health <= 0 || isDead)
        {
            isDead = true;
            //死亡特效
            if (deathParticle == null) { Debug.Log("没有特效"); return; };
            ParticleSystem ps = Instantiate(deathParticle, transform.position, Quaternion.identity);
            Destroy(ps.gameObject, ps.main.duration);
        }
    }

    //人物闪烁
    public IEnumerator Shake()
    {
        // 定义抖动效果的持续时间
        float shakeDuration = 0.2f;

        // 定义闪烁效果的持续时间
        float blinkDuration = 0.1f;

        // 定义闪烁效果之间的间隔
        float blinkInterval = 0.05f;

        // 获取SpriteRenderer组件
        SpriteRenderer spriteRenderer = playerBody.GetComponent<SpriteRenderer>();

        // 在抖动效果的持续时间内，在起始位置和随机位置之间进行插值
        for (float t = 0; t < shakeDuration; t += Time.deltaTime)
        {
            // 闪烁角色
            if (t % blinkInterval < blinkDuration)
            {
                spriteRenderer.enabled = !spriteRenderer.enabled;
            }
            else
            {
                spriteRenderer.enabled = true;
            }
            yield return null;
        }
        // 确保Sprite可见
        spriteRenderer.enabled = true;
    }

    private void GameOver()
    {
        //停止游戏
        Time.timeScale = 0;
        //弹出结束菜单
        ResourceManager.instance.gameOverMenu.SetActive(true);
        Destroy(gameObject);
    }
}

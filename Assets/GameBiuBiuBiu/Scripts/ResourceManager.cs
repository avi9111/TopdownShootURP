using UnityEngine;
using TMPro;
using WeChatWASM;

//资源管理器
public class ResourceManager : MonoBehaviour
{
    public static ResourceManager instance;
    public PlayerGun playerGun;//机枪1
    public DamageText damageText;//飘字特效预制体
    [Header("金币")]
    public GameObject gold;
    [Header("UI")]
    public GameObject gameOverMenu;
    public TMP_Text goldNumberText;//金币数量文本
    public TMP_Text bulletsNumberText;//子弹数量文本
    [Header("摇杆")]
    public VariableJoystickMove joystickMove;
    public VariableJoystickShoot JoystickShoot;
    public AudioSource audioSource;  //播放器组件
    [Header("音乐")]
    public AudioClip menuMusic;//菜单背景音乐
    public AudioClip gameMusic;//游戏背景音乐
    [Header("音效")]
    public AudioClip clickSound;//按钮点击音效
    public AudioClip selectSound;//按钮选择音效
    public AudioClip shootSound;//枪声音效
    public AudioClip changeoverSound;//换弹音效
    public AudioClip enemyBlastSound;//敌人爆炸音效
    public AudioClip noBulletSound;//没子弹射击
    public AudioClip changeBulletSound;//换子弹

    void Awake()
    {
        instance = this;
    }
    //显示血量金币数
    public void SetGoldNumberText(int number)
    {
        goldNumberText.SetText(number.ToString());
    }

    //显示剩余子弹数
    public void GetBulletsNumberText(int number)
    {
        bulletsNumberText.SetText(number.ToString());
    }

    //伤害飘字
    public void GetDamageText(Vector3 position, int damage, bool isCritical)
    {
        // 实例化伤害文本
        DamageText damageTextRes = Instantiate(damageText, position, Quaternion.identity);
        // 设置伤害文本
        damageTextRes.SetUp(damage, isCritical);
    }
    //加血飘字
    public void GetaddHealthText(Vector3 position, int damage)
    {
        // 实例化伤害文本
        DamageText damageTextRes = Instantiate(damageText, position, Quaternion.identity);
        // 设置伤害文本
        damageTextRes.SetAddHealth(damage);
    }
    
    //金额不够飘字
    public void GetGoodsText(Vector3 position, string text)
    {
        // 实例化伤害文本
        DamageText damageTextRes = Instantiate(damageText, position, Quaternion.identity);
        // 设置伤害文本
        damageTextRes.SetGoodsText(text);
    }

    public void PlayMenuMusic()
    {
        audioSource.clip = menuMusic;//设定播放的片段
        audioSource.Play();//播放背景音乐
    }
    public void PlayGameMusic()
    {
        audioSource.clip = gameMusic;
        audioSource.Play();
    }
    public void PlayClickSound()
    {
        audioSource.PlayOneShot(clickSound);
    }

    public void PlaySelectSound()
    {
        audioSource.PlayOneShot(selectSound);
    }

    public void PlayShootSound()
    {
        audioSource.PlayOneShot(shootSound);
    }
    public void PlayChangeoverSound()
    {
        audioSource.PlayOneShot(changeoverSound);
    }
    public void PlayEnemyBlastSound()
    {
        audioSource.PlayOneShot(enemyBlastSound);
    }
    public void PlayNoBulletSound()
    {
        audioSource.PlayOneShot(noBulletSound);
    }
    public void PlayChangeBulletSound()
    {
        audioSource.PlayOneShot(changeBulletSound);
    }
    public void PlayGoldPickUpSound()
    {
        //金币拾取音效
        AudioClip gold_Pick_up = (AudioClip)ResourceManagerNew.Instance.LoadResourceSync("Sounds/gold_Pick_up");
        audioSource.PlayOneShot(gold_Pick_up);
    }
    public void PlayFallSound()
    {
        //金币掉落音效
        AudioClip gold_fall = (AudioClip)ResourceManagerNew.Instance.LoadResourceSync("Sounds/gold_fall");
        audioSource.PlayOneShot(gold_fall);
    }
}

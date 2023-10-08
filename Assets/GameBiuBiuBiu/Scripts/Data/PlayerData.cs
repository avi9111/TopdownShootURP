using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "TopDownFPS/PlayerData", order = 0)]
public class PlayerData : ScriptableObject
{
    public float speed;//移动速度
    public float startHealth;//开始血量
    public float health;//当前血量
    public bool isDead;//是否死亡
    public int clipSize;//弹夹容量
    public int fireRate;//射速
    public float reloadTime;//换子弹时间

    //最小和最大攻击数值
    public int minDamage;
    public int maxDamage;

    public int criticalMultiplier;//暴击加成百分比

    public float criticalChance;//暴击率
    public int addHealth = 5;//加血量
    public float addHealthTime = 5f;//多久加血

    public float getGoldChance = 0.2f;//爆金币率
    public int getGoldNumber = 1;//爆金币数量
    public int GoldNumber = 0;//金币数量
}
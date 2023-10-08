using UnityEngine;

//管理人物数据
public class PlayerStats : MonoBehaviour
{

    //public static PlayerStats Instance { get { return new PlayerStats(); } }
    //public static PlayerStats instance;
    public static PlayerStats instance;
    public PlayerData playerData;
    public PlayerData playerDataTemplate;

    /// <summary>
    /// 打开 Menu 有两种状态；一是原来的GameOver Menu；而是新的点击头像也显示的Menu
    /// </summary>
    public bool showGameCMenu;
    private void Awake()
    {
        instance = this;
        if (playerData == null)
        {
            playerData = Instantiate(playerDataTemplate);
        }
    }

    public float speed
    {
        get { return playerData == null ? 0 : playerData.speed; }
        set { playerData.speed = value; }
    }
    public int GoldNumber
    {
        get { return playerData == null ? 0 : playerData.GoldNumber; }
        set { playerData.GoldNumber = value; }
    }
    public float getGoldChance
    {
        get { return playerData == null ? 0 : playerData.getGoldChance; }
        set { playerData.getGoldChance = value; }
    }
    public int getGoldNumber
    {
        get { return playerData == null ? 0 : playerData.getGoldNumber; }
        set { playerData.getGoldNumber = value; }
    }
    public float startHealth
    {
        get { return playerData.startHealth; }
        set { playerData.startHealth = value; }
    }
    public float health
    {
        get { return playerData.health; }
        set { playerData.health = value; }
    }
    public bool isDead
    {
        get { return playerData.isDead; }
        set { playerData.isDead = value; }
    }
    public int clipSize
    {
        get { return playerData.clipSize; }
        set { playerData.clipSize = value; }
    }
    public int fireRate
    {
        get { return playerData.fireRate; }
        set { playerData.fireRate = value; }
    }
    public float reloadTime
    {
        get { return playerData.reloadTime; }
        set { playerData.reloadTime = value; }
    }
    public int minDamage
    {
        get { return playerData.minDamage; }
        set { playerData.minDamage = value; }
    }
    public int maxDamage
    {
        get { return playerData.maxDamage; }
        set { playerData.maxDamage = value; }
    }
    public int criticalMultiplier
    {
        get { return playerData.criticalMultiplier; }
        set { playerData.criticalMultiplier = value; }
    }

    public float criticalChance
    {
        get { return playerData.criticalChance; }
        set { playerData.criticalChance = value; }
    }
    public int addHealth
    {
        get { return playerData.addHealth; }
        set { playerData.addHealth = value; }
    }
    public float addHealthTime
    {
        get { return playerData.addHealthTime; }
        set { playerData.addHealthTime = value; }
    }
}
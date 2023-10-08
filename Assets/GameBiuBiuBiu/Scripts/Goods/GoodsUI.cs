using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//出生点管理
[RequireComponent(typeof(Button))]
public class GoodsUI : MonoBehaviour
{
    [Header("UI")]
    public GameObject image;
    public GameObject title;
    public GameObject price;
    [Header("数据")]
    public GoodsInfo[] goodsInfo;
    //当前商品
    private GoodsInfo _thisGoodsInfo;
    //当前索引
    private int _thisIndex;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnClickCloseGoods);
    }

    //启用事件，只执行一次。当脚本组件被启用的时候执行一次。在Awake后调用
    private void OnEnable()
    {
        UpdateInfo();
    }

    public void UpdateInfo()
    {
        //获取随机索引
        _thisIndex = Random.Range(0, goodsInfo.Length);
        _thisGoodsInfo = goodsInfo[_thisIndex];
        //随机生成价格
        _thisGoodsInfo.price = Random.Range(5, 21);
        //随机int数值
        _thisGoodsInfo.valueInt = Random.Range(1, 4);
        //随机float数值,保留一位小数
        _thisGoodsInfo.valueFloat = Mathf.Round(Random.Range(0.1f, 0.3f) * 10f) / 10f;
        //UI显示
        image.GetComponent<Image>().sprite = _thisGoodsInfo.image;
        price.GetComponent<TextMeshProUGUI>().SetText("-" + _thisGoodsInfo.price.ToString());
        //显示文案
        switch (_thisGoodsInfo.name)
        {
            case "speed":
            case "reloadTime":
            case "fireRate":
            case "criticalChance":
            case "addHealthTime":
            case "getGoldChance":
                title.GetComponent<TextMeshProUGUI>().SetText(_thisGoodsInfo.title.ToString() + _thisGoodsInfo.valueFloat.ToString());
                break;
            case "startHealth":
            case "health":
            case "clipSize":
            case "minDamage":
            case "maxDamage":
            case "criticalMultiplier":
            case "addHealth":
            case "getGoldNumber":
            case "GoldNumber":
                title.GetComponent<TextMeshProUGUI>().SetText(_thisGoodsInfo.title.ToString() + _thisGoodsInfo.valueInt.ToString());
                break;
            default:
                Debug.LogError("未知的属性类型：" + _thisGoodsInfo.name);
                break;
        }
    }

    //修改对应数值
    public bool UpdateData()
    {
        //如果钱不够，提升金额不足
        if (PlayerStats.instance.GoldNumber < _thisGoodsInfo.price)
        {
            ResourceManager.instance.GetGoodsText(transform.position, "金币不足");
            return false;
        }

        //扣钱
        PlayerStats.instance.GoldNumber -= _thisGoodsInfo.price;

        switch (_thisGoodsInfo.name)
        {
            case "speed":
                PlayerStats.instance.speed += _thisGoodsInfo.valueFloat;
                break;
            case "reloadTime":
                PlayerStats.instance.reloadTime -= _thisGoodsInfo.valueFloat;
                //显示不小0
                PlayerStats.instance.reloadTime = Mathf.Min(PlayerStats.instance.reloadTime, 0);
                break;
            case "criticalChance":
                PlayerStats.instance.criticalChance += _thisGoodsInfo.valueFloat;
                //显示不大于1
                PlayerStats.instance.criticalChance = Mathf.Max(PlayerStats.instance.criticalChance, 1);
                break;
            case "addHealthTime":
                PlayerStats.instance.addHealthTime -= _thisGoodsInfo.valueFloat;
                PlayerStats.instance.addHealthTime = Mathf.Min(PlayerStats.instance.addHealthTime, 1);
                break;
            case "getGoldChance":
                PlayerStats.instance.getGoldChance += _thisGoodsInfo.valueFloat;
                PlayerStats.instance.getGoldChance = Mathf.Max(PlayerStats.instance.getGoldChance, 1);
                break;
            case "startHealth":
                PlayerStats.instance.startHealth += _thisGoodsInfo.valueInt;
                break;
            case "health":
                PlayerStats.instance.health += _thisGoodsInfo.valueInt;
                break;
            case "clipSize":
                PlayerStats.instance.clipSize += (int)_thisGoodsInfo.valueInt;
                break;
            case "fireRate":
                PlayerStats.instance.fireRate += (int)_thisGoodsInfo.valueInt;
                break;
            case "minDamage":
                PlayerStats.instance.minDamage += (int)_thisGoodsInfo.valueInt;
                //最新攻击不能大于最大攻击
                PlayerStats.instance.minDamage = Mathf.Min(PlayerStats.instance.minDamage, PlayerStats.instance.maxDamage);
                break;
            case "maxDamage":
                PlayerStats.instance.maxDamage += (int)_thisGoodsInfo.valueInt;
                break;
            case "criticalMultiplier":
                PlayerStats.instance.criticalMultiplier += (int)_thisGoodsInfo.valueInt;
                break;
            case "addHealth":
                PlayerStats.instance.addHealth += (int)_thisGoodsInfo.valueInt;
                break;
            case "getGoldNumber":
                PlayerStats.instance.getGoldNumber += (int)_thisGoodsInfo.valueInt;
                break;
            case "GoldNumber":
                PlayerStats.instance.GoldNumber += (int)_thisGoodsInfo.valueInt;
                break;
            default:
                Debug.LogError("未知的属性类型：" + _thisGoodsInfo.name);
                break;
        }
        return true;
    }

    //关闭goods
    public void OnClickCloseGoods()
    {
        //播放音效
        ResourceManager.instance.PlayClickSound();
        if (!UpdateData()) return;
        gameObject.SetActive(false);
        //更新金币文字显示
        ResourceManager.instance.SetGoldNumberText(PlayerStats.instance.GoldNumber);
    }
}

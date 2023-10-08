using System;
using System.Collections;
using System.Globalization;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// 管理倒计时？？ ResultView也会用到这个。。。。
/// </summary>
public class WaveForm : MonoBehaviour
{
    //public Image countDownAimBg;
    public TMP_Text countDownText;
    public TMP_Text waveNumberText;
    public CanvasGroup canvasGroup;
    public GameObject goods;
    public GameObject goods1;
    public GameObject goods2;
    public GameObject goods3;
    bool isCountingDown;
    float countDownTime;
    bool isShowGoods;
    static WaveForm _inst;

    public Image imgAim0;
    public Image imgAim1;
    bool aimCheck = false;
    public static bool IsShowLevelResultUI() {
        if (_inst == null) return false;
        return _inst.isShowGoods;
    }
    private void Awake()
    {
        _inst = this;
        ResourceManager.instance.PlayGameMusic();//设定播放的片段
        //countDownText.alpha = 1;
        //canvasGroup.alpha = 1;
        countDownTime = WaveManager.timeBetweenWaves;
        EventDispatcher.Instance.Regist(EventMazeGame.OnShowLevelUI,OnUIShown);
        EventDispatcher.Instance.Regist(EventMazeGame.OnHideLevelUI, OnUiHide);

        EventTriggerListener.Get(imgAim0.gameObject).onClick += OnAutoAimClick;
        EventTriggerListener.Get(imgAim1.gameObject).onClick += OnAutoAimClick;
        
        ////////////////// 玩家版本 //////////////////////////
        aimCheck = true;//先设为true ，下面代码，再默认翻转，默认需要自动瞄准
        OnAutoAimClick(null);
    }

    void SetImage(bool check) {
        if (check)
        {
            imgAim0.gameObject.SetActive(false);
            imgAim1.gameObject.SetActive(true);
        }
        else
        {
            imgAim0.gameObject.SetActive(true);
            imgAim1.gameObject.SetActive(false);
        }
    }

    void OnAutoAimClick(GameObject sender) {
        
        aimCheck = !aimCheck;
        if (aimCheck == true) {
            PlayerController.Inst.attackType = PlayerController.AttackType.AutoAim;
        }
        else
            PlayerController.Inst.attackType = PlayerController.AttackType.HandRotate;
        SetImage(aimCheck);
    }

    private void OnDestroy()
    {
        EventDispatcher.Instance.UnRegist(EventMazeGame.OnShowLevelUI, OnUIShown);
        EventDispatcher.Instance.UnRegist(EventMazeGame.OnHideLevelUI, OnUiHide);
    }
    void OnUIShown() {
        var maskImg = GetComponent<Image>();
        maskImg.enabled = true;

        //countDownAimBg.gameObject.SetActive(true);
    }
    void OnUiHide() {
        var maskImg = GetComponent<Image>();
        maskImg.enabled = false;

        //countDownAimBg.gameObject.SetActive(false );
    }
    private void Update()
    {
        //关卡数
        int miniShownLevel = Math.Max(1, WaveManager.levelNumber - 1);
        waveNumberText.SetText(miniShownLevel.ToString());
        isCountingDown = WaveManager.isCountingDown;/////////// 现在通过 countDownTime 判断是否执行倒数 ////////////////
        countDownText.gameObject.SetActive(isCountingDown);
        //倒计时
        if (!isCountingDown)
        {
            //隐藏商店
            goods1.SetActive(false);
            goods2.SetActive(false);
            goods3.SetActive(false);
            isShowGoods = true;
            countDownTime = WaveManager.timeBetweenWaves;
            EventDispatcher.Instance.DispatchEvent(EventMazeGame.OnHideLevelUI);
            return;
        }
        else
        {
            if (isShowGoods && WaveManager.levelNumber!=1)
            {
                //显示商店
                goods1.SetActive(true);
                goods2.SetActive(true);
                goods3.SetActive(true);
                isShowGoods = false;
                EventDispatcher.Instance.DispatchEvent(EventMazeGame.OnShowLevelUI);
            }
        }

        
        countDownTime -= Time.deltaTime;

        if (countDownTime % 1 > 0.5f)
        {
            countDownText.alpha -= Time.deltaTime * 2;
        }
        else
        {
            countDownText.alpha += Time.deltaTime * 2;
        }
        if (countDownTime < 0.1f)
        {
            //隐藏倒计时
            isCountingDown = false;
            canvasGroup.alpha = 0;
            countDownText.alpha = 0;
            //gameObject.SetActive(false);

            var maskImg = GetComponent<Image>();
            maskImg.enabled = false;
            EventDispatcher.Instance.DispatchEvent(EventMazeGame.OnHideLevelUI);
        }
        else
        {
            //显示倒计时
            canvasGroup.alpha = 1;
            countDownText.alpha = 1;
            countDownText.SetText(Mathf.Round(countDownTime).ToString(CultureInfo.InvariantCulture));
            EventDispatcher.Instance.DispatchEvent(EventMazeGame.OnShowLevelUI);
        }
    }
}

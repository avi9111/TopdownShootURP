using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
/// <summary>
/// GameOver-UI 逻辑
/// </summary>
public class GameOver : MonoBehaviour
{
    public TextMeshProUGUI title;
    public Button quitBtn;
    private void Awake()
    {
        // gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        if (PlayerStats.instance.showGameCMenu == true)
        {
            Time.timeScale = 0;
            title.text = " 加油，少年";
            quitBtn.GetComponentInChildren<TextMeshProUGUI>().text = "关闭菜单";
        }
        else
        {
            title.text = " 别气馁，少年";
            quitBtn.GetComponentInChildren<TextMeshProUGUI>().text = "退出应用（无用）";
        }
    }
    private void OnDisable()
    {
        PlayerStats.instance.showGameCMenu = false;
        Time.timeScale = 1;
    }
    private void OnDestroy()
    {
    }
    public void OnClikeRestart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Game");
    }

    public void OnClikeMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    /// <summary>
    /// 大退，没什么用了。。。
    /// </summary>
    public void OnClikeQuit()
    {
        if (PlayerStats.instance.showGameCMenu == true)
        {
            gameObject.SetActive(false);
            
        }
        else {
            Application.Quit();
        }
        
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
/// <summary>
/// GameOver-UI �߼�
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
            title.text = " ���ͣ�����";
            quitBtn.GetComponentInChildren<TextMeshProUGUI>().text = "�رղ˵�";
        }
        else
        {
            title.text = " �����٣�����";
            quitBtn.GetComponentInChildren<TextMeshProUGUI>().text = "�˳�Ӧ�ã����ã�";
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
    /// ���ˣ�ûʲô���ˡ�����
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

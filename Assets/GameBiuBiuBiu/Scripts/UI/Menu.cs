using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public ToggleGroup toggleGroup;

    private Toggle currentSelection => toggleGroup.GetFirstActiveToggle();
    private Toggle onToggle;
    private AudioSource audioSource;

    private void Start()
    {
        ResourceManager.instance.PlayMenuMusic();//设定播放的片段

        var toggles = toggleGroup.GetComponentsInChildren<Toggle>();
        foreach (var toggle in toggles)
        {
            toggle.onValueChanged.AddListener(_ => OnTogglevalueChanged(toggle));
        }
        currentSelection.onValueChanged?.Invoke(true);
    }

    private void OnTogglevalueChanged(Toggle toggle)
    {
        if (currentSelection == onToggle)
        {
            ResourceManager.instance.PlayClickSound();
            switch (toggle.name)
            {
                case "GameStart":
                    SceneManager.LoadScene("Game");
                    break;
                case "Settings":
                    //Debug.LogWarning(message: "TOD0:Open settings form...");
                    SceneManager.LoadScene("Demo 1 - Dungeon Tilemap");
                    break;
                case "Quit":
                    Application.Quit();
                    break;
                default:
                    throw new UnityException(message: "Toggle name is invalid.");

            }
            return;
        }
        if (toggle.isOn)
        {
            ResourceManager.instance.PlaySelectSound();//播放音效
            onToggle = toggle;
            onToggle.transform.Find("Label").GetComponent<TMP_Text>().color = Color.yellow;
        }
        else
        {
            onToggle.transform.Find("Label").GetComponent<TMP_Text>().color = Color.white;
        }
    }
}

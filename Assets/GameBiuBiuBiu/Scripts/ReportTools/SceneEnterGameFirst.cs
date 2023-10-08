//using System.Diagnostics;
using WeChatWASM;
using UnityEngine;
/// <summary>
/// 此脚本应放在，Game.unity
/// </summary>
public class SceneEnterGameFirst:MonoSingleton<SceneEnterGameFirst>
{
    void Start()
    {
        DontDestroyOnLoad(this);
        Debug.LogError("ReportGameStart() once");
        WX.ReportGameStart();
        
    }
}

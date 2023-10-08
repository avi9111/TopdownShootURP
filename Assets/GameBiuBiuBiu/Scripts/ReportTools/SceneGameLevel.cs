using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using WeChatWASM;

public class SceneGameLevel : MonoBehaviour
{
    void Start()
    {

        ReportSceneOption opt = new ReportSceneOption();
        opt.sceneId = 1002;//game
        opt.complete = (res) =>
        {
            Debug.Log("ReportScene complete" + res);
        };
        opt.success = (res) =>
            {
                Debug.Log("ReportScene success: " + res);
            };
        opt.fail = (res) =>
            {
                Debug.Log("ReportScene fail: " + res);
        };
        Debug.LogError("report Scene 1002");
        WX.ReportScene(opt);
    }

}

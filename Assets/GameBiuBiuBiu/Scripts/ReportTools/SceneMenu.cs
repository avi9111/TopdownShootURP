using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WeChatWASM;

public class SceneMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.LogError("report Scene 1001");
        ReportSceneOption opt = new ReportSceneOption();
        opt.sceneId = 1001;//menu

        Debug.LogError("report Scene 1001 £¿£¿xx^*&^");
        WX.ReportScene(opt);
    }

}

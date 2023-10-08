using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : MonoBehaviour
{
    public float destroyTime = 10.0f;
    private void Start() {
        StartCoroutine(OnSetDestroy());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag =="Player"){
            PlayerStats.instance.GoldNumber += 1;
            ResourceManager.instance.SetGoldNumberText(PlayerStats.instance.GoldNumber);
            ResourceManager.instance.PlayGoldPickUpSound();//音效
            ResourceManagerNew.Instance.RecyclePooledObject("Prefabs/Gold", gameObject);
        }
    }

    //销毁
    IEnumerator OnSetDestroy(){
        while (true)
        {
            yield return new WaitForSeconds(destroyTime);
            ResourceManagerNew.Instance.RecyclePooledObject("Prefabs/Gold", gameObject);
        }
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowTarget : MonoBehaviour
{
    
    [Tooltip("//跟随的目标")]
    public Transform target;
    public bool isTopDownVision = true;
    [Header("如果是俯视觉，则是offset忽略（无用）")]
    [Tooltip("//跟随的高度")]
    public float height = 5f;
    [Header("若是Light2D,则distance == 0")]
    [Tooltip("//跟随的距离")]
    public float distance = 3f;
    

    //float z_height;
    private void Awake()
    {
        //z_height = transform.position.z;
    }
    void Start()
    {

    }

    void LateUpdate()
    {
        //Vector3 forward = transform.forward;
        //forward.y = 0;

        //Vector3 newForward = Vector3.Lerp(forward, target.forward, 1 * Time.deltaTime);
        if (target == null) return;
        if(isTopDownVision)
        {
            transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
        }
        else
            transform.position = target.position - target.forward * distance + Vector3.up * height;

        // 不需要Look at （俯冲视觉）
        //transform.LookAt(target);
    }
}
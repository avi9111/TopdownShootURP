using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowTarget : MonoBehaviour
{
    
    [Tooltip("//�����Ŀ��")]
    public Transform target;
    public bool isTopDownVision = true;
    [Header("����Ǹ��Ӿ�������offset���ԣ����ã�")]
    [Tooltip("//����ĸ߶�")]
    public float height = 5f;
    [Header("����Light2D,��distance == 0")]
    [Tooltip("//����ľ���")]
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

        // ����ҪLook at �������Ӿ���
        //transform.LookAt(target);
    }
}
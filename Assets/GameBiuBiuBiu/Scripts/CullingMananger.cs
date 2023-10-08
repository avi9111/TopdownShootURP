using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CullingMananger : MonoBehaviour
{
    public static CullingMananger Inst;
    public float updateInterval = 0.5f;
    float _lastHit;
    List<Renderer> spheres = new List<Renderer>();
    public Transform player;
    public float playerViewRange=3f;
    private void Awake()
    {
        Inst = this; 
    }

    public void AddSphere(GameObject gameObject)
    {
        var render = gameObject.GetComponentInChildren<Renderer>();
        if (render == null)
        {
            render=gameObject.GetComponentInChildren<SpriteRenderer>(); 
        }

        if (render == null) return;
        AddSphere(render);
    }
    public void AddSphere(Renderer render) {
        spheres.Add(render); 
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > _lastHit + updateInterval)
        {
            DoCulling();

            _lastHit = Time.time;
        }
    }

    void DoCulling()
    { 
        foreach (Renderer r in spheres)
        {
            if(r==null) continue;
            if ((r.transform.position - player.position).sqrMagnitude > playerViewRange*playerViewRange)
                r.enabled = false;
            else
                r.enabled = true;
            
        }
    }
}


using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// 资源管理器
public class ResourceManagerNew : MonoBehaviour
{
    #region Singleton

    private static ResourceManagerNew instance;

    // 单例模式
    public static ResourceManagerNew Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType(typeof(ResourceManagerNew)) as ResourceManagerNew;
                if (instance == null)
                {
                    GameObject manager = new GameObject("ResourceManagerNew");
                    instance = manager.AddComponent<ResourceManagerNew>();
                }
            }
            return instance;
        }
    }

    #endregion

    // 资源路径
    public string resourcePath = "";

    // 资源表
    public Dictionary<string, Object> resourceTable = new Dictionary<string, Object>();

    // 对象池
    public Dictionary<string, Stack<GameObject>> objectPool = new Dictionary<string, Stack<GameObject>>();

    // 异步加载资源
    public void LoadResourceAsync(string name, System.Action<Object> OnLoaded)
    {
        StartCoroutine(LoadResourceAsyncCoroutine(name, OnLoaded));
    }

    // 同步加载资源
    public Object LoadResourceSync(string name)
    {
        Object resource = null;
        if(!resourceTable.TryGetValue(name, out resource))
        {
            resource = Resources.Load(resourcePath + name);
            if (resource != null)
            {
                resourceTable.Add(name, resource);
            }else{
                Debug.Log("找不到资源"+resourcePath + name);
            }
        }
        return resource;
    }

    // 获取对象池中的对象
    public GameObject GetPooledObject(string name, Vector3 position, Quaternion rotation)
    {
        Stack<GameObject> stack = null;
        GameObject obj = null;
        if (objectPool.TryGetValue(name, out stack) && stack.Count > 0)
        {
            obj = stack.Pop();
            obj.transform.position = position;
            obj.transform.rotation = rotation;
            obj.SetActive(true);
        }
        else
        {
            Object prefab = LoadResourceSync(name);
            if (prefab != null)
            {
                obj = Instantiate(prefab, position, rotation) as GameObject;
            }
        }
        return obj;
    }

    // 回收对象池中的对象
    public void RecyclePooledObject(string name, GameObject obj)
    {
        Stack<GameObject> stack = null;
        if (!objectPool.TryGetValue(name, out stack))
        {
            stack = new Stack<GameObject>();
            objectPool[name] = stack;
        }
        obj.SetActive(false);
        stack.Push(obj);
    }

    // 异步加载资源协程
    private IEnumerator LoadResourceAsyncCoroutine(string name, System.Action<Object> OnLoaded)
    {
        Object resource = null;
        if(!resourceTable.TryGetValue(name, out resource))
        {
            ResourceRequest request = Resources.LoadAsync(resourcePath + name);
            yield return new WaitUntil(() => request.isDone);
            if (request.asset != null)
            {
                resource = request.asset;
                resourceTable.Add(name, resource);
            }
        }
        OnLoaded?.Invoke(resource);
    }

    // 卸载资源
    public void UnloadResource(string name)
    {
        Object resource = null;
        if(resourceTable.TryGetValue(name, out resource))
        {
            Resources.UnloadAsset(resource);
            resourceTable.Remove(name);
            Debug.Log(name + " is destroyed!!!"); 
        }
        if (objectPool.ContainsKey(name))
        {
            objectPool[name].Clear();
        }
    }
}


//µ¥ÀýºÍ»¥³âËø£¬²Î¿¼:
//https://blog.csdn.net/f_957995490/article/details/108091022
using UnityEngine;

public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    private static T _inst;
    public static T Inst
    {
        get
        {
            if (_inst == null)
            {
                var go = new GameObject();
                go.name = typeof(T).ToString();
                _inst = go.AddComponent<T>();
            }

            return _inst;
        }
    }

    // protected virtual void Awake()
    // {
    // 	Instance = this as T;
    // }
}
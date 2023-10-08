public class SpecSingleton<T> where T : class, new()
{
    public SpecSingleton()
    {
    }

    private static T sInstance;
    // 用于lock块的对象
    private static readonly object sSynclock = new object();

    public static T Instance
    {
        get
        {
            if (sInstance == null)
            {
                lock (sSynclock)
                {
                    if (sInstance == null)
                    {
                        // 若T class具有私有构造函数,那么则无法使用SingletonProvider<T>来实例化new T();
                        sInstance = new T();
                    }
                }
            }
            return sInstance;
        }
        set { sInstance = value; }
    }
}
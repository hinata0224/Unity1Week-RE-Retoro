using UnityEngine;

/// <summary>
/// シングルトンクラス。継承されたクラスが存在しない場合は自動でオブジェクトを作成する
/// </summary>
public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    protected virtual bool dontDestroyOnLoad => true;

    private static T instance;
    public static T Instance
    {
        get
        {
            if (!instance)
            {
                CreateSingletonInstanceIfNeeded();
            }
            return instance;
        }
    }

    protected virtual void Awake()
    {
        if (instance == null)
        {
            instance = this as T;
            if (dontDestroyOnLoad)
            {
                DontDestroyOnLoad(gameObject);
            }
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public static void CreateSingletonInstanceIfNeeded()
    {
        if (instance == null)
        {
            instance = FindObjectOfType<T>();
            if (!instance)
            {
                GameObject singleton = new GameObject();
                instance = singleton.AddComponent<T>();
                singleton.name = typeof(T).ToString();

                if ((instance as SingletonMonoBehaviour<T>).dontDestroyOnLoad)
                {
                    DontDestroyOnLoad(singleton);
                }
            }
        }
    }
}

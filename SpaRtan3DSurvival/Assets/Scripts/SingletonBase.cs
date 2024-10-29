using UnityEngine;

public class SingletonBase<T> : MonoBehaviour where T : MonoBehaviour
{
    protected static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject findObj = GameObject.FindObjectOfType(typeof(T)) as GameObject;

                if (findObj == null)
                {
                    findObj = new GameObject(typeof(T).Name);
                    instance = findObj.AddComponent<T>();
                }

                else
                {
                    instance = findObj.GetComponent<T>();
                }
            }

            return instance;
        }
    }

    protected virtual void Awake()
    {
        if (instance == null)
        {
            instance = this as T;
        }

        else
        {
            if (instance != this)
                Destroy(gameObject);
        }
    }
}

public class ManagerBase<T> : SingletonBase<T> where T : MonoBehaviour
{
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(transform.root.gameObject);
    }
}
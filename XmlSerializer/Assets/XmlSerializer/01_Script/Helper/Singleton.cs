using UnityEngine;
using System.Collections;

public class SingletonBehaviour<T> : MonoBehaviour where T : MonoBehaviour {

	static Object _lock = new Object();

	static T _instance = null;

	public static T instance
	{
		get
		{
			lock(_lock)
			{
				if (_instance == null)
				{
					_instance = FindObjectOfType<T>();

					if (_instance == null)
					{
						GameObject singletonObj = new GameObject(typeof(T).Name);
						_instance = singletonObj.AddComponent<T>();
					}

					_instance.transform.position = Vector3.zero;
					_instance.transform.rotation = Quaternion.identity;

					DontDestroyOnLoad(_instance.gameObject);
				}

				return _instance;
			}
		}
	}
}

public class FindBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    static Object _lock = new Object();

    static T _instance = null;

    public static T instance
    {
        get
        {
            lock(_lock)
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<T>();
                }

                return _instance;
            }
        }
    }
}
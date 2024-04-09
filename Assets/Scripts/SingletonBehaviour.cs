using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonBehaviour<T> : MonoBehaviour
    where T : MonoBehaviour
{
    private static T instance;

    public static T Instance => instance;

    protected void Awake()
    {
        if (instance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
            return;
        }

        instance = GetComponent<T>();
        DontDestroyOnLoad(gameObject);
    }
}

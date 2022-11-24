using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Singleton pattern
/// </summary>
/// <typeparam Your script="T"></typeparam>
public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static T _Instance;

    protected virtual void Awake()
    {
        if (_Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        _Instance = GetComponent<T>();
    }

    public static T Instance => _Instance;

    private void OnDestroy()
    {
        if (_Instance.gameObject == this.gameObject)
        {
            _Instance = null;
        }
    }
}

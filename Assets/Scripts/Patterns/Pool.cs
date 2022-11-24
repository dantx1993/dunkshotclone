using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : Singleton<Pool>
{
    private Dictionary<PoolName, List<GameObject>> _ObjectPool = new Dictionary<PoolName, List<GameObject>>();

    public GameObject Instantiate(PoolName poolName, GameObject original, Vector3 position, Quaternion rotation, Transform parent = null)
    {
        GameObject result;
        if (!_ObjectPool.ContainsKey(poolName))
        {
            _ObjectPool.Add(poolName, new List<GameObject>());
        }
        List<GameObject> listGameObject = _ObjectPool[poolName];
        if (listGameObject.Count == 0)
        {
            result = Instantiate(original);
            result.SetActive(true);
            result.transform.rotation = Quaternion.identity;
        }
        else
        {
            result = listGameObject[0];
            listGameObject.RemoveAt(0);
        }
        result.transform.position = position;
        result.transform.rotation = rotation;
        result.transform.SetParent(parent);
        result.SetActive(true);
        return result;
    }
    public void Destroy(PoolName poolName, GameObject go)
    {
        if (!_ObjectPool.ContainsKey(poolName))
        {
            _ObjectPool.Add(poolName, new List<GameObject>());
        }
        List<GameObject> listGameObject = _ObjectPool[poolName];
        listGameObject.Add(go);
        listGameObject[listGameObject.Count - 1].SetActive(false);
        listGameObject[listGameObject.Count - 1].transform.position = transform.position;
    }
}

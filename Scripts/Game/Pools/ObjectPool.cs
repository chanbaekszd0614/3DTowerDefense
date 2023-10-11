using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
    private static ObjectPool instance;
    private Dictionary<string, Queue<GameObject>> objectPool = new Dictionary<string, Queue<GameObject>>();
    private GameObject pool;

    public static ObjectPool Instance
    {
        get{
            if (instance == null)
            {
                instance = new ObjectPool();
            }

            return instance;
        }
    }

    public GameObject GetObject(GameObject prefab)//创建物品
    {
        GameObject _object;
        if (pool == null)//当前场景无对象池（第一次进入游戏或切换场景）
        {
            pool = new GameObject("ObjectPool");
            objectPool = new Dictionary<string, Queue<GameObject>>();
        }

        if (!objectPool.ContainsKey(prefab.name) || objectPool[prefab.name].Count == 0)//如果池子里没有物品
        {
            _object = GameObject.Instantiate(prefab);
            PushObject(_object);
            
            GameObject childPool=GameObject.Find(prefab.name+"Pool");
            if (!childPool)
            {
                childPool = new GameObject(prefab.name + "Pool");
                childPool.transform.SetParent(pool.transform);
            }
            _object.transform.SetParent(childPool.transform);
        }
        //从队列中提取对象，返回
        _object = objectPool[prefab.name].Dequeue();
        _object.SetActive(true);
        return _object;
    }

    public void PushObject(GameObject prefab)//回收物品
    {
        //通过Instantiate实例化的物品都带有（Clone）后缀，去除在存储
        string _name = prefab.name.Replace("(Clone)", string.Empty);
        if (!objectPool.ContainsKey(_name))
        {
            objectPool.Add(_name,new Queue<GameObject>());
        }
        objectPool[_name].Enqueue(prefab);
        prefab.SetActive(false);
    }
}

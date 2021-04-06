using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public int id;

    public GameObject prefab;

    [SerializeField]
    private Queue<GameObject> avialableObjects = new Queue<GameObject>();


    void Awake()
    {
        GrowPool();
    }

    private void GrowPool()
    {
        for(int i = 0; i < 5; i++)
        {
            var instanceToAdd = Instantiate(prefab);
            instanceToAdd.transform.SetParent(transform);
            AddToPool(instanceToAdd);
        }
        Debug.Log($"Pool {id} has grown");
    }

    public void AddToPool(GameObject instance)
    {
        instance.SetActive(false);
        avialableObjects.Enqueue(instance);
    }

    public GameObject GetFromPool()
    {
        if (avialableObjects.Count == 0)
            GrowPool();

        var instance = avialableObjects.Dequeue();
        instance.SetActive(true);
        return instance;
    }
}

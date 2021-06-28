using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private int _id;
    [SerializeField] private GameObject _prefab;
    [SerializeField] private Queue<GameObject> _avialableObjects = new Queue<GameObject>();

    void Awake()
    {
        GrowPool();
    }

    private void GrowPool()
    {
        for(int i = 0; i < 5; i++)
        {
            var instanceToAdd = Instantiate(_prefab, transform.position, transform.rotation);
            instanceToAdd.transform.SetParent(transform);
            instanceToAdd.GetComponent<IPoolable>().SetPool(this);
            AddToPool(instanceToAdd);
        }
        Debug.Log($"Pool {_id} has grown");
    }

    public void AddToPool(GameObject instance)
    {
        instance.transform.SetParent(transform);
        instance.SetActive(false);
        _avialableObjects.Enqueue(instance);
    }

    public GameObject GetFromPool()
    {
        if (_avialableObjects.Count == 0)
            GrowPool();

        var instance = _avialableObjects.Dequeue();
        instance.SetActive(true);
        return instance;
    }
}

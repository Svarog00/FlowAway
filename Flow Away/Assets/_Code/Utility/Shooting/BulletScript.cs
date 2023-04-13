using System;
using UnityEngine;

//Player shot script
public class BulletScript : MonoBehaviour, IPoolable 
{
    private const string ReturnToPoolMethodName = "ReturnToPool";

    public GameObject shooter;
    private ObjectPool _objectPool;
    private int _damage = 0;

    public int Damage 
    {
        get => _damage;
        set => _damage = value;
    }

    // Start is called before the first frame update
    void Start()
    {
        Invoke(ReturnToPoolMethodName, 3f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamagable target;
        if (collision.TryGetComponent(out target) && collision.tag != "Player")
        {
            target.Hurt(_damage);
            //Destroy(gameObject);
            ReturnToPool();
        }
        else if (collision.tag == "Border" || collision.gameObject == shooter)
        {
            //Skip these objects
        }
        else
        {
            ReturnToPool();
        }
    }

    public void SetPool(ObjectPool pool)
    {
        _objectPool = pool;
    }

    public void ReturnToPool()
    {
        _objectPool.AddToPool(gameObject);
    }
}

using System;
using UnityEngine;

//Player shot script
public class BulletScript : MonoBehaviour, IPoolable 
{
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
        //Destroy(gameObject, 3);
        Invoke("ReturnToPool", 3f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<IDamagable>() != null && collision.tag != "Player")
        {
            collision.GetComponent<IDamagable>().Hurt(_damage);
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

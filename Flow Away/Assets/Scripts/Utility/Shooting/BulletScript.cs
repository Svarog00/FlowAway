using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour //Player shot script
{
    [SerializeField] private int damage = 0;
    public GameObject shooter;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 3);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<IDamagable>() != null && collision.tag != "Player")
        {
            collision.GetComponent<IDamagable>().Hurt(damage);
            Destroy(gameObject);
        }
        else if (collision.tag == "Border" || collision.gameObject == shooter)
        {
            //Skip these objects
        }
        else
        {
            Debug.Log("Bullet hit in" + collision.tag);
            Destroy(gameObject);
        }
    }
}

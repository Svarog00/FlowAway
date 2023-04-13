using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotScript : MonoBehaviour //Enemy shot script
{
    public Vector2 speed = new Vector2(0, 0);
    public GameObject shooter;

    [SerializeField] private int damage = 0;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 3);
    }

    private void Update()
    {
        Vector2 currentPosition = new Vector2(transform.position.x, transform.position.y);
        Vector2 newPosition = currentPosition + speed * Time.deltaTime;
        RaycastHit2D[] hits = Physics2D.LinecastAll(currentPosition, newPosition);

        IDamagable target;
        foreach (RaycastHit2D hit in hits)
        {
            if(hit.collider.TryGetComponent(out target) || hit.collider.gameObject == shooter)
            {
                 if(!hit.collider.CompareTag("Enemy"))
                 {
                    target.Hurt(damage);
                    Destroy(gameObject);
                }
            }
            if(hit.collider.tag == "Border" || hit.collider.tag == "Enemy")
            {
                continue;
            }
            else
            {
                Destroy(gameObject);
                break;
            }
        }

        transform.position = newPosition;
    }
}

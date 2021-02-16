using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    public GameObject shotPrefab;
    public Transform firePoint;
    public Transform player;

    public float shootDelay = 1f;

    [SerializeField]
    private float _shootCooldown;

    private Vector2 direction;

    private void Start()
    {
        _shootCooldown = 0f;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = player.transform.position;
    }

    private void FixedUpdate()
    {
        LookAtCursor();
    }

    public void Attack()
    {
        gameObject.SetActive(true);
        LookAtCursor();
        if (CanAttack())
        {
            //создание новго выстрела
            GameObject shotTransform = Instantiate(shotPrefab, firePoint.position, firePoint.rotation);
            //перемещение
            shotTransform.GetComponent<Rigidbody2D>().AddForce(firePoint.right * 5, ForceMode2D.Impulse);
            FindObjectOfType<AudioManager>().Play("Shot");

            _shootCooldown = shootDelay;
            StartCoroutine(ReloadWeapon());
        }

    }

    //Направление в соответствии с курсором
    private void LookAtCursor()
    {
        Vector3 mousePosition = Input.mousePosition; 
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);//get mouse position
        direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y); //get heading to the mouse position
        transform.right = direction;
    }

    private IEnumerator ReloadWeapon()
    {
        while(true)
        {
            _shootCooldown -= Time.deltaTime;
            if (_shootCooldown <= 0) 
            {
                //playSound
                gameObject.SetActive(false);
                yield break;
            }
            yield return null;
        }

    }

    private bool CanAttack()
    {
        return _shootCooldown <= 0f;
    }
}

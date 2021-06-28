using System.Collections;
using System;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    public GameObject shotPrefab;
    public Transform firePoint;
    public Transform player;
    public ObjectPool objectPool;
    
    [SerializeField] private int _damage;
    [SerializeField] private int _shotsCount;
    [SerializeField] private float _shootDelay = 0.75f;
    [SerializeField] private float _reloadTime = 2f;

    private int _curShotsCount;
    private float _curShootCooldown;
    private float _curReloadTime;
    private BulletScript _bulletInstance;

    [SerializeField] private Timer _cooldownTimer;
    [SerializeField] private Timer _reloadTimer;

    private void Start()
    {
        _curShotsCount = _shotsCount;
        _curReloadTime = 0f;
        _curShootCooldown = 0f;
        gameObject.SetActive(false);

        _cooldownTimer.Action = WeaponCooldowned;
        _reloadTimer.Action = WeaponReloaded;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = player.transform.position;
        LookAtCursor();
    }

    public void Attack()
    {
        gameObject.SetActive(true);
        LookAtCursor();
        if (CanAttack())
        {
            //создание новго выстрела
            //GameObject shotTransform = Instantiate(shotPrefab, firePoint.position, firePoint.rotation);
            GameObject shotTransform = objectPool.GetFromPool();
            //перемещение
            shotTransform.transform.position = firePoint.position;
            shotTransform.transform.rotation = firePoint.rotation;
            shotTransform.transform.parent = null;
            shotTransform.GetComponent<Rigidbody2D>().AddForce(firePoint.right * 5, ForceMode2D.Impulse);
            _bulletInstance = shotTransform.GetComponent<BulletScript>();
            _bulletInstance.shooter = player.gameObject;
            _bulletInstance.Damage = _damage;

            FindObjectOfType<AudioManager>().Play("Shot");
            _curShootCooldown = _shootDelay;
            _cooldownTimer.SetTime(_shootDelay);

            _curReloadTime = _reloadTime;
            _reloadTimer.SetTime(_reloadTime);


            _curShotsCount--;
            Debug.Log($"After shot {_curShotsCount}");
        }
    }

    //Направление в соответствии с курсором
    private void LookAtCursor()
    {
        Vector3 mousePosition = UtilitiesClass.GetWorldMousePosition(); 
        Vector2 aimDir = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(aimDir.y, aimDir.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, 0, angle);
        
        Vector3 localScale = Vector3.one;
        if(angle > 90 || angle < -90)
        {
            localScale.y = -1f;
        }
        else
        {
            localScale.y = 1f;
        }
        transform.localScale = localScale;
    }

    void WeaponCooldowned()
    {
        _curShootCooldown = 0f;
        gameObject.SetActive(false);
    }
    void WeaponReloaded()
    {
        FindObjectOfType<AudioManager>().Play("WeaponReloaded");
        _curShotsCount = _shotsCount;
    }

    private bool CanAttack() => _curShootCooldown <= 0f && _curShotsCount > 0;
}

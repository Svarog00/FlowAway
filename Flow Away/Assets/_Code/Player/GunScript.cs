using Assets.Scripts.Infrustructure;
using UnityEngine;

public class GunScript : MonoBehaviour, ICoroutineRunner
{
    public GameObject shotPrefab;
    public Transform firePoint;
    public Transform player;
    public ObjectPool objectPool;
    
    [SerializeField] private int BulletSpeed = 5;
    [SerializeField] private int _damage;
    [SerializeField] private int _shotsCount;
    [SerializeField] private float _shootDelay = 0.75f;
    [SerializeField] private float _reloadTime = 2f;

    private int _curShotsCount;
    private float _curShootCooldown;
    private BulletScript _bulletInstance;

    private Timer _cooldownTimer;
    private Timer _reloadTimer;

    private SpriteRenderer _sprite;

    private void Start()
    {
        _curShotsCount = _shotsCount;
        _curShootCooldown = 0f;
        //gameObject.SetActive(false);

        _sprite = GetComponent<SpriteRenderer>();
        _sprite.enabled = false;

        _cooldownTimer = new Timer(this, WeaponCooldowned);
        _reloadTimer = new Timer(this, WeaponReloaded);
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = player.transform.position;
        LookAtCursor();
    }

    public void Attack()
    {
        //gameObject.SetActive(true);
        _sprite.enabled = true;
        LookAtCursor();
        if (CanAttack())
        {
            //создание новго выстрела
            GameObject shotTransform = objectPool.GetFromPool();
            //перемещение
            shotTransform.transform.position = firePoint.position;
            shotTransform.transform.rotation = firePoint.rotation;
            shotTransform.transform.parent = null;
            shotTransform.GetComponent<Rigidbody2D>().AddForce(firePoint.right * BulletSpeed, ForceMode2D.Impulse);

            _bulletInstance = shotTransform.GetComponent<BulletScript>();
            _bulletInstance.shooter = player.gameObject;
            _bulletInstance.Damage = _damage;

            AudioManager.Instance.Play("Shot");
            _curShootCooldown = _shootDelay;

            _cooldownTimer.StartTimer(_shootDelay);

            _reloadTimer.StartTimer(_reloadTime);
            

            _curShotsCount--;
        }
    }

    //Направление в соответствии с курсором
    private void LookAtCursor()
    {
        Vector3 mousePosition = UtilitiesClass.GetWorldMousePosition(); 
        Vector2 aimDir = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(aimDir.y, aimDir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        
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
        _sprite.enabled = false;
    }

    void WeaponReloaded()
    {
        AudioManager.Instance.Play("WeaponReloaded");
        _curShotsCount = _shotsCount;
    }

    private bool CanAttack() => _curShootCooldown <= 0f && _curShotsCount > 0;
}

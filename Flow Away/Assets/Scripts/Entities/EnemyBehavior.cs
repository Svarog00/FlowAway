using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum EnemyStates { Patroling, Chasing, Attacking };

public class EnemyBehavior : MonoBehaviour
{
    [Header("Aggregation")]
    [SerializeField] private EnemyHealth _enemyHealth;
    [SerializeField] private EnemyMovement _enemyMovement;
    [SerializeField] private EnemyAttack _enemyAttack;

    [Header("Player relation")]
    public LayerMask layerMask;

    protected GameObject _player;
    protected Player_Health _playerHP;
    private bool _playerDetected;

    [SerializeField] private EnemyStates _currentState;
    [SerializeField] private int _weight = 0; //���������� ������, ������� ����� ���������� ����������� ��� ����� �� ������
    [SerializeField] private float _agressionDistance; //��������� �� ������� ���������� ���

    protected bool _canAttack;
    protected float _distanceToPlayer;
    protected Rigidbody2D _playerPosition;
    protected Rigidbody2D _enemyPosition;
    protected Vector2 _direction;
    protected Vector2 _vectorToPlayer;

    [Header("Patrol")]
    public Transform[] patrolSpots;

    private float _curWaitTime;
    private int _randomSpot = 0;
    [SerializeField] private float _waitTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        _currentState = EnemyStates.Patroling;
        _enemyPosition = GetComponent<Rigidbody2D>();
        _canAttack = false;
        _randomSpot = Random.Range(0, patrolSpots.Length);
    }

    // Update is called once per frame
    void Update()
    {
        //���� ������ ����� ���, �� ����������� ���������� �� ��������� ������
        //����� �� ����� ����������� ������ ������. ����� ������ ��������� - ���������� ����� ��������� �����
        if (_currentState == EnemyStates.Patroling)
        {
            Patrol();
        }
        else if (_currentState == EnemyStates.Chasing)
        {
            Chase();
        }
        else if(_currentState == EnemyStates.Attacking)
        {
            Engage();
        }
    }

    private void CalculateDistance()
    {
        _vectorToPlayer = transform.position - _player.transform.position; //������������ ������ � ������ / a vector to the player
        _distanceToPlayer = _vectorToPlayer.magnitude; //����� ������� / lenght of the vector
        _direction = _vectorToPlayer / _distanceToPlayer; //direction to player
    }

    #region Patroling
    private void ScanArea()
    {
        Collider2D[] detectedEnemies = Physics2D.OverlapCircleAll(transform.position, _agressionDistance, layerMask); //find the player in circle
        foreach (Collider2D enemy in detectedEnemies)
        {
            if (enemy.tag == "Player" && !_playerDetected) //���� ��������� ���, �� ��������� ������ �� ������
            {
                if (_player == null)
                {
                    _player = enemy.gameObject;
                    Invisibility playerInsibility = _player.GetComponent<Invisibility>();
                    playerInsibility.OnInsibilityEnable += PlayerInsibility_OnInsibilityEnable;
                    _playerHP = _player.GetComponent<Player_Health>();
                    _playerPosition = _player.GetComponent<Rigidbody2D>();
                }
                _currentState = EnemyStates.Chasing;
                _enemyMovement.CanMove = true;
                _playerDetected = true;
                break;
            }
        }
    }

    private void Patrol()
    {
        ScanArea();
        if (patrolSpots.Length > 0)
        {
            if (Vector2.Distance(transform.position, patrolSpots[_randomSpot].position) <= 0.6f)
            {
                _enemyMovement.CanMove = false;
                if (_curWaitTime <= 0f)
                {
                    _curWaitTime = _waitTime;
                    _randomSpot = Random.Range(0, patrolSpots.Length);
                    _enemyMovement.SetPoint(patrolSpots[_randomSpot].position);
                    _enemyMovement.CanMove = true;
                }
                else
                {
                    _curWaitTime -= Time.deltaTime;
                }
            }
            else
            {
                _enemyMovement.SetPoint(patrolSpots[_randomSpot].position);
                _enemyMovement.CanMove = true;
            }
        }
        else
        {
            _enemyMovement.CanMove = false;
        }
    }
    #endregion

    #region Chasing
    private void Chase()
    {
        CalculateDistance();
        //����������� �������� �� ������������
        _enemyMovement.SetDirection(_direction);
        _enemyMovement.CanMove = true;
        if (_distanceToPlayer > _agressionDistance)
        {
            _currentState = EnemyStates.Patroling;
            _playerDetected = false;
        }
        else if (_distanceToPlayer <= _enemyAttack.AttackDistance) //���� ����� ������� ������, �� ������������ ��� �����
        {
            _enemyMovement.CanMove = false;

            if (!_canAttack && _playerHP.FreeSlots > _weight) //���� ������ ������� �� ������� ����� �����������, �� ����� ���������
            {
                _canAttack = true;
                _playerHP.FreeSlots -= _weight;
                _currentState = EnemyStates.Attacking;
            }
        }
    }
    #endregion

    private void Engage()
    {
        CalculateDistance();
        if (_distanceToPlayer > _enemyAttack.AttackDistance)
        {
            _canAttack = false;
            _playerHP.RestoreSlots(_weight);
            _currentState = EnemyStates.Chasing;
        }
        else
        {
            _enemyAttack.VectorToPlayer = _vectorToPlayer;
            _enemyAttack.Attack();
        }
    }

    private void PlayerInsibility_OnInsibilityEnable(object sender, Invisibility.OnInvisibilityEnableEventArgs e)
    {
        if (e.isActive && _currentState != EnemyStates.Patroling)
        {
            _currentState = EnemyStates.Patroling;
            _playerDetected = false;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, _agressionDistance);
    }
}

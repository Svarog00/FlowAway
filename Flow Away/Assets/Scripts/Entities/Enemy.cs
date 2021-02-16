using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum EnemyStates { Patroling, Chasing };

public class Enemy : MonoBehaviour, IHealth
{
    [Header("Enemy base")]
    public Animator animator;
    [SerializeField] private EnemyStates currentState;

    [Header("Player relation")]
    public LayerMask layerMask;
    private bool playerDetected;
    private Transform center;
    protected Player_Health playerHP;
    protected GameObject Player;

    [SerializeField] private int weight = 0; //количество слотов, которые будут заниматься противником при атаке по игроку

    public float attackDistance; //Дистанция на которой может совершить атаку
    public float agressionDistance; //Дистанция на которой происходит агр
    public float chill = 0; //pause between attacks

    [SerializeField] private Rigidbody2D playerPosition;
    private Rigidbody2D enemyPosition;

    private bool _faceRight;
    [SerializeField] protected int damage;
    [SerializeField] protected float _chillTime = 1f;

    protected Vector2 direction;
    protected bool _canAttack;
    protected Vector2 _movement;
    protected Vector2 _headingToPlayer;
    protected float _distanceToPlayer;

    [Header("Stats")]
    [SerializeField] protected float maxSpeed;
    [SerializeField] protected int hp;
    protected float _currentSpeed;

    [Header("Patrol")]
    public Transform[] patrolSpots;
    private int randomSpot;
    [SerializeField] private float waitTime = 0f;
    private float curWaitTime;

    // Start is called before the first frame update
    void Start()
    {
        currentState = EnemyStates.Patroling;
        enemyPosition = GetComponent<Rigidbody2D>();
        center = GetComponent<Transform>();
        _canAttack = false;
        randomSpot = Random.Range(0, patrolSpots.Length);
    }

    // Update is called once per frame
    protected void Update()
    {
        //Если игрока рядом нет, то патрулирует территорию по рандомным точкам
        //Дойдя до точки запускается таймер отдиха. Когда таймер кончается - выбирается новая рандомная точка
        if (currentState == EnemyStates.Patroling)
        {
            if(patrolSpots.Length > 0)
            {
                _currentSpeed = maxSpeed; 
                transform.position = Vector2.MoveTowards(transform.position, patrolSpots[randomSpot].position, _currentSpeed * Time.deltaTime); 
                if(Vector2.Distance(transform.position, patrolSpots[randomSpot].position) <= 0.2f) 
                {
                    _currentSpeed = 0;
                    if (curWaitTime <= 0f) 
                    {
                        curWaitTime = waitTime;
                        randomSpot = Random.Range(0, patrolSpots.Length);
                        _currentSpeed = maxSpeed;
                    }
                    else curWaitTime -= Time.deltaTime;
                }
            }

            Collider2D[] detectedEnemies = Physics2D.OverlapCircleAll(center.position, agressionDistance, layerMask); //find the player in circle
            foreach (Collider2D enemy in detectedEnemies)
            {
                if (enemy.tag == "Player" && !playerDetected) //если произошел агр, то заполняем ссылки на игрока
                {
                    if(Player == null)
                    {
                        Player = enemy.gameObject;
                        playerHP = Player.GetComponent<Player_Health>();
                        playerPosition = Player.GetComponent<Rigidbody2D>();
                    }
                    currentState = EnemyStates.Chasing;
                    playerDetected = true;
                    break;
                }
            }
        }

        if (playerDetected)
        {
            EstimateDistance();//Рассчет дистанции для дальнейших действий
            if (currentState == EnemyStates.Chasing)
            {
                //Поворот спрайта в зависимости от положения игрока
                if (playerPosition.position.x < gameObject.transform.position.x && _faceRight == true)
                {
                    Flip();
                }
                if (playerPosition.transform.position.x > gameObject.transform.position.x && _faceRight == false)
                {
                    Flip();
                }
                Chase(); //Go to the enemy if he is too close
                //конец поведение врага по отношению к игроку

                if (_distanceToPlayer > agressionDistance)
                {
                    currentState = EnemyStates.Patroling;
                    playerDetected = false;
                }
            }
        }
        animator.SetFloat("Speed", _currentSpeed); //В зависимости от скорости активировать анимации
    }

    protected virtual void FixedUpdate()
    {
        enemyPosition.MovePosition(enemyPosition.position - direction * _currentSpeed * Time.deltaTime); //movement
    }

    void EstimateDistance()
    {
        _headingToPlayer = enemyPosition.position - playerPosition.position; //направленный вектор к игроку / a vector to the player
        _distanceToPlayer = _headingToPlayer.magnitude; //длина вектора / lenght of the vector
        direction = _headingToPlayer / _distanceToPlayer; //direction to player
    }

    protected void Chase()
    {
        _currentSpeed = maxSpeed; //Увеличиваем скорость до максимальной
        if (_distanceToPlayer <= attackDistance) //Если игрок слишком близко, то остановиться для атаки
        {
            _currentSpeed = 0f;

            if (!_canAttack && playerHP.FreeSlots > weight) //Если игрока атакует не слишком много противников, то можно атаковать
            {
                _canAttack = true;
                playerHP.FreeSlots -= weight;
            }
            if (chill <= 0 && _canAttack) //Если является атакующим и паузка кончилась, то атака
            {
                Attack();
            }
        }
        else
        {
            Leave();
        }
    }

    protected virtual void Attack() { }

    protected void Leave()
    {
        if (_canAttack)
        {
            _canAttack = false;
            playerHP.RestoreSlots(weight);
        }
    }

    public virtual void Hurt(int damage) //get damage from player or another entity
    {
        hp -= damage;
        if(hp <= 0)
        {
            _canAttack = false;
            Destroy(gameObject, 2f);
            //return to pool
        }
    }

    public void Heal() { }

    protected void Flip() //turn left or right depends on player position
    {
        _faceRight = !_faceRight;
        Vector3 Scaler = gameObject.transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

    protected IEnumerator Cooldown()
    {
        while (true)
        {
            chill -= Time.deltaTime;
            if (chill <= 0)
            {
                yield break;
            }
            yield return null;
        }
    }
}

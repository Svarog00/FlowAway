using Assets.Scripts.BehaviourStates;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum EnemyStates { Patroling, Chasing, Attacking };

public class EnemyBehavior : AgentBehaviour
{
	[Header("Components")]
	[SerializeField] private EnemyHealth _enemyHealth;
	[SerializeField] private EnemyMovement _enemyMovement;
	[SerializeField] private EnemyAttack _enemyAttack;

	[Header("States")]
	private AIBehaviourState currentState;

	[Header("Player relation")]
	public LayerMask layerMask;

	[SerializeField] private EnemyStates _currentState;
	[SerializeField] private int _weight = 0; //���������� ������, ������� ����� ���������� ����������� ��� ����� �� ������
	[SerializeField] private float _agressionDistance; //��������� �� ������� ���������� ���
	[SerializeField] private float _chaseTimeOut = 0.5f; //����� ������

	protected GameObject player;
	protected PlayerHealthController playerHP;
	private bool _playerDetected;

	protected bool canAttack = false;
	protected float distanceToPlayer;
	protected Rigidbody2D rb2;
	protected Vector2 direction;
	protected Vector2 vectorToPlayer;

	[Header("Patrol")]
	public Transform[] patrolSpots;

	private float _curWaitTime;
	private int _randomSpot = 0;
	[SerializeField] private float _waitTime = 0f;

	// Start is called before the first frame update
	void Awake()
	{
		rb2 = GetComponent<Rigidbody2D>();
	}

	private void Start()
	{
		StartCoroutine(PatrolState());
	}

	// Update is called once per frame
	void Update()
	{
		//���� ������ ����� ���, �� ����������� ���������� �� ��������� ������
		//����� �� ����� ����������� ������ ������. ����� ������ ��������� - ���������� ����� ��������� �����
	   if(_playerDetected)
       {
			CalculateDistance();
       }

		//currentState.Handle();
	}

	private void SetState(AIBehaviourState state)
    {
		currentState = Instantiate(state);

    }

	IEnumerator FSMCoroutine()
    {
		yield return null;
    }

	private void CalculateDistance()
	{
		vectorToPlayer = transform.position - player.transform.position; //������������ ������ � ������ / a vector to the player
		distanceToPlayer = vectorToPlayer.magnitude; //����� ������� / lenght of the vector
		direction = vectorToPlayer / distanceToPlayer; //direction to player
	}

	private void DetectTargets()
	{
		Collider2D[] detectedEnemies = Physics2D.OverlapCircleAll(transform.position, _agressionDistance, layerMask); //find the player in circle
		foreach (Collider2D enemy in detectedEnemies)
		{
			if (enemy.tag == "Player" && !_playerDetected) //���� ��������� ���, �� ��������� ������ �� ������
			{
				if (player == null)
				{
					player = enemy.gameObject;
					Invisibility playerInsibility = player.GetComponent<Invisibility>();
					playerInsibility.OnInsibilityEnable += PlayerInsibility_OnInsibilityEnable;
					playerHP = player.GetComponent<PlayerHealthController>();
				}
				_playerDetected = true;
				break;
			}
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

	private void OnDrawGizmosSelected()
	{
		Gizmos.DrawWireSphere(transform.position, _agressionDistance);
	}

	IEnumerator PatrolState()
	{
		_currentState = EnemyStates.Patroling;

		while (_currentState == EnemyStates.Patroling)
		{
			DetectTargets();

			if(_playerDetected)
			{
				StartCoroutine(ChaseState());
				yield break;
			}

			if (patrolSpots.Length > 0)
			{
				_randomSpot = Random.Range(0, patrolSpots.Length);
				if (Vector2.Distance(transform.position, patrolSpots[_randomSpot].position) <= 0.6f)
				{
					_enemyMovement.CanMove = false;
					if (_curWaitTime <= 0f)
					{
						_curWaitTime = _waitTime;
						_randomSpot = Random.Range(0, patrolSpots.Length);
						_enemyMovement.SetTargetPosition(patrolSpots[_randomSpot].position);
						_enemyMovement.CanMove = true;
					}
					else
					{
						_curWaitTime -= Time.deltaTime;
					}
				}
				else
				{
					_enemyMovement.SetTargetPosition(patrolSpots[_randomSpot].position);
					_enemyMovement.CanMove = true;
				}
			}
			else
			{
				_enemyMovement.CanMove = false;
			}

			yield return null;
		}
	}

	IEnumerator ChaseState()
	{
		_currentState = EnemyStates.Chasing;
		Vector3 oldTargetPosition = player.transform.position;
		_enemyMovement.SetTargetPosition(oldTargetPosition);
		float elapsedTime = 0f;
		while (_currentState == EnemyStates.Chasing)
		{
			if(oldTargetPosition != player.transform.position)
            {
				oldTargetPosition = player.transform.position;
				_enemyMovement.SetTargetPosition(oldTargetPosition);
			}

			_enemyMovement.CanMove = true;

			if (distanceToPlayer > _agressionDistance)
			{
				elapsedTime += Time.deltaTime;

				if(elapsedTime >= _chaseTimeOut)
                {
					_currentState = EnemyStates.Patroling;
					_playerDetected = false;
					StartCoroutine(PatrolState());
					yield break;
				}
			}
			else if (distanceToPlayer <= _enemyAttack.AttackDistance) //���� ����� ������� ������, �� ������������ ��� �����
			{
				elapsedTime = 0f;
				_enemyMovement.CanMove = false;
				if (!canAttack && playerHP.GetFreeSlots() > _weight) //���� ������ ������� �� ������� ����� �����������, �� ����� ���������
				{
					StartCoroutine(EngageState());
					yield break;
				}
			}
			else
            {
				elapsedTime = 0f;
			}

			yield return null;
		}
	}

	IEnumerator EngageState()
	{
		_currentState = EnemyStates.Attacking;
		canAttack = true;
		playerHP.DecreaseSlots(_weight);
		while (_currentState == EnemyStates.Attacking)
		{
			if (distanceToPlayer > _enemyAttack.AttackDistance)
			{
				canAttack = false;
				playerHP.RestoreSlots(_weight);
				StartCoroutine(ChaseState());
				yield break;
			}
			else
			{
				_enemyAttack.VectorToPlayer = vectorToPlayer;
				_enemyAttack.Attack();
			}

			yield return null;
		}
	}
}

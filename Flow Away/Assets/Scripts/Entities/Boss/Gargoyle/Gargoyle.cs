using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gargoyle : Boss
{


	/*public Transform attackPoint;
	public float meleeRange;*/

	// public GameObject shotPrefab;
	// public Transform firePoint;


	/* 
	 * Air attack should be consists of two stages: 
	 * - chase Player
	 * - if Gargo reach Player zone (some units around Player = Sphere(player`s center, airAttackZone) she attack Player, then use Timer for attack delay
	 * - repeat (while hp >= 50%)
	*/
	private enum Phases { waiting_phase, first_phase, second_phase };
	[Header("Gargoyle base")]
	public Animator _animator;
	public GameObject shotPrefab;
	public Transform firePoint;

	private Phases _currentPhase;
    
	[Header("Player relation")]
	public LayerMask playerLayer;
	
	private float _distanceToPlayer;
    private Vector2 _directionToPlayer;
	private Rigidbody2D _playerPosition;
	private Rigidbody2D _gargoyleCenter;
	private bool playerDetected;

	public Transform Player;

	[Header("Stats")]

	public float agressionDistance;
    public float spittleRange;
	public float airAttackRadius;
	public float maxSpeed;
	private float _currentSpeed;

    private void Awake()
    {
		healthPoints = 300;
		_currentState = "Gargoyle_Idle";
		_currentPhase = Phases.waiting_phase;
		_animator = GetComponentInChildren<Animator>();
	}

    private void Start()
	{
		_colliderTrigger = FindObjectOfType<ColliderTrigger>();
		_colliderTrigger.OnPlayerEnterTrigger += ColliderTriger_OnPlayerEnterTrigger;
		_gargoyleCenter = GetComponent<Rigidbody2D>();
		playerDetected = false;
	}

    private void Update()
	{
		switch(_currentPhase)
		{
			case Phases.first_phase:
			{

					//Find Player on the scene
					Collider2D[] detectedEnemies = Physics2D.OverlapCircleAll(_gargoyleCenter.transform.position, agressionDistance, playerLayer); //find the player in circle
					foreach (Collider2D enemy in detectedEnemies)
					{
						if (enemy.tag == "Player" && !playerDetected) 
						{
							if (_player == null)
							{
								_player = enemy.gameObject;
								_playerHealth = _player.GetComponent<Player_Health>();
								_playerPosition = _player.GetComponent<Rigidbody2D>();
							}
							playerDetected = true;
							break;
						}
					}

					//Chase function
					if (playerDetected)
					{
						EstimateDistance();
						Chase();
						//Air attack function
						//Timer interface
						//Animation state
						//Health check (if hp < 50% -> nextStage funck)
					}
					break;
			}

			case Phases.second_phase:
			{

				//Chase function
				//Animation state
				//Air attack func
				//Timer between air & spittle
				//Spittle
				//timer
				//Heatlh check
				break;
			}
		}

		if(chill > 0)
        {
			chill -= Time.deltaTime;
        }
	}
	private void FixedUpdate()
	{
		if (playerDetected && chill <= 0)
		{
			_gargoyleCenter.MovePosition(_gargoyleCenter.position - _directionToPlayer * _currentSpeed * Time.deltaTime); //movement
		}
	}

	private void Chase()
	{
		_currentSpeed = maxSpeed; //Увеличиваем скорость до максимальной
		if (airAttackRadius >= _distanceToPlayer) //Если игрок слишком близко, то остановиться для атаки
		{
			_currentSpeed = 0f;
			if (chill <= 0) //Если является атакующим и паузка кончилась, то атака
			{
				SlamAttack();
			}
		}
	}

	void EstimateDistance()
	{
		Vector2 _headingToPlayer = (Vector2)gameObject.transform.position - _playerPosition.position; //направленный вектор к игроку / a vector to the player
		_distanceToPlayer = _headingToPlayer.magnitude; //длина вектора / lenght of the vector
		_directionToPlayer = _headingToPlayer / _distanceToPlayer; //direction to player
	}

	private void SlamAttack()
	{
		ChangeAnimationState("Gargoyle_FallingDown"); //Change state to animate
		PlayAnimation(); //Play certain animation
		Invoke("DealDamage", _animator.GetCurrentAnimatorStateInfo(0).length); //Invoke certain function after animation has ended
		ChangeAnimationState("Gargoyle_FlyIdle");
		Invoke("PlayAnimation", _animator.GetCurrentAnimatorStateInfo(0).length);
	}

	private void DealDamage()
	{
		Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(gameObject.transform.position, airAttackRadius, playerLayer); //find the player in circle
		//damage them
		foreach (Collider2D enemy in hitEnemies)
		{
			if (enemy.tag == "Player" && chill <= 0)
			{
				_playerHealth.Hurt(damage);
				break;
			}
		}
		playerDetected = false;
		chill = _chillTime; //Pause between attacks
	}


	private void SpittleAttack()
    {
		ChangeAnimationState("Gargoyle_Spite");
		PlayAnimation();
		//создание новго выстрела
		GameObject shotTransform = Instantiate(shotPrefab, firePoint.position, firePoint.rotation.normalized);
		//перемещение
		shotTransform.GetComponent<ShotScript>().speed = new Vector2(5, 5) * -_directionToPlayer;
		shotTransform.GetComponent<ShotScript>().shooter = gameObject;
		FindObjectOfType<AudioManager>().Play("Shot");
		chill = _chillTime;
	}

	private void NextPhase(Phases phase)
	{
		switch(phase)
		{
			case Phases.waiting_phase:
			{
				_currentPhase = Phases.first_phase;
				break;
			}

			case Phases.first_phase:
			{
				_currentPhase = Phases.second_phase;
				break;
			}

			case Phases.second_phase:
			{
				//Die
				break;
			}

		}
	}

	protected override void ColliderTriger_OnPlayerEnterTrigger(object sender, EventArgs e)
	{
		//Show UI
		Debug.Log("BEGIN BOSSFIGHT...");
		NextPhase(_currentPhase);
		ChangeAnimationState("Gargoyle_TakingOff");
		PlayAnimation();
		ChangeAnimationState("Gargoyle_FlyIdle");
		Invoke("PlayAnimation", _animator.GetCurrentAnimatorStateInfo(0).length + 0.2f);

		_colliderTrigger.OnPlayerEnterTrigger -= ColliderTriger_OnPlayerEnterTrigger;
	}

	private void PlayAnimation()
	{
		_animator.Play(_currentState);
	}

	void OnDrawGizmosSelected()
	{
		if(Player.position == null)
        {
			return;
        }
		Gizmos.DrawWireSphere(Player.position, airAttackRadius);
	}
}

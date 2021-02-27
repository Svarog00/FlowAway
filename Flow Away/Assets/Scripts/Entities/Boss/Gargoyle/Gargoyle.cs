using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gargoyle : Boss
{
	/* 
	 * Air attack should be consists of two stages: 
	 * - chase Player
	 * - if Gargo reach Player zone (some units around Player = Sphere(player`s center, airAttackZone) she attack Player, then use Timer for attack delay
	 * - repeat (while hp >= 50%)
	*/
	private enum Phases { waiting_phase, first_phase, second_phase, dying_phase };
	[Header("Gargoyle base")]
	public Animator _animator;
	public GameObject shotPrefab;
	public Transform firePoint;
	public Sprite[] phaseSprites;
	private CapsuleCollider2D _body;
	private Vector2 airBodyPos = new Vector2(0f, 0.75f);
	private Vector2 normalBodyPos = new Vector2(0f, 0.18f);

	[Header("Player relation")]
	public LayerMask playerLayer;
	
	private Phases _currentPhase;
	private float _distanceToPlayer;
    private Vector2 _directionToPlayer;
	private Rigidbody2D _playerPosition;
	private Rigidbody2D _gargoyleCenter;
	private bool playerDetected;
	private bool isShooting;
	public Transform Player;

	[Header("Stats")]

	public float agressionDistance;
	public float airAttackRadius;
	public float maxSpeed;
	public float cooldownSpittle;
	private float _currentSpeed;
	
    private void Awake()
    {
		_healthPoints = healthPointMax;
		_currentState = "Gargoyle_Idle";
		_currentPhase = Phases.waiting_phase;
		_animator = GetComponentInChildren<Animator>();
		_body = GetComponent<CapsuleCollider2D>();
	}

    private void Start()
	{
		isShooting = false;
		_colliderTrigger = FindObjectOfType<ColliderTrigger>();
		_colliderTrigger.OnPlayerEnterTrigger += ColliderTriger_OnPlayerEnterTrigger;
		base.OnPhaseIconChange(phaseSprites[0]);
		_gargoyleCenter = GetComponent<Rigidbody2D>();
		playerDetected = false;
	}

    private void Update()
	{ 	  
		switch (_currentPhase)
		{
			case Phases.first_phase:
			{ 
				if (_healthPoints <= 0.5f * healthPointMax)//если хп меньше половины -> следующая фаза и изменение фазы на интерфейсе
				{
					NextPhase(_currentPhase);
					base.OnPhaseIconChange(phaseSprites[1]);
				}
			//Find Player on the scene
				FindPlayerAtScene();
				if (playerDetected)
				{		
					EstimateDistance();
					//Chase function
				    Chase();
				}
				break;
			}

			case Phases.second_phase:
			{
				if (_healthPoints <= 0f) //проверка здоровья
				{
					NextPhase(_currentPhase);
					base.OnActivatedUI(false);
				}
				//Find Player on the scene
				FindPlayerAtScene();
				//Chase function
				if (playerDetected)
				{
					EstimateDistance();
					Chase();
					if (isShooting == true)//если произошел AirAttack, то идет вызов функции, которая отвечает за плевки
					{
						Invoke("SpittleAttack", _animator.GetCurrentAnimatorStateInfo(0).length);
						_body.offset = normalBodyPos;//изменение положения колайдера гаргульи ( 0,75 - Air, 0.18 - Earth)
					}
				}
				break;
			}

			case Phases.dying_phase:
            {
				ChangeAnimationState("Gargoyle_Dying");
				PlayAnimation();
				Invoke("SetActive", _animator.GetCurrentAnimatorStateInfo(0).length);
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
		if (playerDetected && chill <= 0 && _healthPoints > 0)
		{
			_gargoyleCenter.MovePosition(_gargoyleCenter.position - _directionToPlayer * _currentSpeed * Time.deltaTime); //movement
		}
	}

	private void FindPlayerAtScene()
    {
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
	}


	private void Chase()
	{
		if(_currentPhase == Phases.second_phase)
        {
			_currentSpeed = maxSpeed * 1.2f;
		}
		else
        {
			_currentSpeed = maxSpeed; //Увеличиваем скорость до максимальной
        }

		if (airAttackRadius >= _distanceToPlayer) //Если игрок слишком близко, то остановиться для атаки
		{
			_currentSpeed = 0f;
			if (chill <= 0) //Если является атакующим и паузка кончилась, то атака
			{
				//Air attack function
				SlamAttack();
				_body.offset = airBodyPos;
				if (_currentPhase == Phases.second_phase && chill <= 0)//изменение флага, чтобы гаргулья начала плеваться ГОВНИЩЕМ(моим кодом)
                {
					isShooting = true;
                }
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
		_body.offset = normalBodyPos;
		Invoke("DealDamage", _animator.GetCurrentAnimatorStateInfo(0).length); //Invoke certain function after animation has ended
		ChangeAnimationState("Gargoyle_FlyIdle");
		Invoke("PlayAnimation", _animator.GetCurrentAnimatorStateInfo(0).length);
		_body.offset = airBodyPos;
	}

	private void DealDamage()
	{
		Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(gameObject.transform.position, airAttackRadius, playerLayer); //find the player in circle
		//damage them
		foreach (Collider2D enemy in hitEnemies)
		{
			if (enemy.tag == "Player" && chill <= 0)
			{
				_playerHealth.Hurt(_damage);
				break;
			}
		}
		playerDetected = false;
		chill = _chillTime; //Pause between attacks
	}

	private void SpittleAttack()
    {
		if (chill <= 0)
		{
			ChangeAnimationState("Gargoyle_Spite");
			PlayAnimation();
			//создание новго выстрела
			GameObject shotTransform = Instantiate(shotPrefab, firePoint.position, firePoint.rotation.normalized);
			//перемещение
			shotTransform.GetComponent<ShotScript>().speed = new Vector2(5, 5) * -_directionToPlayer;
			shotTransform.GetComponent<ShotScript>().shooter = gameObject;
			FindObjectOfType<AudioManager>().Play("Shot");
			chill = cooldownSpittle;
			isShooting = false;
		}
	}

	private void SetActive()
    {
		gameObject.SetActive(false);
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
				_currentPhase = Phases.dying_phase;
				break;
			}

		}
	}
	
	protected override void ColliderTriger_OnPlayerEnterTrigger(object sender, EventArgs e)
	{
		//Show UI
		base.OnActivatedUI(true);
		Debug.Log("BEGIN BOSSFIGHT...");
		NextPhase(_currentPhase);
		ChangeAnimationState("Gargoyle_TakingOff");
		PlayAnimation();
		ChangeAnimationState("Gargoyle_FlyIdle");
		Invoke("PlayAnimation", _animator.GetCurrentAnimatorStateInfo(0).length + cooldownSpittle);
		_body.offset = airBodyPos;
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

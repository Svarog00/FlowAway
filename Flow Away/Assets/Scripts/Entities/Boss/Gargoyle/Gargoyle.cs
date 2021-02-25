using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gargoyle : Boss
{
	/*public Transform attackPoint;
	public float meleeRange;*/

	 


	/* 
	 * Air attack (Phase 1) should be consists of two stages: 
	 * - chase Player
	 * - if Gargo reach Player zone she attack Player, then use Timer for attack delay
	 * - repeat (while hp >= 50%)
	*/

/*
 * Air attack and Spittle (Phase 2):
 * - chase Player (speed = max * 0.3f)
 * - air attack
 * - when Gargoyle wake up, she start spiitle Player
 * - repeat (while hp > 0)
 */


	private enum Phases { waiting_phase, first_phase, second_phase };
	[Header("Gargoyle base")]

	private Phases _currentPhase;
	[SerializeField] private Animator _animator;
	[SerializeField]private CapsuleCollider2D _capsuleCollider2D;
	public GameObject shotPrefab;
	public Transform firePoint;
	public Image phaseImage;
	public Sprite[] phaseSprites;

	[Header("Player relation")]
	public LayerMask playerLayer;
	
	private float _distanceToPlayer;
    private Vector2 _directionToPlayer;
	private Rigidbody2D _playerPosition;
	private Rigidbody2D _gargoyleCenter;
	private bool playerDetected;

	public Transform Player;

	[Header("Stats")]

	[SerializeField] private float _shotCooldown = 0;
	[SerializeField] private float _curCooldown;
	private float _currentSpeed;
	public float agressionDistance;
    //public float spittleRange;
	public float airAttackRadius;
	public float maxSpeed;
	

	private void Awake()
    {
		_healthPoints = 300;
		_healthPointMax = _healthPoints;
		_currentState = "Gargoyle_Idle";
		_currentPhase = Phases.waiting_phase;
		_animator = GetComponentInChildren<Animator>();
		_capsuleCollider2D = GetComponent<CapsuleCollider2D>();
		_curCooldown = 0;
	}

    private void Start()
	{
		_colliderTrigger = FindObjectOfType<ColliderTrigger>();
		_colliderTrigger.OnPlayerEnterTrigger += ColliderTriger_OnPlayerEnterTrigger;
		phaseImage.sprite = phaseSprites[0];
		_gargoyleCenter = GetComponent<Rigidbody2D>();
		playerDetected = false;
	}

    private void Update()
	{
		
		switch (_currentPhase)
		{
			case Phases.waiting_phase:
            {
		    	break;
            }


			case Phases.first_phase:
			{

					//Find Player on the scene (default)
					FindPlayer();
					//Chase function
					if (playerDetected)
					{
						EstimateDistance();
						Chase();
						//Air attack function
						//Timer interface
						//Animation state
						//Health check (if hp < 50% -> nextStage funck)
						if(_healthPoints <= 0.5 * _healthPointMax)
                        {
							NextPhase(_currentPhase);

							//Change Gargoyle`s icon UI
							Debug.Log("21312");
							phaseImage.sprite = phaseSprites[1];
							//phaseImage2.enabled = true;
                        }
					}
					break;
			}

			case Phases.second_phase:
			{
				//Chase function
				if(playerDetected)
                {
					//EstimateDistance();
					//Chase();
					//Spittle();
				}
				//Animation state
				//Air attack func
				//Timer between air & spittle
				//Spittle
				//timer
				//Heatlh check
				break;
			}


            default:
                {
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
				Attack();
			}
		}
	}

	void EstimateDistance()
	{
		Vector2 _headingToPlayer = (Vector2)gameObject.transform.position - _playerPosition.position; //направленный вектор к игроку / a vector to the player
		_distanceToPlayer = _headingToPlayer.magnitude; //длина вектора / lenght of the vector
		_directionToPlayer = _headingToPlayer / _distanceToPlayer; //direction to player
	}

	private void Attack()
	{
		ChangeAnimationState("Gargoyle_FallingDown");
		
		PlayAnimation();
		Invoke("DealDamage", _animator.GetCurrentAnimatorStateInfo(0).length);
		_capsuleCollider2D.offset = new Vector2(0f, 0.18f);
		ChangeAnimationState("Gargoyle_FlyIdle");
		Invoke("PlayAnimation", _animator.GetCurrentAnimatorStateInfo(0).length);
		_capsuleCollider2D.offset = new Vector2(0f, 0.75f) ;
		
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

	
	private void FindPlayer()
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
				//currentState = EnemyStates.Chasing;
				playerDetected = true;
				break;
			}
		}
	}

	private void Spittle()
    {
		ChangeAnimationState("Gargoyle_Spite");
		if (_curCooldown <= 0f)
		{
			GameObject shot = Instantiate(shotPrefab, firePoint.position, firePoint.rotation.normalized);
			shot.GetComponent<ShotScript>().speed = new Vector2(5, 5) * - _directionToPlayer;
			_curCooldown = _shotCooldown;
		}
	}



	private void NextPhase(Phases currentPhase)
	{
		switch(currentPhase)
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
		  //0.18 - def , 0.75 -fly
		//StartCoroutine(Timer(10.5f));

		ChangeAnimationState("Gargoyle_FlyIdle");
		Invoke("PlayAnimation", _animator.GetCurrentAnimatorStateInfo(0).length + 0.2f);
		_capsuleCollider2D.offset = new Vector2(0f, 0.75f);
		



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

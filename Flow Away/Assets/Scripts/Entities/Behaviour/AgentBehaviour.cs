using Assets.Scripts.BehaviourStates;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AgentBehaviour : MonoBehaviour
{
    protected AIStateMachine StateMachine;

    [SerializeField] private float _waitTime = 1f;
    [SerializeField] private int _weight = 0; //количество слотов, которые будут заниматься противником при атаке по игроку
	[SerializeField] private float _agressionDistance; //Дистанция на которой происходит агр
	[SerializeField] private float _chaseTimeOut = 0.5f; //Время погони

    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private Transform[] _patrolSpots;

    private GameObject _player;

    private bool _enemyDetected;

    private float _distanceToPlayer;
    private Vector2 _direction;
    private Vector2 _vectorToPlayer;

    public float AgressionDistance 
    { 
        get => _agressionDistance; 
    }

    public float ChaseTimeOut 
    { 
        get => _chaseTimeOut; 
    }

    public int Weight 
    { 
        get => _weight; 
    }

    public GameObject Player 
    { 
        get => _player;
        set
        {
            _player = value;
            Invisibility playerInsibility = _player.GetComponent<Invisibility>();
            playerInsibility.OnInsibilityEnable += PlayerInsibility_OnInsibilityEnable;
        }
    }

    public float DistanceToPlayer { get => _distanceToPlayer; }
    public Vector2 Direction { get => _direction; }
    public Vector2 VectorToPlayer { get => _vectorToPlayer; }
    public bool EnemyDetected { get => _enemyDetected; set => _enemyDetected = value; }
    public Transform[] PatrolSpots { get => _patrolSpots; }
    public float WaitTime { get => _waitTime; }
    public LayerMask LayerMask { get => _layerMask; }

    protected void Init()
    {
        StateMachine = new AIStateMachine();
    }

    private void Update()
    {
        StateMachine.Work();

        if (_enemyDetected)
        {
            CalculateDistance();
        }
    }

    private void PlayerInsibility_OnInsibilityEnable(object sender, Invisibility.OnInvisibilityEnableEventArgs e)
    {
        if (e.isActive && StateMachine.CurrentState.GetType() != typeof(PatrolState))
        {
            StateMachine.Enter<PatrolState>();
        }
    }

    private void CalculateDistance()
    {
        _vectorToPlayer = transform.position - _player.transform.position; //направленный вектор к игроку / a vector to the player
        _distanceToPlayer = _vectorToPlayer.magnitude; //длина вектора / lenght of the vector
        _direction = _vectorToPlayer / _distanceToPlayer; //direction to player
    }
}
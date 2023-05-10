using Assets.Scripts.BehaviourStates;
using UnityEngine;

public abstract class AgentBehaviour : MonoBehaviour
{
    public bool EnemyDetected { get; set; }

    public int Weight => _weight;

    public GameObject Player 
    { 
        get => _player;
        set
        {
            if(_player == null)
            {
                _player = value;
                Invisibility playerInsibility = _player.GetComponent<Invisibility>();
                playerInsibility.OnInsibilityEnable += PlayerInsibility_OnInsibilityEnable;
            }
        }
    }
    
    public Transform[] PatrolSpots => _patrolSpots;

    public float AgressionDistance => _agressionDistance;
    public float ChaseTimeOut => _chaseTimeOut;
    public float WaitTime => _waitTime;
    
    public LayerMask PlayerLayerMask => _layerMask;

    protected BehaviourStateMachine StateMachine;

    [SerializeField] private int _weight = 0; //количество слотов, которые будут заниматься противником при атаке по игроку

    [SerializeField] private float _waitTime = 1f;
	[SerializeField] private float _agressionDistance;
	[SerializeField] private float _chaseTimeOut = 0.5f;

    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private Transform[] _patrolSpots;

    private GameObject _player;

    protected void Init()
    {
        StateMachine = new BehaviourStateMachine();
        _player = null;
    }

    private void Update()
    {
        StateMachine.Work();
    }

    private void PlayerInsibility_OnInsibilityEnable(object sender, Invisibility.OnInvisibilityEnableEventArgs e)
    {
        if (e.isActive && StateMachine.CurrentState.GetType() != typeof(PatrolState))
        {
            StateMachine.Enter<PatrolState>();
        }
    }
}
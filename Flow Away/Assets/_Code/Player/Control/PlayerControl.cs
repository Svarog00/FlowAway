using Assets.Scripts.Infrastructure.Services;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private GunScript weapon;
    [SerializeField] private PlayerAttack _playerAttack;

    private IInputService _inputService;
    private PlayerMovement _playerMovement;
    private HealingCapsulesController _playerHealing;
    private HotkeysSystem _hotkeysSystem;

    private bool _canMove;

    public bool CanAttack { get; set; }

    public bool CanMove { 
        get => _canMove;
        set 
        {
            _canMove = value;
            if(!_canMove)
            {
                _playerMovement.StopMove();
            }
        }
    }

    private void Awake()
    {
        _inputService = ServiceLocator.Container.Single<IInputService>();
        _hotkeysSystem = new HotkeysSystem(gameObject);
    }

    private void Start()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _playerHealing = GetComponent<HealingCapsulesController>();

        CanAttack = true;
        CanMove = true;
    }

    private void Update()
    {
        AbilitiesInput();
        MovementInput();
        HealingInput();
        MeleeAttackInput();
        GunAttackInput();
    }

    private void AbilitiesInput()
    {
        _hotkeysSystem.GetInput();
    }

    private void MovementInput()
    {
        if(_canMove && _inputService.Axis.sqrMagnitude > 0)
        {
            _playerMovement.HandleMove(_inputService.Axis);
        }
        else
        {
            _playerMovement.StopMove();
        }
    }

    private void HealingInput()
    {
        if (_inputService.IsHealButtonDown())
        {
            _playerHealing.HandleHeal();
        }
    }

    private void MeleeAttackInput()
    {
        if (_inputService.IsMeleeAttackButtonDown() && CanAttack)
        {
            _playerAttack.Melee();
        }
    }

    private void GunAttackInput()
    {
        if(_inputService.IsShootButtonDown() && CanAttack)
        {
            weapon.Attack();
        }
    }
}

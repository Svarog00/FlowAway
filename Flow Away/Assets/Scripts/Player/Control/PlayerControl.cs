using Assets.Scripts.Infrustructure;
using Assets.Scripts.Services;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public WeaponScript weapon;

    private Player_Attack _playerAttack;
    private IInputService _inputService;
    private Player_Movement _playerMovement;
    private PlayerHealthController _playerHealing;

    public bool CanAttack { get; set; }
    public bool CanMove { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        _inputService = Game.InputService;

        _playerMovement = GetComponent<Player_Movement>();
        _playerHealing = GetComponent<PlayerHealthController>();
        _playerAttack = GetComponent<Player_Attack>();
        CanAttack = true;
        CanMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        MovementInput();
        HealingInput();
        MeleeAttackInput();
        GunAttackInput();
    }

    void MovementInput()
    {
        if(CanMove && _inputService.Axis.sqrMagnitude > 0)
        {
            _playerMovement.HandleMove(_inputService.Axis, _inputService.IsDashButtonDown());
        }
    }

    void HealingInput()
    {
        if (_inputService.IsHealButtonDown())
        {
            _playerHealing.HandleHeal();
        }
    }

    void MeleeAttackInput()
    {
        if (_inputService.IsMeleeAttackButtonDown() && CanAttack)
        {
            _playerAttack.Melee();
        }
    }

    void GunAttackInput()
    {
        if(_inputService.IsShootButtonDown() && CanAttack)
        {
            weapon.Attack();
        }
    }
}

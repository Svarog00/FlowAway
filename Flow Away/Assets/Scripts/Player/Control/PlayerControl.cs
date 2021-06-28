using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public WeaponScript weapon;

    private Player_Attack _playerAttack;
    private Player_Movement _playerMovement;
    private Player_Healing _playerHealing;

    public bool CanAttack { get; set; }
    public bool CanMove { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        _playerMovement = GetComponent<Player_Movement>();
        _playerHealing = GetComponent<Player_Healing>();
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
        if(CanMove)
        {
            _playerMovement.HandleMove(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), Input.GetButtonDown("Dash"));
        }
    }

    void HealingInput()
    {
        if (Input.GetButtonDown("Heal"))
        {
            _playerHealing.HandleHeal();
        }
    }

    void MeleeAttackInput()
    {
        if (Input.GetButtonDown("MeleeStrike") && CanAttack)
        {
            _playerAttack.Melee();
        }
    }

    void GunAttackInput()
    {
        if(Input.GetButtonDown("GunFire") && CanAttack)
        {
            weapon.Attack();
        }
    }
}

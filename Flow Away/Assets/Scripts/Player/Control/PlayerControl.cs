using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public WeaponScript weapon;

    public bool CanAttack { get; set; }
    public bool CanMove { get; set; }

    private Player_Attack player_Attack;
    private Player_Movement player_Movement;
    private Player_Healing player_Healing;

    // Start is called before the first frame update
    void Start()
    {
        player_Movement = GetComponent<Player_Movement>();
        player_Healing = GetComponent<Player_Healing>();
        player_Attack = GetComponent<Player_Attack>();
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
            player_Movement.HandleMove(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), Input.GetButtonDown("Dash"));
        }
    }

    void HealingInput()
    {
        if (Input.GetButtonDown("Heal"))
        {
            player_Healing.HandleHeal();
        }
    }

    void MeleeAttackInput()
    {
        if (Input.GetButtonDown("MeleeStrike") && CanAttack)
        {
            player_Attack.Melee();
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

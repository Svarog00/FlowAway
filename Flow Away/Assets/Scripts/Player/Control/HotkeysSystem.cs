using Assets.Scripts.Infrastructure.Services;
using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
struct HotkeyAbility
{
    public Gadget gadget;
    public Action acitvateAbilityAction;
}

public class HotkeysSystem
{
    private IInputService _inputService;
    private GameObject _player;
    private List<HotkeyAbility> _hotkeyAbilities;

    public HotkeysSystem(GameObject player)
    {
        _inputService = ServiceLocator.Container.Single<IInputService>();
        _player = player;
        _hotkeyAbilities = new List<HotkeyAbility>();

        _hotkeyAbilities.Add(new HotkeyAbility 
        { 
            gadget = _player.GetComponent<Invisibility>(), 
            acitvateAbilityAction = () => _player.GetComponent<Invisibility>().HandleActivate() 
        });

        _hotkeyAbilities.Add(new HotkeyAbility
        {
            gadget = _player.GetComponent<PowerShield>(),
            acitvateAbilityAction = () => _player.GetComponent<PowerShield>().HandleActivate()
        });
    }

    public void GetInput()
    {
        if (Input.GetButtonDown("First Module"))
        {
            _hotkeyAbilities[0].acitvateAbilityAction();
        }
        else if (Input.GetButtonDown("Second Module"))
        {
            _hotkeyAbilities[1].acitvateAbilityAction();
        }
    }
}


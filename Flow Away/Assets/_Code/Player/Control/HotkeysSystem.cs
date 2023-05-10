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
    private const string FirstModuleButtonName = "First Module";
    private const string SecondModuleButtonName = "Second Module";
    private const string ThirdModuleButtonName = "Third Module";

    private IInputService _inputService;
    private GameObject _player;
    private List<HotkeyAbility> _hotkeyAbilities;

    public HotkeysSystem(GameObject player)
    {
        _inputService = ServiceLocator.Container.Single<IInputService>();
        _player = player;
        _hotkeyAbilities = new List<HotkeyAbility>
        {
            new HotkeyAbility
            {
                gadget = _player.GetComponent<Invisibility>(),
                acitvateAbilityAction = () => _player.GetComponent<Invisibility>().HandleActivate()
            },

            new HotkeyAbility
            {
                gadget = _player.GetComponent<PowerShield>(),
                acitvateAbilityAction = () => _player.GetComponent<PowerShield>().HandleActivate()
            },

            new HotkeyAbility
            {
                gadget = _player.GetComponent<HookController>(),
                acitvateAbilityAction = () => _player.GetComponent<HookController>().HandleActivate()
            }
        };
    }

    public void GetInput()
    {
        if (_inputService.IsAbilityButtonDown(FirstModuleButtonName))
        {
            _hotkeyAbilities[0].acitvateAbilityAction();
        }
        else if (_inputService.IsAbilityButtonDown(SecondModuleButtonName))
        {
            _hotkeyAbilities[1].acitvateAbilityAction();
        }
        else if (_inputService.IsAbilityButtonDown(ThirdModuleButtonName))
        {
            _hotkeyAbilities[2].acitvateAbilityAction();
        }
    }
}


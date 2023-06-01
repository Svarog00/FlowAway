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
    private GadgetManager _gadgetManager;

    private List<HotkeyAbility> _hotkeyAbilities;

    public HotkeysSystem(GameObject player)
    {
        _inputService = ServiceLocator.Container.Single<IInputService>();
        _player = player;
        _gadgetManager = _player.GetComponent<GadgetManager>();
        _hotkeyAbilities = new List<HotkeyAbility>();

        for(int i = 0; i < _gadgetManager.Gadgets.Count; i++)
        {
            int index = i;
            _hotkeyAbilities.Add(new HotkeyAbility
            {
                gadget = _gadgetManager.Gadgets[index],
                acitvateAbilityAction = () => _gadgetManager.Gadgets[index].HandleActivate(),
            });
        }
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
        else if (_inputService.IsDashButtonDown())
        {
            _hotkeyAbilities[3].acitvateAbilityAction();
        }
    }
}


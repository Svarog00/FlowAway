using System.Collections;
using UnityEngine;
using System;
using static PlayerHealthController;

public class HealingCapsulesController : MonoBehaviour
{
    public event EventHandler<OnCapsulesCountChangedEventArgs> OnCapsulesCountChanged;
    public class OnCapsulesCountChangedEventArgs : EventArgs
    {
        public enum OperationType { Add, Remove }

        public OperationType OperType;
        public int CapsulesCount;
    }

    public int CapsulesCount => _capsulesModel.Count;

    [SerializeField] private int _initCapsuleCount;
    [SerializeField] private PlayerHealthController _playerHealth;

    private HealingCapsulesModel _capsulesModel;

    // Use this for initialization
    void Start()
    {
        _capsulesModel = new HealingCapsulesModel(_initCapsuleCount);

        OnCapsulesCountChanged?.Invoke(this, new OnCapsulesCountChangedEventArgs
        {
            OperType = OnCapsulesCountChangedEventArgs.OperationType.Add,
            CapsulesCount = _initCapsuleCount
        });
    }

    public void AddCapsule()
    {
        _capsulesModel.Count++;

        OnCapsulesCountChanged?.Invoke(this, new OnCapsulesCountChangedEventArgs
        {
            OperType = OnCapsulesCountChangedEventArgs.OperationType.Add,
            CapsulesCount = _capsulesModel.Count
        });
    }

    public void LoadCapsule(int count)
    {
        _capsulesModel.Count = count;
        OnCapsulesCountChanged?.Invoke(this, new OnCapsulesCountChangedEventArgs
        {
            OperType = OnCapsulesCountChangedEventArgs.OperationType.Add,
            CapsulesCount = count
        });
    }

    public void HandleHeal()
    {
        if (_capsulesModel.Count > 0 && _playerHealth.CurrentHealth < _playerHealth.MaxHealth)
        {
            AudioManager.Instance.Play("Heal");

            _playerHealth.Heal();

            OnCapsulesCountChanged?.Invoke(this, new OnCapsulesCountChangedEventArgs
            {
                OperType = OnCapsulesCountChangedEventArgs.OperationType.Remove,
                CapsulesCount = _capsulesModel.Count
            });

            _capsulesModel.Count--;
        }
    }
}
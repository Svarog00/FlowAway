using System.Collections;
using UnityEngine;
using System;

public class HealingCapsulesController : MonoBehaviour
{
    public event EventHandler<OnCapsulesCountChangedEventArgs> OnCapsulesCountChanged;
    public class OnCapsulesCountChangedEventArgs : EventArgs
    {
        public enum OperationType { Add, Remove }

        public OperationType OperType;
        public int CapsulesCount;
    }


    [SerializeField] private int _initCapsuleCount;

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

    // Update is called once per frame
    void Update()
    {

    }
}
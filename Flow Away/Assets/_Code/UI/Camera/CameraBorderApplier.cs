using UnityEngine;
using Unity.Cinemachine;

public class CameraBorderApplier : MonoBehaviour
{
    [SerializeField] private Collider2D _confiner;

    private CinemachineConfiner2D _cinemachineVirtualCamera;

    private void Awake()
    {
        _cinemachineVirtualCamera = FindFirstObjectByType<CinemachineConfiner2D>();
    }

    private void Start()
    {
        _cinemachineVirtualCamera.BoundingShape2D = _confiner;
    }
}

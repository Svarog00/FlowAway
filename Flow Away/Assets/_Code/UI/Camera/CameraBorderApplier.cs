using UnityEngine;
using Cinemachine;

public class CameraBorderApplier : MonoBehaviour
{
    [SerializeField] private Collider2D _confiner;

    private CinemachineConfiner _cinemachineVirtualCamera;

    private void Awake()
    {
        _cinemachineVirtualCamera = FindObjectOfType<CinemachineConfiner>();
    }

    private void Start()
    {
        _cinemachineVirtualCamera.m_BoundingShape2D = _confiner;
    }
}

using Unity.Cinemachine;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private CinemachineCamera _camera;

    public void Follow(GameObject hero)
    {
        _camera.Target.TrackingTarget = hero.transform;
    }
}

using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public void Follow(GameObject hero)
    {
        GetComponentInChildren<CinemachineVirtualCamera>().Follow = hero.transform;
    }
}

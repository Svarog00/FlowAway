using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CapsulesUI : MonoBehaviour
{
    public int maxCapsules;
    public Image[] currentCapsules;

    public Sprite fullCapsule;
    public Sprite emptyCapsule;

    public void Start()
    {
        
    }

    public void AddCapsule(int capsuleCount)
    {
        for(int i = 0; i < capsuleCount; i++)
        {
            currentCapsules[i].sprite = fullCapsule;
        }
    }

    public void RemoveCapsule(int capsuleCount)
    {
        currentCapsules[capsuleCount - 1].sprite = emptyCapsule;
    }



}

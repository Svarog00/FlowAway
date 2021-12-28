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
        FindObjectOfType<PlayerHealthController>().OnCapsulesCountChanged += CapsulesUI_OnCapsulesCountChanged;
        AddCapsule(FindObjectOfType<PlayerHealthController>().GetCapsuleCount());
    }

    private void CapsulesUI_OnCapsulesCountChanged(object sender, PlayerHealthController.OnCapsulesCountChangedEventArgs e)
    {
        switch (e.OperType)
        {
            case PlayerHealthController.OnCapsulesCountChangedEventArgs.OperationType.Add :
                AddCapsule(e.CapsulesCount);
                break;

            case PlayerHealthController.OnCapsulesCountChangedEventArgs.OperationType.Remove:
                RemoveCapsule(e.CapsulesCount);
                break;
        }
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

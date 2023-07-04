using UnityEngine;
using UnityEngine.UI;

using static HealingCapsulesController.OnCapsulesCountChangedEventArgs;

public class CapsulesUI : MonoBehaviour
{
    public Image[] currentCapsules;
    
    public int maxCapsules;

    public Sprite fullCapsule;
    public Sprite emptyCapsule;

    public void Start()
    {
        FindObjectOfType<HealingCapsulesController>().OnCapsulesCountChanged += CapsulesUI_OnCapsulesCountChanged;
        AddCapsule(FindObjectOfType<HealingCapsulesController>().CapsulesCount);
    }

    private void CapsulesUI_OnCapsulesCountChanged(object sender, HealingCapsulesController.OnCapsulesCountChangedEventArgs e)
    {
        switch (e.OperType)
        {
            case OperationType.Add :
                AddCapsule(e.CapsulesCount);
                break;

            case OperationType.Remove:
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

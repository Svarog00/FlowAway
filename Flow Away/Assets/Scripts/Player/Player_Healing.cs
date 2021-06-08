using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Healing : MonoBehaviour
{
    private Player_Health playerHealth;
    public CapsulesUI capsuleUI;

    [SerializeField]
    private int capsuleCount;

    private void Start()
    {
        playerHealth = GetComponent<Player_Health>();
        capsuleUI.AddCapsule(capsuleCount);
    }

    public int GetCapsuleCount()
    {
        return capsuleCount;
    }

    public void AddCapsuleCount() 
    {
        capsuleCount++;
    }

    public void LoadCapsule(int count)
    {
        capsuleCount = count;
        capsuleUI.AddCapsule(count);
    }
  
    public void HandleHeal()
    {
        if (capsuleCount > 0 && playerHealth.CurrentHealth < playerHealth.MaxHealth)
        {
            playerHealth.Heal();
            capsuleUI.RemoveCapsule(capsuleCount);
            capsuleCount--;
        }
    }
}

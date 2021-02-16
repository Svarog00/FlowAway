using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    public Slider bossHealthBar;

    public void SetMaxHealth(int maxHealth)
    {
        bossHealthBar.maxValue = maxHealth;
        bossHealthBar.value = maxHealth;
    }

    public void SetHealth(int health)
    {
        bossHealthBar.value = health;
    }
}

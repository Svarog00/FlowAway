using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    public Slider bossHealthBar;


    private void Start()
    {
        Boss boss = FindObjectOfType<Boss>();
        boss.OnHealthChanged += Boss_OnHealthChanged;
    }

    private void Boss_OnHealthChanged(object sender, Boss.OnHealthChangedEventArgs e)
    {
        bossHealthBar.value = e.currentHealth;
    }
}

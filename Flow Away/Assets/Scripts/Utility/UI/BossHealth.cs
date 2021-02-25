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
        boss.OnHealthChanged += SetHealth;
        bossHealthBar.maxValue = boss._healthPointMax;
        bossHealthBar.value = boss._healthPointMax;
    }

    public void SetHealth(object sender, Boss.OnHealthChangedEventArgs e)
    {
        bossHealthBar.value = e.currentHealth;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    public Slider bossHealthBar;//полоса здоровья босса
    public Image phaseIcon;//иконка фазы
    public GameObject visualHealthBar;//весь визульный UI босса
    public Boss boss;

    private void Start()
    {
        boss.OnHealthChanged += Boss_OnHealthChanged;
        boss.OnBossActivatedUI += Boss_OnActivatedUI;
        boss.OnBossPhaseIconChange += Boss_OnPhaseIconChange;
        bossHealthBar.maxValue = boss.healthPointMax;//инициализация начальных значений для правильного отображения здоровья на слайдере 
        bossHealthBar.value = boss.healthPointMax;
    }

    private void Boss_OnActivatedUI(object sender, Boss.OnBossActivatedUIEventArgs e)
    {
        if (e.setUIFalse == false)
        {
            visualHealthBar.SetActive(false);
        }
        else if (e.setUIFalse == true)
        {
            visualHealthBar.SetActive(true);      
        }
    }
    private void Boss_OnHealthChanged(object sender, Boss.OnHealthChangedEventArgs e)
    {
        bossHealthBar.value = e.currentHealth;
    }

    private void Player_OnDeath(object sender, EventArgs e)
    {
        visualHealthBar.SetActive(false);
    }

    private void Boss_OnPhaseIconChange(object sender, Boss.OnPhaseIconChangeEventArgs e)
    {
        phaseIcon.sprite = e.changedSprite;
    }
}

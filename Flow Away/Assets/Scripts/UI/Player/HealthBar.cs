using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthBar;

    private void Start()
    {
        Player_Health player = FindObjectOfType<Player_Health>();
        player.OnHealthChanged += Player_OnHealthChanged;
    }

    private void Player_OnHealthChanged(object sender, Player_Health.OnHealthChangedEventArgs e)
    {
        healthBar.value = e.curHealth;
    }
}

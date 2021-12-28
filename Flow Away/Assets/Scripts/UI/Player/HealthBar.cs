using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthBar;

    private void Start()
    {
        FindObjectOfType<PlayerHealthController>().OnHealthChanged += Player_OnHealthChanged;
    }

    public void Player_OnHealthChanged(object sender, PlayerHealthController.OnHealthChangedEventArgs e)
    {
        healthBar.value = e.curHealth;
    }
}

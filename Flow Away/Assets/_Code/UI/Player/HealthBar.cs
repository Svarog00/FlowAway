using UnityEngine;
using UnityEngine.UI;

using CustomEventArguments;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider healthBar;

    private void Start()
    {
        FindObjectOfType<PlayerHealthController>().OnHealthChanged += Player_OnHealthChanged;
    }

    public void Player_OnHealthChanged(object sender, OnHealthChangedEventArgs e)
    {
        healthBar.value = e.CurHealth;
    }
}

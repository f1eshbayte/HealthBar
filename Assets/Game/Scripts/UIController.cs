using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private PlayerHealth _player;
    [SerializeField] private HealthBar _healthBar;

    private void OnEnable()
    {
        _player.OnHealthChanged.AddListener(_healthBar.UpdateHealth);
    }

    private void OnDisable()
    {
        _player.OnHealthChanged.RemoveListener(_healthBar.UpdateHealth);
    }

    public void OnDamageButton()
    {
        _player.TakeDamage(_player.DamageHealth);
    }

    public void OnHealButton()
    {
        _player.Heal(_player.HealHealth);
    }
}
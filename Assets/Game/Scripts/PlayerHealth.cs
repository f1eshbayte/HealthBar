using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float _maxHealth = 100;
    
    public readonly float DamageHealth = 10;
    public readonly float HealHealth = 10;
    
    public UnityEvent<float> OnHealthChanged;
    
    private float _currentHealth;

    private void Start()
    {
        _currentHealth = _maxHealth;
        OnHealthChanged?.Invoke(GetHealthNormalized());
    }

    public void TakeDamage(float amount)
    {
        _currentHealth = Mathf.Max(_currentHealth - amount, 0);
        OnHealthChanged?.Invoke(GetHealthNormalized());
    }

    public void Heal(float amount)
    {
        _currentHealth = Mathf.Min(_currentHealth + amount, _maxHealth);
        OnHealthChanged?.Invoke(GetHealthNormalized());
    }

    private float GetHealthNormalized()
    {
        return _currentHealth / _maxHealth;
    }
}
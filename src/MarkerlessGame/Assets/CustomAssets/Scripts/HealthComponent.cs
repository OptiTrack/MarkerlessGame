using UnityEngine;

public class HealthComponent : MonoBehaviour
{

    public float MaxHealth;
    private float _currentHealth;

    void Start()
    {
        _currentHealth = MaxHealth;
    }

    public void AdjustHealth(float amount)
    {
        // Set health to clamped, adjusted value: 0 <= _currentHealth <= MaxHealth
        _currentHealth = Mathf.Clamp(_currentHealth + amount, 0, MaxHealth);
        Debug.Log("Current health: " + _currentHealth);

        // Trigger 'OnDeath' if health at 0
        if (_currentHealth <= 0)
        {
            OnDeath();
        }
    }

    void OnDeath()
    {
        // Write to console and destroy this game object
        Debug.Log("Health at 0, object is dead");
        Destroy(gameObject);
    }
}

using System;
using UnityEngine;

public class HealthComponent : MonoBehaviour, IHitResponder
{
    public bool fireOnDeathEvent = true;
    public bool destroyOnDeath = false;

    public float maxHealth = 100f;
    private float _currentHealth;

    private void Awake() => SetHealth(maxHealth);

    public void SetHealth(float value)
    {
        _currentHealth = Mathf.Clamp(value, 0, maxHealth);
        Debug.Log($"Health set to {_currentHealth}");

        HealthAdjustedContext healthAdjustedContext = new HealthAdjustedContext
        {
            entity = gameObject,
            healthComponent = this,
            newHealth = _currentHealth
        };

        GameEventBus.Raise(healthAdjustedContext);
    }

    public float GetCurrentHealth()
    {
        return _currentHealth;
    }

    // Method required by IHitResponder interface; accepts a hit context sent by hurtbox
    public void HandleHit(HitContext context)
    {
        Debug.Log("Before atttack, health at " + _currentHealth);

        SetHealth(_currentHealth - context.baseDamage);

        Debug.Log("After attack, health now at " + _currentHealth);

        if (_currentHealth <= 0 && fireOnDeathEvent)
        {
            OnDeath();
        }
    }

    void OnDeath()
    {
        Debug.Log("Health at 0, object is dead");

        DefeatEvent defeatEvent = new DefeatEvent
        {
            victim = gameObject
        };

        GameEventBus.Raise(defeatEvent);

        if (destroyOnDeath)
        {
            Destroy(gameObject);
        }
    }
}

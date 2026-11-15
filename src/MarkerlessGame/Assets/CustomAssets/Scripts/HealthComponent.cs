using UnityEngine;

public class HealthComponent : MonoBehaviour, IHitResponder
{

    public float maxHeatlh;
    private float _currentHealth;

    private void Awake() => SetHealth(maxHeatlh);

    public void SetHealth(float value)
    {
        _currentHealth = value;
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

        if (_currentHealth <= 0)
        {
            OnDeath();
        }
    }

    void OnDeath()
    {
        Debug.Log("Health at 0, object is dead");

        DefeatEvent defeatEvent = new DefeatEvent
        {
            Victim = gameObject
        };

        GameEventBus.Raise(defeatEvent);
    }
}

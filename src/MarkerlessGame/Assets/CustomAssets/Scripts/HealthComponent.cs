using UnityEngine;

public class HealthComponent : MonoBehaviour, IHitResponder
{

    [SerializeField] private float _maxHealth;
    private float _currentHealth;

    private void Awake() => _currentHealth = _maxHealth;

    // Method required by IHitResponder interface; accepts a hit context sent by hurtbox
    public void HandleHit(HitContext context)
    {
        _currentHealth -= context.BaseDamage;

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

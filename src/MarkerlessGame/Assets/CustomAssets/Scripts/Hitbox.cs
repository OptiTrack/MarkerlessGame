using UnityEngine;
using UnityEngine.Rendering;

public class Hitbox : MonoBehaviour
{
    public bool IsInvuln = false;

    private HealthComponent _healthComponent;

    void Start()
    {
        _healthComponent = GetComponent<HealthComponent>();
    }

    public bool RecieveHit(Attack attack, float speed)
    {
        if (!IsInvuln)
        {
            float damageTaken = Mathf.Clamp(attack.BaseDamage + (speed * attack.SpeedBonusPercent), attack.BaseDamage, attack.MaxDamage);
            Debug.Log("Taking this much damage: " + damageTaken);
            _healthComponent.AdjustHealth(-damageTaken);
            return true;
        }

        return false;
    }
   
}

using UnityEngine;

public class Hitbox : MonoBehaviour
{
    [SerializeField] private Attack _attack;
    private SpeedCalculator _speedCalculator;

    private void Start() => _speedCalculator = GetComponent<SpeedCalculator>();

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hitbox collided with " + other.gameObject.name);

        // Create and send a hit context to a hurtbox if detected
        if (other.TryGetComponent<Hurtbox>(out Hurtbox hurtbox))
        {
            Debug.Log("Hurtbox detected, attempting to damage");

            float attackDamage = _attack.BaseDamage;

            if (_speedCalculator)
            {
                attackDamage += _attack.BonusDamageScalingCurve.Evaluate(_speedCalculator.ObjectSpeed);
            }

            attackDamage = Mathf.Clamp(attackDamage, 0, _attack.MaxDamage);

            HitContext context = new HitContext
            {
                Attacker = gameObject,
                Victim = other.gameObject,
                BaseDamage = attackDamage
            };

            Debug.Log("Sending " + attackDamage + " to hurtbox");
            hurtbox.RecieveHit(context);
        }
    }
}

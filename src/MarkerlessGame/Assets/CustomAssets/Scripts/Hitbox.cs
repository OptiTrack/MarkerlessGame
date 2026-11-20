using UnityEngine;

public class Hitbox : MonoBehaviour
{
    public Attack attack;
    public SpeedCalculator speedCalculator;

    private void Start() {if (!speedCalculator) speedCalculator = GetComponent<SpeedCalculator>();}

    public HitContext SendHit(Hurtbox target)
    {
        float attackDamage = attack.baseDamage;

        if (speedCalculator)
        {
            attackDamage += attackDamage * attack.bonusDamageScalingCurve.Evaluate(speedCalculator.currentSpeed);

            Debug.Log("Attack going at " + speedCalculator.currentSpeed + " speed, so damage now up to " + attackDamage);
        }

        attackDamage = Mathf.Clamp(attackDamage, 0, attack.maxDamage);

        HitContext context = new HitContext
        {
            Attacker = gameObject,
            Victim = target.gameObject,
            baseDamage = attackDamage
        };

        Debug.Log("Sending " + attackDamage + " to hurtbox");
        target.RecieveHit(context);

        return context;
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hitbox collided with " + other.gameObject.name);

        // Create and send a hit context to a hurtbox if detected
        if (other.TryGetComponent<Hurtbox>(out Hurtbox hurtbox))
        {
            Debug.Log("Hurtbox detected, attempting to damage");

            SendHit(hurtbox);
        }
    }
}

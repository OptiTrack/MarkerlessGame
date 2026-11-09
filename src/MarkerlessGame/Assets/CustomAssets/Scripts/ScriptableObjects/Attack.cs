using UnityEngine;

[CreateAssetMenu(fileName = "Attack", menuName = "Scriptable Objects/Attack")]
public class Attack : ScriptableObject
{
    public float BaseDamage;
    public float MaxDamage;
    public AnimationCurve BonusDamageScalingCurve = AnimationCurve.EaseInOut(0f, 0f, 9f, 5f);

    private void OnValidate()
    {
        if (BaseDamage > MaxDamage)
        {
            BaseDamage = MaxDamage;
        }
    }

}

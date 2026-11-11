using UnityEngine;

[CreateAssetMenu(fileName = "Attack", menuName = "Scriptable Objects/Attack")]
public class Attack : ScriptableObject
{
    public float baseDamage;
    public float maxDamage;
    public AnimationCurve bonusDamageScalingCurve = AnimationCurve.EaseInOut(0f, 0f, 9f, 5f);

    private void OnValidate()
    {
        if (baseDamage > maxDamage)
        {
            baseDamage = maxDamage;
        }
    }

}

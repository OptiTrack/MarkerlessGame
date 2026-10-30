using UnityEngine;

[CreateAssetMenu(fileName = "Attack", menuName = "Scriptable Objects/Attack")]
public class Attack : ScriptableObject
{
    public float BaseDamage;
    public float MaxDamage;
    public float SpeedBonusPercent;
}

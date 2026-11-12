using UnityEngine;

public interface IEventPayload { }

public interface IHitResponder
{
    void HandleHit(HitContext context);
}

public struct HitContext : IEventPayload
{
    public GameObject Attacker;
    public GameObject Victim;
    public float baseDamage;
}

public struct DefeatEvent : IEventPayload
{
    public GameObject Victim;
}
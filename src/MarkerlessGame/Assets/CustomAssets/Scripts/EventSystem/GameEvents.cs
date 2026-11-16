using UnityEngine;

public interface IEventPayload { }

public interface IHitResponder
{
    void HandleHit(HitContext context);
}

public struct HitContext : IEventPayload
{
    public GameObject attacker;
    public GameObject victim;
    public float baseDamage;
}

public struct DefeatEvent : IEventPayload
{
    public GameObject victim; // Easier to track than "winner" in health components
    public float timeLeft;

    // Change or add data as needed
}

public struct ScoreUpdateContext : IEventPayload
{
    public int newScore;
}
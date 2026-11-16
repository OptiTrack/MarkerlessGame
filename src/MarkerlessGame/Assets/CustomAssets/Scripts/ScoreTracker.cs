using UnityEngine;

public class ScoreTracker : MonoBehaviour
{
    public readonly struct ScoreValues // Adjust values during testing
    {
        public static int regularHit = 100;
        public static int weakpointHit = 500;
        public static int remainingHealth = 10;
        public static int remainingTime = 10;
        public static int fightWon = 5000;
    }

    public int currentPlayerScore = 0;

    private void OnEnable()
    {
        GameEventBus.Subscribe<HitContext>(UpdateScoreFromHit);
        GameEventBus.Subscribe<DefeatEvent>(FinalizeScore);
    }

    private void OnDisable()
    {
        GameEventBus.Unsubcribe<HitContext>(UpdateScoreFromHit);
        GameEventBus.Unsubcribe<DefeatEvent>(FinalizeScore);
    }

    public void UpdateScoreFromHit(HitContext context)
    {
        // Only update the score if the enemy was hit by the player
        // Self-inflicted player collisions shouldn't occur, but we add the check just in case
        if (context.attacker.CompareTag("Player") && context.victim.CompareTag("Enemy")) // Placeholder, may need to change further into development
        {
            // Add logic here to check in context if a weakpoint was hit

            // Scale added hit score based on damage/speed
            currentPlayerScore += ScoreValues.regularHit * (int)context.baseDamage;
        }

        ScoreUpdateContext scoreContext = new ScoreUpdateContext
        {
            newScore = currentPlayerScore
        };

        GameEventBus.Raise(scoreContext);
    }

    public void FinalizeScore(DefeatEvent context)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player"); // Could move reference to start if needed elsewhere
        
        if (player.TryGetComponent<HealthComponent>(out HealthComponent healthComponent))
        {
            currentPlayerScore += ScoreValues.remainingHealth * (int)healthComponent.GetCurrentHealth();
        }

        if (context.victim == null)
        {
            // No winner; timeout
        }
        else if (context.victim.CompareTag("Enemy"))
        {
            currentPlayerScore += ScoreValues.fightWon;

            currentPlayerScore += ScoreValues.remainingTime * (int)context.timeLeft;
        }

        ScoreUpdateContext scoreContext = new ScoreUpdateContext
        {
            newScore = currentPlayerScore
        };

        GameEventBus.Raise(scoreContext);
    }
}

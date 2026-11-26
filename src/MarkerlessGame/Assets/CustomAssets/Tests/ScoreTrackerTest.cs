using NUnit.Framework;
using UnityEngine;

public class ScoreTrackerTest
{
    private GameObject trackerObject;
    private ScoreTracker scoreTracker;
    private GameObject player;
    private HealthComponent healthComponent;
    private GameObject enemy;

    // Setup for tests
    [SetUp]
    public void Setup()
    {
        // Setup for player game object
        player = new GameObject("TestPlayer");
        player.SetActive(false);
        player.tag = "Player";
        healthComponent = player.AddComponent<HealthComponent>();
        healthComponent.maxHeatlh = 100;
        player.SetActive(true);

        // Setup for enemy game object
        enemy = new GameObject("TestEnemy");
        enemy.tag = "Enemy";

        // Setup for score tracker game object
        trackerObject = new GameObject("ScoreTracker_Test");
        trackerObject.SetActive(false);
        scoreTracker = trackerObject.AddComponent<ScoreTracker>();
        trackerObject.SetActive(true);
    }

    // Clean-up after tests
    [TearDown]
    public void TearDown()
    {
        if (trackerObject != null)
        {
            Object.DestroyImmediate(trackerObject);
            trackerObject = null;
            scoreTracker = null;
        }

        if (player != null)
        {
            Object.DestroyImmediate(player);
            player = null;
            healthComponent = null;
        }

        if (enemy != null)
        {
            Object.DestroyImmediate(enemy);
            enemy = null;
        }
    }

    // Sends different hit contexts to the score tracker to ensure score is only added from damage dealt to enemy
    [Test]
    public void ScoreTracker_OnlyUpdateScoreOnHitFromPlayerToEnemy()
    {
        // Define a base damage value that will scale the points recieved and declare what's expected
        const int baseDamage = 5;
        int expectedPlayerScore = ScoreTracker.ScoreValues.regularHit * baseDamage;

        // New hit context for a hit delivered onto the enemy from the player
        HitContext enemyHitContext = new HitContext
        {
            victim = enemy,
            attacker = player,
            baseDamage = baseDamage
        };

        // Attempt to update the score from this player-inflicted hit
        scoreTracker.UpdateScoreFromHit(enemyHitContext);

        // Ensure the score value matches what is expected based on the base damage and attacker/victim
        Assert.AreEqual(
            expectedPlayerScore, 
            scoreTracker.currentPlayerScore, 
            "Score from player hit on enemy not calculated properly."
        );

        // Another new hit context for a hit delivered onto the player from the enemy instead
        HitContext playerHitContext = new HitContext
        {
            victim = player,
            attacker = enemy,
            baseDamage = baseDamage
        };

        // Attempt to update the score from the enemy-inflicted hit
        // Note: this should not update the score, since score is only added from player actions/hits
        scoreTracker.UpdateScoreFromHit(playerHitContext);

        // Ensure the score did not change from the previous hit since it was delivered onto the player
        Assert.AreEqual(
            expectedPlayerScore, 
            scoreTracker.currentPlayerScore, 
            "Score was inapropriately changed from enemy hit on player."
        );
    }

    // Tests the finalization of player score given the player wins, and has remaining health and time
    [Test]
    public void ScoreTracker_FinalizeScoreOnVictory()
    {
        // Define values for player's remaining health and time left on clock
        const int remainingHealth = 30;
        const int timeLeft = 20;

        // Expect final score to be a flat bonus for winning plus scaled values based on health and time left
        int expectedPlayerScore = remainingHealth * ScoreTracker.ScoreValues.remainingHealth
                                + timeLeft * ScoreTracker.ScoreValues.remainingTime
                                + ScoreTracker.ScoreValues.fightWon;

        // Set the player's health to the correct value
        healthComponent.SetHealth(remainingHealth);

        // New defeat context in which the player one (i.e. the victim was the enemy game object)
        DefeatEvent context = new DefeatEvent
        {
            victim = enemy,
            timeLeft = timeLeft
        };

        // Finalize the score with this context
        scoreTracker.FinalizeScore(context);

        // Ensure that the final score calculated matches the expected with properly scaled "bonus points"
        Assert.AreEqual(
            expectedPlayerScore, 
            scoreTracker.currentPlayerScore,
            "Final score on victory does not match remaining health, remaining time, and win bonus."
        );
    }

    // Tests the finalization of player score given the player loses the match
    [Test]
    public void ScoreTracker_FinalizeScoreOnLoss()
    {
        // Define how much time is left so we can make sure no bonus points are awarded
        const int timeLeft = 15;

        // Only expected points would come from any hits landed from player on enemy
        int expectedPlayerScore = 0;

        // Player lost, so set health to 0
        healthComponent.SetHealth(0);

        // New defeat context in which the player lost (i.e. player is the victim)
        DefeatEvent context = new DefeatEvent
        {
            victim = player,
            timeLeft = timeLeft
        };

        // Finalize score with this context
        scoreTracker.FinalizeScore(context);

        // Make sure no points are awarded, since player has no health and they lost
        Assert.AreEqual(
            expectedPlayerScore, 
            scoreTracker.currentPlayerScore,
            "Final score value does not only contain scores form hits."
        );
    }

    // Tests the finalization of player score given the player is alive and runs out of time
    [Test]
    public void ScoreTracker_FinalizeScoreOnTimeout()
    {
        // Define how much remaining health the player has and expected bonus points based on this value
        const int remainingHealth = 5;
        int expectedPlayerScore = remainingHealth * ScoreTracker.ScoreValues.remainingHealth;

        // Set the health to the remaining value
        healthComponent.SetHealth(remainingHealth);

        // New defeat context in which no-one won and time ran out (i.e. no victim, fight was a "draw")
        DefeatEvent context = new DefeatEvent
        {
            victim = null,
            timeLeft = 0
        };

        // Finalize the score with this context
        scoreTracker.FinalizeScore(context);

        // Make sure the player's final score is granted bonus points for remaining health and nothing more
        Assert.AreEqual(
            expectedPlayerScore, 
            scoreTracker.currentPlayerScore,
            "Final score value does match remaining health bonus."
        );
    }
}

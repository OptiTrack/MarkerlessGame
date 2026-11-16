using System.Collections;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

public class ScoreTrackerTest
{
    private ScoreTracker scoreTracker;
    private GameObject player;
    private HealthComponent healthComponent;
    private GameObject enemy;

    [SetUp]
    public void Setup()
    {
        scoreTracker = new ScoreTracker();

        player = new GameObject("TestPlayer");
        player.tag = "Player";
        healthComponent = player.AddComponent<HealthComponent>();

        enemy = new GameObject("TestEnemy");
        enemy.tag = "Enemy";
    }

    // Clean-up after tests
    [TearDown]
    public void TearDown()
    {
        if (player != null) Object.DestroyImmediate(player);
        if (enemy != null) Object.DestroyImmediate(enemy);
    }

    [Test]
    public void ScoreTracker_OnlyUpdateScoreOnHitFromPlayerToEnemy()
    {
        HitContext enemyHitContext = new HitContext
        {
            victim = enemy,
            attacker = player,
            baseDamage = 5
        };

        scoreTracker.UpdateScoreFromHit(enemyHitContext);

        int expectedPlayerScore = ScoreTracker.ScoreValues.regularHit * 5;

        Assert.AreEqual(expectedPlayerScore, scoreTracker.currentPlayerScore, 
                        "Score from hit by player not calculated properly.");

        HitContext playerHitContext = new HitContext
        {
            victim = player,
            attacker = enemy,
            baseDamage = 5
        };

        scoreTracker.UpdateScoreFromHit(playerHitContext);

        Assert.AreEqual(expectedPlayerScore, scoreTracker.currentPlayerScore, 
                        "Score incorrectly changed from an enemy hit.");
    }

    [Test]
    public void ScoreTracker_FinalizeScoreOnVictory()
    {
        healthComponent.SetHealth(30);

        DefeatEvent context = new DefeatEvent
        {
            victim = enemy,
            timeLeft = 20
        };

        scoreTracker.FinalizeScore(context);

        int expectedPlayerScore = 30 * ScoreTracker.ScoreValues.remainingHealth 
                                + 20 * ScoreTracker.ScoreValues.remainingTime 
                                + ScoreTracker.ScoreValues.fightWon;

        Assert.AreEqual(expectedPlayerScore, scoreTracker.currentPlayerScore, 
                        "Final score value is not set properly based on the winner and remaining time & health");
    }

    [Test]
    public void ScoreTracker_FinalizeScoreOnLoss()
    {
        healthComponent.SetHealth(0);

        DefeatEvent context = new DefeatEvent
        {
            victim = player,
            timeLeft = 15
        };

        scoreTracker.FinalizeScore(context);

        int expectedPlayerScore = 0;

        Assert.AreEqual(expectedPlayerScore, scoreTracker.currentPlayerScore,
                        "Final score value does not only contain scores form hits");
    }

    [Test]
    public void ScoreTracker_FinalizeScoreOnTimeout()
    {
        healthComponent.SetHealth(5);

        DefeatEvent context = new DefeatEvent
        {
            victim = null,
            timeLeft = 0
        };

        scoreTracker.FinalizeScore(context);

        int expectedPlayerScore = 5 * ScoreTracker.ScoreValues.remainingHealth;

        Assert.AreEqual(expectedPlayerScore, scoreTracker.currentPlayerScore,
                        "Final score value does not only contain scores form hits and remaining health");
    }
}

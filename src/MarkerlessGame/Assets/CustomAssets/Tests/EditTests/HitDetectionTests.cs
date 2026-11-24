using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;
using static UnityEngine.GraphicsBuffer;

public class HitDetectionTests
{
    private GameObject _victim;
    private Hurtbox _hurtbox;
    private HealthComponent _healthComponent;

    private GameObject _attacker;
    private Hitbox _hitbox;
    private SpeedCalculator _speedCalculator;

    // Setup for an attacker and a victim with their respective components needed
    [SetUp]
    public void Setup()
    {
        _victim = new GameObject("TestVictim");
        _hurtbox = _victim.AddComponent<Hurtbox>();
        _healthComponent = _victim.AddComponent<HealthComponent>();
        _hurtbox.InitHitResponders();

        _attacker = new GameObject("TestAttacker");
        _hitbox = _attacker.AddComponent<Hitbox>();
        _speedCalculator = _attacker.AddComponent<SpeedCalculator>();
        _hitbox.speedCalculator = _speedCalculator;
    }
    
    // Cleanup at the end of tests
    [TearDown]
    public void TearDown()
    {
        if (_victim != null) Object.DestroyImmediate(_victim);
        if (_attacker != null) Object.DestroyImmediate(_attacker);
    }

    // Tests speed-based damage scaling on an attack sent from a hitbox
    [UnityTest]
    public IEnumerator Hitbox_CalculatesDamageScalingAndSendsContexToHurtbox()
    {
        // Attack object setup
        Attack attack = new Attack
        {
            baseDamage = 40.0f,
            maxDamage = 200.0f,
            bonusDamageScalingCurve = AnimationCurve.Linear(0, 0, 3, 3) // Can be more complex, simple for test
        };

        _hitbox.attack = attack;

        _speedCalculator.currentSpeed = 2;

        HitContext actualContext = _hitbox.SendHit(_hurtbox);

        // Wait until next frame for response
        yield return null;

        HitContext expectedContext = new HitContext
        {
            Attacker = _attacker,
            Victim = _victim,
            baseDamage = 40.0f + 40.0f * 2.0f
        };

        Assert.AreEqual(expectedContext, actualContext, "Damage scaling was not applied correctly to attack.");
    }

    // Tests hurtboxes recieving a hit context/event (from a hitbox), applying it's own damage multiplier,
    // and triggering responders
    [UnityTest]
    public IEnumerator Hurtbox_RecievesHitContextAndAppliesDamageMultiplier_UpdatesHealthResponder()
    {
        _healthComponent.maxHeatlh = 100.0f;
        _healthComponent.SetHealth(100.0f);
        _hurtbox.damageMultiplier = 1.5f;

        yield return null;

        HitContext context = new HitContext
        {
            Attacker = _attacker,
            Victim = _victim,
            baseDamage = 60.0f
        };

        _hurtbox.RecieveHit(context);

        yield return null;

        Assert.AreEqual(_healthComponent.GetCurrentHealth(), 10.0f, "Damage scaling was not applied correctly.");
    }
}

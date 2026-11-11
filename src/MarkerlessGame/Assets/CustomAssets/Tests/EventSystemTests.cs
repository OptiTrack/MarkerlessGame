using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class EventSystemTests
{
    private bool _eventFired;
    private float _eventDamage;

    private struct TestHitContext : IEventPayload
    {
        public float damage;
    }

    [SetUp]
    public void Setup()
    {
        _eventFired = false;
        _eventDamage = 0f;
    }

    [Test]
    public void EventBus_SubscribeAndRaise_DeliversPayloadCorrectly()
    {
        // Setup subscription
        GameEventBus.Subscribe<TestHitContext>(HandleTestEvent);

        // Raise/trigger event to listeners
        GameEventBus.Raise(new TestHitContext { damage = 32.5f });

        // Check if listener was invoked and updated values
        Assert.IsTrue(_eventFired, "The event listener was never invoked.");
        Assert.AreEqual(32.5f, _eventDamage, "The event payload data does not match what is expected.");

        // Cleanup
        GameEventBus.Unsubcribe<TestHitContext>(HandleTestEvent);
    }

    [Test]
    public void EventBus_Unsubscribe_StopsRecievingEvents()
    {
        // Subscribe and unsubscribe from event on event bus
        GameEventBus.Subscribe<TestHitContext>(HandleTestEvent);
        GameEventBus.Unsubcribe<TestHitContext>(HandleTestEvent);

        // Trigger event and expect no listeners to be invoked
        GameEventBus.Raise(new TestHitContext { damage = 20.0f });

        Assert.IsFalse(_eventFired, "Listener was invoked after unsubscribing.");
    }

    private void HandleTestEvent(TestHitContext context)
    {
        _eventFired = true;
        _eventDamage = context.damage;
    }
}

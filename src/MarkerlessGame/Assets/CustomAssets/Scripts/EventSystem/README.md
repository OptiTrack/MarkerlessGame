# Custom Event System

This event system uses pre-defined structs that contain fields describing the event context. These context structs extend an empty interface `IEventPayload` in order to constrain the different generic event types/contexts. In code, these contexts get constructed, populated, and passed to object methods and subscribers of events. The game event bus holds these subscriptions in a dictionary using the context's (struct's) type as a key, and a related delegate (method/function) as the value. In code, any script can subscribe to, unsubscribe from, or raise/trigger an event. The event bus will multicast all delegates connected to a specific event that gets raised.

Different game event types/contexts are defined and described in `GameEvents.cs`, along with their relating interfaces such as `IEventPayload`. Custom contexts and interfaces can be added and used for any kind of event without affecting the rest of the code. The `GameEventBus` class

## How To Use

All subscriptions and event triggers are handled in code.

### Writing a New Event Context

Simply create a new, public struct in `GameEvents.cs` that extends the `IEventPayload` interface, and include the variables that you need for the event.

```
// Example:

public struct HitContext : IEventPayload
{
    public GameObject Attacker;
    public GameObject Victim;
    public float BaseDamage;
}

```

### Creating a New Event Context Instance

A new event context must be made and populated with values before being passed as an argument to other methods. These structs do not need to be deallocated after use since they live on the stack, and they will be cleaned up when the method execution finishes and they go out of scope.

```
// Example:

HitContext context = new HitContext
{
    Attacker = gameObject,
    Victim = other.gameObject,
    BaseDamage = attackDamage
};
```

### Subscribing & Unsubscribing


To set up proper subscription and unsubscription in a script, add the corresponding method calls to a script's `OnEnable()` and `OnDisable()` methods. Handling unsubscribing is key to preventing memory leaks due to leftover/dead subscriptions left in the event table.

When calling the subscribe method, include the type (context) after the method name in order to replace the generic type T at runtime, and pass in a parameter of the method you want to be invoke when the event is raised.

Make sure the method you are subscribing to the event takes in the proper context struct as a parameter.

```
// Example:

private void OnEnable()
{
    GameEventBus.Subscribe<HitContext>(HandleHit);
}

private void OnDisable()
{
    GameEventBus.Unsubscribe<HitContext>(HandleHit);
}

private void HandleHit(HitContext context)
{
    ...
}
```

### Raising Events

Simply reference the `GameEventBus` class and call the `Raise()` method on it, passing in the context for the event you want to trigger. The event bus will automatically trigger all methods that have been subscribed to this event, and pass the context to them as an argument.

```
// Example:

GameEventBus.Raise(context);
```

## Other Related Interfaces

For events related to the hit-detection system (i.e. the collision between hurtboxes and hitboxes), there is special interface used for the related `MonoBehavior` scripts that will listen to hits received by hurtboxes: `IHitResponder`. This interface requires a method to be included in any class that extends from it: `HandleHit(HitContext context)`.

Moreover, when a hurtbox receives a hit from a hitbox, it will trigger the `HandleHit()` method in any script (who contains a class that extends the interface) that itself or its parent contains (`Hurtbox` uses `GetComponentsInParent<IHitResponder>()`). 

```
// Example From Hurtbox.cs:

... 
private IHitResponder[] responders;

private void Awake()
{
    responders = GetComponentsInParent<IHitResponder>();
}

public void ReceiveHit(HitContext context)
{

    foreach (var responder in responders)
    {
        responder.HandleHit(context);
    }
}

// Example From HealthComponent.cs

public class HealthComponent : MonoBehaviour, IHitResponder

...

[SerializeField] private float _maxHealth;
private float _currentHealth;

private void Awake() => _currentHealth = _maxHealth;

public void HandleHit(HitContext context)
{
    ...
}
```


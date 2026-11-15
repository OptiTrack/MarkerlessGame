using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

public class Hurtbox : MonoBehaviour
{
    public float damageMultiplier = 1f;

    private IHitResponder[] _responders;

    private void Awake()
    {
        // Cache all components on this object (or parent) that can respond to hits
        _responders = GetComponentsInParent<IHitResponder>();
    }

    public void InitHitResponders()
    {
        _responders = GetComponentsInParent<IHitResponder>();
    }

    public void RecieveHit(HitContext context)
    {
        context.baseDamage *= damageMultiplier;

        // Route the hit context to every registered responder
        foreach (var responder in _responders)
        {
            responder.HandleHit(context);
        }
    }
}

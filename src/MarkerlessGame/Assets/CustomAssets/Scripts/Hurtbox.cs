using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

public class Hurtbox : MonoBehaviour
{
    [SerializeField] private float _damageMultiplier = 1f;

    private IHitResponder[] responders;

    private void Awake()
    {
        // Cache all components on this object (or parent) that can respond to hits
        responders = GetComponentsInParent<IHitResponder>();
    }

    public void RecieveHit(HitContext context)
    {
        context.BaseDamage *= _damageMultiplier;

        // Route the hit context to every registered responder
        foreach (var responder in responders)
        {
            responder.HandleHit(context);
        }
    }
}

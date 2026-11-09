using UnityEngine;
using UnityEngine.Events;

public class PhysicalButton : MonoBehaviour, IHitResponder
{
    public UnityEvent PhysicalButtonPressed;

    public void OnPress()
    {
        Debug.Log("Button Pressed!");
        PhysicalButtonPressed.Invoke();
    }

    public void HandleHit(HitContext context)
    {
        OnPress();
    }
}

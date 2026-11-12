using UnityEngine;
using UnityEngine.Events;

public class PhysicalButton : MonoBehaviour, IHitResponder
{
    public UnityEvent physicalButtonPressed;

    public void OnPress()
    {
        Debug.Log("Button Pressed!");
        physicalButtonPressed.Invoke();
    }

    public void HandleHit(HitContext context)
    {
        OnPress();
    }
}

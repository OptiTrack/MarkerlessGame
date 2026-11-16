using UnityEngine;

public class TestHit : MonoBehaviour
{

    public HealthComponent targetHealthComponent;

    private void FixedUpdate()
    {
        targetHealthComponent.SetHealth(targetHealthComponent.GetCurrentHealth() - 1);
    }
}

using UnityEngine;
using System.Collections;

public class TestHealthUI : MonoBehaviour
{

    public HealthComponent targetHealthComponent;

    private float damagePerHit = 10f;
    private float hitInterval = 1.5f;
    private int maxHits = 5;

    private void Start()
    {
        StartCoroutine(TestHealthChanges());
    }

    private IEnumerator TestHealthChanges()
    {
        for (int i = 0; i < maxHits; i++)
        {
            yield return new WaitForSeconds(hitInterval);

            if (targetHealthComponent == null)
            {
                Debug.LogWarning("TestHealthUI: No HealthComponent assigned.");
                yield break;
            }

            float currentHealth = targetHealthComponent.GetCurrentHealth();

            targetHealthComponent.SetHealth(
                Mathf.Max(0, currentHealth - damagePerHit)
            );
        }
    }
}


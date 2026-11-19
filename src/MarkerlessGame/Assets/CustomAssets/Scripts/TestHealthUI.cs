using UnityEngine;
using System.Collections;

public class TestHealthUI : MonoBehaviour
{

    [Header("Health Components")]
    [SerializeField] private HealthComponent playerHealth;
    [SerializeField] private HealthComponent enemyHealth;

    [Header("Test Settings")]
    [SerializeField] private bool testPlayerHealth = true;
    [SerializeField] private bool testEnemyHealth = true;

    [Header("Damage Settings")]
    [SerializeField] private float damagePerHit = 10f;
    [SerializeField] private float hitInterval = 1.5f;
    [SerializeField] private int maxHits = 5;

    private void Start()
    {
        if (testPlayerHealth)
        {
            if (playerHealth != null)
                StartCoroutine(TestHealthChanges(playerHealth));
            else
                Debug.LogWarning("TestHealthUI: Player HealthComponent not assigned.");
        }

        if (testEnemyHealth)
        {
            if (enemyHealth != null)
                StartCoroutine(TestHealthChanges(enemyHealth));
            else
                Debug.LogWarning("TestHealthUI: Enemy HealthComponent not assigned.");
        }
    }

    private IEnumerator TestHealthChanges(HealthComponent target)
    {
        for (int i = 0; i < maxHits; i++)
        {
            yield return new WaitForSeconds(hitInterval);

            if (target == null)
            {
                Debug.LogWarning("TestHealthUI: No HealthComponent assigned.");
                yield break;
            }

            float currentHealth = target.GetCurrentHealth();

            target.SetHealth(
                Mathf.Max(0, currentHealth - damagePerHit)
            );
        }
    }
}


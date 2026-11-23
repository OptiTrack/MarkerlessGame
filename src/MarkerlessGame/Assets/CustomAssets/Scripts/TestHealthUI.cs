using UnityEngine;
using System.Collections;

public class TestHealthUI : MonoBehaviour
{

    [Header("Health Components")]
    [SerializeField] private HealthComponent _playerHealth;
    [SerializeField] private HealthComponent _enemyHealth;

    [Header("Test Settings")]
    [SerializeField] private bool _testPlayerHealth = true;
    [SerializeField] private bool _testEnemyHealth = true;

    [Header("Damage Settings")]
    [SerializeField] private float _damagePerHit = 10f;
    [SerializeField] private float _hitInterval = 1.5f;
    [SerializeField] private int _maxHits = 5;

    private void Start()
    {
        if (_testPlayerHealth)
        {
            if (_playerHealth != null)
                StartCoroutine(TestHealthChanges(_playerHealth));
            else
                Debug.LogWarning("TestHealthUI: Player HealthComponent not assigned.");
        }

        if (_testEnemyHealth)
        {
            if (_enemyHealth != null)
                StartCoroutine(TestHealthChanges(_enemyHealth));
            else
                Debug.LogWarning("TestHealthUI: Enemy HealthComponent not assigned.");
        }
    }

    private IEnumerator TestHealthChanges(HealthComponent target)
    {
        for (int i = 0; i < _maxHits; i++)
        {
            yield return new WaitForSeconds(_hitInterval);

            if (target == null)
            {
                Debug.LogWarning("TestHealthUI: No HealthComponent assigned.");
                yield break;
            }

            float currentHealth = target.GetCurrentHealth();

            target.SetHealth(
                Mathf.Max(0, currentHealth - _damagePerHit)
            );
        }
    }
}



using System.Collections;
using UnityEngine;

public class TestScoreControllerUI : MonoBehaviour
{
    [Header("Score UI")]
    [SerializeField] private ScoreUIController _scoreUIController;

    [Header("Test Settings")]
    [SerializeField] private int _pointsPerUpdate = 250;
    [SerializeField] private float _updateInterval = 1.5f;
    [SerializeField] private int _maxUpdates = 10;

    private int _currentScore = 0;

    private void Start()
    {
        if (_scoreUIController == null)
        {
            Debug.LogWarning(
                "TestScoreUI: ScoreUIController not assigned."
            );
            return;
        }

        StartCoroutine(TestScoreChanges());
    }

    private IEnumerator TestScoreChanges()
    {
        // Initialize score display.
        _currentScore = 0;
        _scoreUIController.DisplayScore(_currentScore);

        for (int i = 0; i < _maxUpdates; i++)
        {
            yield return new WaitForSeconds(_updateInterval);

            _currentScore += _pointsPerUpdate;

            _scoreUIController.DisplayScore(_currentScore);

            Debug.Log($"TestScoreUI: Score increased to {_currentScore}");
        }

        Debug.Log("TestScoreUI: Score test finished!");
    }
}

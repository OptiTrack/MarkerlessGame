
using System.Collections;
using UnityEngine;

public class TestScoreControllerUI : MonoBehaviour
{
    [Header("Score UI")]
    [SerializeField] private ScoreUIController scoreUIController;

    [Header("Test Settings")]
    [SerializeField] private int pointsPerUpdate = 250;
    [SerializeField] private float updateInterval = 1.5f;
    [SerializeField] private int maxUpdates = 10;

    private int currentScore = 0;

    private void Start()
    {
        if (scoreUIController == null)
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
        currentScore = 0;
        scoreUIController.DisplayScore(currentScore);

        for (int i = 0; i < maxUpdates; i++)
        {
            yield return new WaitForSeconds(updateInterval);

            currentScore += pointsPerUpdate;

            scoreUIController.DisplayScore(currentScore);

            Debug.Log($"TestScoreUI: Score increased to {currentScore}");
        }

        Debug.Log("TestScoreUI: Score test finished!");
    }
}

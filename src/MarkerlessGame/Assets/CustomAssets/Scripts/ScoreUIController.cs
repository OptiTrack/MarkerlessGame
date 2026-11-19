
using UnityEngine;
using UnityEngine.UIElements;

public class ScoreUIController : MonoBehaviour
{
    private Label _scoreLabel;
    [SerializeField] private ScoreTracker _scoreTracker;
    private void OnEnable()
    {
        UIDocument uiDocument = GetComponent<UIDocument>();
        if (uiDocument == null)
        {
            Debug.LogError("ScoreUIController: UIDocument not found!");
            return;
        }

        VisualElement root = uiDocument.rootVisualElement;

        _scoreLabel = root.Q<Label>("Score_Label");

        if (_scoreLabel == null)
        {
            Debug.LogError("ScoreUIController: Could not find UI elements");
            return;
        }

        // Listen for score changes.
        GameEventBus.Subscribe<ScoreUpdateContext>(UpdateScoreUI);
        DisplayScore(_scoreTracker.GetCurrentScore());

    }
    private void OnDisable()
    {
        GameEventBus.Unsubscribe<ScoreUpdateContext>(UpdateScoreUI);
    }

    private void UpdateScoreUI(ScoreUpdateContext context)
    {
        DisplayScore(context.newScore);
    }

    public void DisplayScore(int currentScore)
    {
        if (_scoreLabel == null )
            return;

        int displayedScore = Mathf.Max(0, Mathf.RoundToInt(currentScore));
        _scoreLabel.text = displayedScore.ToString("0000");
    }
}
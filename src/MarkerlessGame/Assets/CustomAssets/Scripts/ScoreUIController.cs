
using UnityEngine;
using UnityEngine.UIElements;

public class ScoreUIController : MonoBehaviour
{
    private Label _scoreLabel;
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
        DisplayScore(0);

        // Listen for score changes.
        GameEventBus.Subscribe<ScoreUpdateContext>(UpdateScoreUI);

    }
    private void OnDisable()
    {
        GameEventBus.Unsubscribe<ScoreUpdateContext>(UpdateScoreUI);
    }

    private void UpdateScoreUI(ScoreUpdateContext context)
    {
        DisplayScore(context.newScore);
    }

    public void DisplayScore(float currentScore)
    {
        if (_scoreLabel == null )
            return;

        _scoreLabel.text = currentScore.ToString("0000");
    }
}
using UnityEngine;
using TMPro;

public class TestScoreUI : MonoBehaviour
{
    private TextMeshPro text;

    private void Awake()
    {
        text = GetComponent<TextMeshPro>();   
    }

    private void OnEnable()
    {
        GameEventBus.Subscribe<ScoreUpdateContext>(UpdateScoreUI);
    }

    private void OnDisable()
    {
        GameEventBus.Unsubscribe<ScoreUpdateContext>(UpdateScoreUI);
    }

    private void UpdateScoreUI(ScoreUpdateContext context)
    {
        text.text = ("Score: " + context.newScore);
    }
}

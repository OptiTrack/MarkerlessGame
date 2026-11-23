
using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;

public class HealthUIController : MonoBehaviour
{
    private float _maxBarWidth = 168f;
    private const int _maxHealth = 100;
    private const int _enemyHealthBarCount = 10;

    [Header("Enemy Health Bar Colors")]
    [SerializeField] private Color _enemyHealthActive = Color.red;
    [SerializeField] private Color _enemyHealthInactive = Color.gray;

    //UI Elements
    private Label _playerHealthText;
    private VisualElement _healthBarFill;
    private Label _enemyHealthText;
    private VisualElement _enemyHealthBarFill;

    private List<VisualElement> _enemyHealthBars = new List<VisualElement>();
    

    private void OnEnable()
    {
        UIDocument uiDocument = GetComponent<UIDocument>();

        if (uiDocument == null)
        {
            Debug.LogError("HealthUIController: UIDocument not found!");
            return;
        }

        VisualElement root = uiDocument.rootVisualElement;

        _playerHealthText = root.Q<Label>("Health_Value");
        _enemyHealthText = root.Q<Label>("Enemy_Health");
        _healthBarFill = root.Q<VisualElement>("Health_Bar_Fill");
        _enemyHealthBarFill = root.Q<VisualElement>("Health_Bar_Fill");
        for (int i = 0; i < _enemyHealthBarCount; i++)
        {
            var bar = root.Q<VisualElement>($"Enemy_Bar_{i}");
            if (bar != null)
                _enemyHealthBars.Add(bar);
        }

        if (_playerHealthText == null || _healthBarFill == null || _enemyHealthText == null|| _enemyHealthBarFill == null)
        {
            Debug.LogError("HealthUIController: Could not find UI elements");
            return;
        }


        // Listen for health changes.
        GameEventBus.Subscribe<HealthAdjustedContext>(UpdateHealthUI);
        Debug.Log("HealthUIController subscribed to HealthAdjustedContext");

    }
    private void OnDisable()
    {
        GameEventBus.Unsubscribe<HealthAdjustedContext>(UpdateHealthUI);
    }

    private void UpdateHealthUI(HealthAdjustedContext context)
    {

        string tag = context.entity.tag;
        float newHealth = Mathf.Clamp(context.healthComponent.GetCurrentHealth(), 0f, _maxHealth);
        float healthPercent = _maxHealth > 0
            ? newHealth / _maxHealth
            : 0f;

        if (tag == "Player"){
            DisplayPlayerHealth(healthPercent);
        } else if (tag == "Enemy"){
            DisplayEnemyHealth(healthPercent);
        }
    }

    public void DisplayPlayerHealth(float healthPercent)
    {
        Debug.Log("HealthUIController received health event");
        if (_playerHealthText == null || _healthBarFill == null)
            return;

        healthPercent = Mathf.Clamp01(healthPercent);
        int displayedHealth = Mathf.RoundToInt(healthPercent * 100f);
        _playerHealthText.text = displayedHealth.ToString("000");

        _healthBarFill.style.width = new Length(
            _maxBarWidth * healthPercent,
            LengthUnit.Pixel
        );
    }

    public void DisplayEnemyHealth(float healthPercent)
    {
        if (_enemyHealthText == null || _enemyHealthBars == null)
            return;

        healthPercent = Mathf.Clamp01(healthPercent);
        int displayedHealth = Mathf.RoundToInt(healthPercent * 100f);
        _enemyHealthText.text = displayedHealth.ToString("000");

        int filledBars = Mathf.RoundToInt(
            healthPercent * _enemyHealthBarCount
        );

        for (int i = 0; i < _enemyHealthBars.Count; i++)
        {
            _enemyHealthBars[i].style.backgroundColor =
                i < filledBars
                    ? _enemyHealthActive
                    : _enemyHealthInactive;
        }
    }
}
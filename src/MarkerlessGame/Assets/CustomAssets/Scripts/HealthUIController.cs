
using UnityEngine;
using UnityEngine.UIElements;

public class HealthUIController : MonoBehaviour
{
    [Header("UI Settings")]
    [SerializeField] private float _maxBarWidth = 168f;

    [Header("Health Component")]
    [SerializeField] private HealthComponent _playerHealth;


    private Label _healthValue;
    private Label _healthText;
    private VisualElement _healthBarFill;

    private void OnEnable()
    {
        UIDocument uiDocument = GetComponent<UIDocument>();

        if (uiDocument == null)
        {
            Debug.LogError("HealthUIController: UIDocument not found!");
            return;
        }

        VisualElement root = uiDocument.rootVisualElement;

        _healthValue = root.Q<Label>("Health_Value");
        _healthText = root.Q<Label>("Health_Text");
        _healthBarFill = root.Q<VisualElement>("Health_Bar_Fill");

        if (_healthValue == null || _healthText == null || _healthBarFill == null)
        {
            Debug.LogError("HealthController: Could not find UI elements!");
            return;
        }

        if (_playerHealth == null)
        {
            Debug.LogError("HealthController: Player HealthComponent not assigned!");
            return;
        }

        // Listen for health changes.
        _playerHealth.HealthChanged += UpdateHealthUI;

        // Display the current health immediately.
        RefreshHealthUI();
    }

    private void OnDisable()
    {
        if (_playerHealth != null)
        {
            _playerHealth.HealthChanged -= UpdateHealthUI;
        }
    }

    private void RefreshHealthUI()
    {
        UpdateHealthUI(
            _playerHealth.GetCurrentHealth(),
            _playerHealth.maxHealth
        );
    }

    private void UpdateHealthUI(float currentHealth, float maxHealth)
    {
        if (_healthValue == null || _healthText == null || _healthBarFill == null)
            return;

        int displayedHealth = Mathf.RoundToInt(currentHealth);
        int displayedMaxHealth = Mathf.RoundToInt(maxHealth);

        float healthPercent = maxHealth > 0
            ? currentHealth / maxHealth
            : 0f;

        _healthValue.text = displayedHealth.ToString("000");
        _healthText.text = $"HP {displayedHealth:000} / {displayedMaxHealth}";

        _healthBarFill.style.width = new Length(
            _maxBarWidth * healthPercent,
            LengthUnit.Pixel
        );
    }
}
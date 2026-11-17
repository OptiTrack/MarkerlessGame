
using UnityEngine;
using UnityEngine.UIElements;

public class HealthUIController : MonoBehaviour
{
    [Header("UI Settings")]
    private float _maxBarWidth = 168f;
    private float _maxHealth;

    [Header("Health Component")]
    [SerializeField] private HealthComponent _playerHealth;


    private Label _healthValue;
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
        _healthBarFill = root.Q<VisualElement>("Health_Bar_Fill");

        if (_healthValue == null || _healthBarFill == null)
        {
            Debug.LogError("HealthUIController: Could not find UI elements");
            return;
        }

        if (_playerHealth == null)
        {
            Debug.LogError("HealthUIController: Player HealthComponent not assigned");
            return;
        }
        _maxHealth = _playerHealth.maxHealth != 0 ? _playerHealth.maxHealth : 100;

        // Listen for health changes.
        GameEventBus.Subscribe<HealthAdjustedContext>(UpdateHealthUI);
        Debug.Log("HealthUIController subscribed to HealthAdjustedContext");

    }
    private void OnDisable()
    {
        // GameEventBus.Unsubscribe<HealthAdjustedContext>(UpdateHealthUI);
    }



    private void UpdateHealthUI(HealthAdjustedContext context)
    {
        if (context.healthComponent != _playerHealth)
            return;

        DisplayHealth(context.healthComponent.GetCurrentHealth());
    }

    public void DisplayHealth(float currentHealth)
    {
        Debug.Log("HealthUIController received health event");
        if (_healthValue == null || _healthBarFill == null)
            return;

        currentHealth = Mathf.Clamp(currentHealth, 0f, _maxHealth);

        int displayedHealth = Mathf.RoundToInt(currentHealth);
        int displayedMax = Mathf.RoundToInt(_maxHealth);

        float healthPercent = _maxHealth > 0
            ? currentHealth / _maxHealth
            : 0f;

        _healthValue.text = displayedHealth.ToString("000");

        _healthBarFill.style.width = new Length(
            _maxBarWidth * healthPercent,
            LengthUnit.Pixel
        );
    }
}
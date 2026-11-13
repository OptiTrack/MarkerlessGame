
using UnityEngine;
using UnityEngine.UIElements;

public class HealthController : MonoBehaviour
{
    [Header("UI Settings")]
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private float maxBarWidth = 168f;

    private Label healthValue;
    private Label healthText;
    private VisualElement healthBarFill;

    private void Awake()
    {
        UIDocument uiDocument = GetComponent<UIDocument>();
        VisualElement root = uiDocument.rootVisualElement;

        healthValue = root.Q<Label>("Health_Value");
        healthText = root.Q<Label>("Health_Text");
        healthBarFill = root.Q<VisualElement>("Health_Bar_Fill");
        Debug.Log("Health_Value found: " + (healthValue != null));
        Debug.Log("Health_Text found: " + (healthText != null));
        Debug.Log("Health_Bar_Fill found: " + (healthBarFill != null));
        SetHealth(100);

    }

    public void SetHealth(int currentHealth)
    {
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        float healthPercent = (float)currentHealth / maxHealth;

        // Update large health number
        healthValue.text = currentHealth.ToString("000");

        // Update small HP label
        healthText.text = $"HP {currentHealth:000} / {maxHealth}";

        // Update health bar
        healthBarFill.style.width =
            new Length(maxBarWidth * healthPercent, LengthUnit.Pixel);
    }
}

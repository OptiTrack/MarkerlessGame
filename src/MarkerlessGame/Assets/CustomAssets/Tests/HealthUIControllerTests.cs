using NUnit.Framework;
using UnityEngine;
using UnityEngine.UIElements;
using System.Reflection;
public class HealthUIControllerTests
{
    private GameObject testObject;
    private HealthUIController uiController;

    private Label healthValue;
    private VisualElement healthBarFill;

    private const float MaxBarWidth = 168f;

    [SetUp]
    public void Setup()
    {
        // Create the controller without activating its GameObject.
        testObject = new GameObject("TestHealthUI");
        testObject.SetActive(false);

        uiController = testObject.AddComponent<HealthUIController>();

        // Create mock UI elements.
        healthValue = new Label();
        healthBarFill = new VisualElement();

        // Assign UI elements to the controller.
        SetPrivateField("_healthValue", healthValue);
        SetPrivateField("_healthBarFill", healthBarFill);
        SetPrivateField("_maxBarWidth", MaxBarWidth);
        SetPrivateField("_maxHealth", 100);
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(testObject);
    }

    private void SetPrivateField(string fieldName, object value)
    {
        typeof(HealthUIController)
            .GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance)
            .SetValue(uiController, value);
    }

    [Test]
    public void DisplayHealth_UpdatesLabels()
    {
        uiController.DisplayHealth(65);

        Assert.AreEqual("065", healthValue.text);
    }

    [Test]
    public void DisplayHealth_UpdatesHealthBarWidth()
    {
        uiController.DisplayHealth(65);

        float expectedWidth = MaxBarWidth * 0.65f;

        Assert.AreEqual(
            expectedWidth,
            healthBarFill.style.width.value.value,
            0.01f
        );
    }

    [Test]
    public void DisplayHealth_AtZero_ShowsEmptyHealthBar()
    {
        uiController.DisplayHealth(0);

        Assert.AreEqual("000", healthValue.text);

        Assert.AreEqual(
            0f,
            healthBarFill.style.width.value.value,
            0.01f
        );
    }

    [Test]
    public void DisplayHealth_AtFullHealth_ShowsFullHealthBar()
    {
        uiController.DisplayHealth(100);

        Assert.AreEqual("100", healthValue.text);

        Assert.AreEqual(
            MaxBarWidth,
            healthBarFill.style.width.value.value,
            0.01f
        );
    }

    [Test]
    public void DisplayHealth_AboveMaximum_ClampsToFullHealth()
    {
        uiController.DisplayHealth(120);

        Assert.AreEqual("100", healthValue.text);

        Assert.AreEqual(
            MaxBarWidth,
            healthBarFill.style.width.value.value,
            0.01f
        );
    }
}
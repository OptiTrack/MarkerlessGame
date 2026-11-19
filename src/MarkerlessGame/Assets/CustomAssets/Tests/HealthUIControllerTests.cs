using NUnit.Framework;
using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;
using System.Reflection;

public class HealthUIControllerTests
{
    private GameObject testObject;
    private HealthUIController uiController;

    private Label playerHealthText;
    private Label enemyHealthText;

    private VisualElement healthBarFill;
    private List<VisualElement> enemyHealthBars;

    private const float MaxBarWidth = 168f;
    private const int EnemyBarCount = 10;

    private Color activeColor = Color.red;
    private Color inactiveColor = Color.gray;

    [SetUp]
    public void Setup()
    {
        testObject = new GameObject("TestHealthUI");
        testObject.SetActive(false);

        uiController = testObject.AddComponent<HealthUIController>();

        // Create mock player UI.
        playerHealthText = new Label();
        healthBarFill = new VisualElement();

        // Create mock enemy UI.
        enemyHealthText = new Label();
        enemyHealthBars = new List<VisualElement>();

        for (int i = 0; i < EnemyBarCount; i++)
        {
            enemyHealthBars.Add(new VisualElement());
        }

        // Inject mock UI elements into controller.
        SetPrivateField("_playerHealthText", playerHealthText);
        SetPrivateField("_healthBarFill", healthBarFill);

        SetPrivateField("_enemyHealthText", enemyHealthText);
        SetPrivateField("_enemyHealthBars", enemyHealthBars);

        SetPrivateField("_enemyHealthActive", activeColor);
        SetPrivateField("_enemyHealthInactive", inactiveColor);
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(testObject);
    }

    private void SetPrivateField(string fieldName, object value)
    {
        FieldInfo field = typeof(HealthUIController).GetField(
            fieldName,
            BindingFlags.NonPublic | BindingFlags.Instance
        );

        if (field == null)
        {
            Assert.Fail($"Could not find private field '{fieldName}' in HealthUIController.");
            return;
        }

        field.SetValue(uiController, value);
    }

    [Test]
    public void DisplayPlayerHealth_UpdatesHealthLabel()
    {
        uiController.DisplayPlayerHealth(0.65f);

        Assert.AreEqual("065", playerHealthText.text);
    }

    [Test]
    public void DisplayPlayerHealth_UpdatesHealthBarWidth()
    {
        uiController.DisplayPlayerHealth(0.65f);

        float expectedWidth = MaxBarWidth * 0.65f;

        Assert.AreEqual(
            expectedWidth,
            healthBarFill.style.width.value.value,
            0.01f
        );
    }

    [Test]
    public void DisplayPlayerHealth_AtZero_ShowsEmptyHealth()
    {
        uiController.DisplayPlayerHealth(0f);

        Assert.AreEqual("000", playerHealthText.text);

        Assert.AreEqual(
            0f,
            healthBarFill.style.width.value.value,
            0.01f
        );
    }

    [Test]
    public void DisplayPlayerHealth_AtFullHealth_ShowsFullHealth()
    {
        uiController.DisplayPlayerHealth(1f);

        Assert.AreEqual("100", playerHealthText.text);

        Assert.AreEqual(
            MaxBarWidth,
            healthBarFill.style.width.value.value,
            0.01f
        );
    }

    [Test]
    public void DisplayPlayerHealth_AboveMaximum_ClampsToFullHealth()
    {
        uiController.DisplayPlayerHealth(1.2f);

        Assert.AreEqual("100", playerHealthText.text);

        Assert.AreEqual(
            MaxBarWidth,
            healthBarFill.style.width.value.value,
            0.01f
        );
    }

    [Test]
    public void DisplayPlayerHealth_BelowZero_ClampsToZero()
    {
        uiController.DisplayPlayerHealth(-0.2f);

        Assert.AreEqual("000", playerHealthText.text);

        Assert.AreEqual(
            0f,
            healthBarFill.style.width.value.value,
            0.01f
        );
    }

    [Test]
    public void DisplayEnemyHealth_UpdatesHealthLabel()
    {
        uiController.DisplayEnemyHealth(0.65f);

        Assert.AreEqual("065", enemyHealthText.text);
    }

    [Test]
    public void DisplayEnemyHealth_ActivatesCorrectNumberOfBars()
    {
        uiController.DisplayEnemyHealth(0.7f);

        // 70% of 10 bars = 7 active bars.
        for (int i = 0; i < EnemyBarCount; i++)
        {
            Color expectedColor = i < 7
                ? activeColor
                : inactiveColor;

            Assert.AreEqual(
                expectedColor,
                enemyHealthBars[i].style.backgroundColor.value,
                $"Enemy health bar {i} has an incorrect color."
            );
        }
    }

    [Test]
    public void DisplayEnemyHealth_AtZero_DeactivatesAllBars()
    {
        uiController.DisplayEnemyHealth(0f);

        foreach (VisualElement bar in enemyHealthBars)
        {
            Assert.AreEqual(
                inactiveColor,
                bar.style.backgroundColor.value
            );
        }

        Assert.AreEqual("000", enemyHealthText.text);
    }

    [Test]
    public void DisplayEnemyHealth_AtFullHealth_ActivatesAllBars()
    {
        uiController.DisplayEnemyHealth(1f);

        foreach (VisualElement bar in enemyHealthBars)
        {
            Assert.AreEqual(
                activeColor,
                bar.style.backgroundColor.value
            );
        }

        Assert.AreEqual("100", enemyHealthText.text);
    }

    [Test]
    public void DisplayEnemyHealth_AboveMaximum_ClampsToFullHealth()
    {
        uiController.DisplayEnemyHealth(1.5f);

        Assert.AreEqual("100", enemyHealthText.text);
    }

    [Test]
    public void DisplayEnemyHealth_BelowZero_ClampsToZero()
    {
        uiController.DisplayEnemyHealth(-0.5f);

        Assert.AreEqual("000", enemyHealthText.text);
    }
}
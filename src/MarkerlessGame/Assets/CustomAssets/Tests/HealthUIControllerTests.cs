using NUnit.Framework;
using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;
using System.Reflection;

public class HealthUIControllerTests
{
    private GameObject _testObject;
    private HealthUIController _uiController;

    private Label _playerHealthText;
    private Label _enemyHealthText;

    private VisualElement _healthBarFill;
    private List<VisualElement> _enemyHealthBars;

    private const float _maxBarWidth = 168f;
    private const int _enemyBarCount = 10;

    private Color _activeColor = Color.red;
    private Color _inactiveColor = Color.gray;

    [SetUp]
    public void Setup()
    {
        _testObject = new GameObject("TestHealthUI");
        _testObject.SetActive(false);

        _uiController = _testObject.AddComponent<HealthUIController>();

        // Create mock player UI.
        _playerHealthText = new Label();
        _healthBarFill = new VisualElement();

        // Create mock enemy UI.
        _enemyHealthText = new Label();
        _enemyHealthBars = new List<VisualElement>();

        for (int i = 0; i < _enemyBarCount; i++)
        {
            _enemyHealthBars.Add(new VisualElement());
        }

        // Inject mock UI elements into controller.
        TestHelpers.SetPrivateField(_uiController, "_playerHealthText", _playerHealthText);
        TestHelpers.SetPrivateField(_uiController, "_healthBarFill", _healthBarFill);

        TestHelpers.SetPrivateField(_uiController, "_enemyHealthText", _enemyHealthText);
        TestHelpers.SetPrivateField(_uiController, "_enemyHealthBars", _enemyHealthBars);

        TestHelpers.SetPrivateField(_uiController, "_enemyHealthActive", _activeColor);
        TestHelpers.SetPrivateField(_uiController, "_enemyHealthInactive", _inactiveColor);
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(_testObject);
    }


    [Test]
    public void DisplayPlayerHealth_UpdatesHealthLabel()
    {
        _uiController.DisplayPlayerHealth(0.65f);

        Assert.AreEqual("065", _playerHealthText.text);
    }

    [Test]
    public void DisplayPlayerHealth_UpdatesHealthBarWidth()
    {
        _uiController.DisplayPlayerHealth(0.65f);

        float expectedWidth = _maxBarWidth * 0.65f;

        Assert.AreEqual(
            expectedWidth,
            _healthBarFill.style.width.value.value,
            0.01f
        );
    }

    [Test]
    public void DisplayPlayerHealth_AtZero_ShowsEmptyHealth()
    {
        _uiController.DisplayPlayerHealth(0f);

        Assert.AreEqual("000", _playerHealthText.text);

        Assert.AreEqual(
            0f,
            _healthBarFill.style.width.value.value,
            0.01f
        );
    }

    [Test]
    public void DisplayPlayerHealth_AtFullHealth_ShowsFullHealth()
    {
        _uiController.DisplayPlayerHealth(1f);

        Assert.AreEqual("100", _playerHealthText.text);

        Assert.AreEqual(
            _maxBarWidth,
            _healthBarFill.style.width.value.value,
            0.01f
        );
    }

    [Test]
    public void DisplayPlayerHealth_AboveMaximum_ClampsToFullHealth()
    {
        _uiController.DisplayPlayerHealth(1.2f);

        Assert.AreEqual("100", _playerHealthText.text);

        Assert.AreEqual(
            _maxBarWidth,
            _healthBarFill.style.width.value.value,
            0.01f
        );
    }

    [Test]
    public void DisplayPlayerHealth_BelowZero_ClampsToZero()
    {
        _uiController.DisplayPlayerHealth(-0.2f);

        Assert.AreEqual("000", _playerHealthText.text);

        Assert.AreEqual(
            0f,
            _healthBarFill.style.width.value.value,
            0.01f
        );
    }

    [Test]
    public void DisplayEnemyHealth_UpdatesHealthLabel()
    {
        _uiController.DisplayEnemyHealth(0.65f);

        Assert.AreEqual("065", _enemyHealthText.text);
    }

    [Test]
    public void DisplayEnemyHealth_ActivatesCorrectNumberOfBars()
    {
        _uiController.DisplayEnemyHealth(0.7f);

        // 70% of 10 bars = 7 active bars.
        for (int i = 0; i < _enemyBarCount; i++)
        {
            Color expectedColor = i < 7
                ? _activeColor
                : _inactiveColor;

            Assert.AreEqual(
                expectedColor,
                _enemyHealthBars[i].style.backgroundColor.value,
                $"Enemy health bar {i} has an incorrect color."
            );
        }
    }

    [Test]
    public void DisplayEnemyHealth_AtZero_DeactivatesAllBars()
    {
        _uiController.DisplayEnemyHealth(0f);

        foreach (VisualElement bar in _enemyHealthBars)
        {
            Assert.AreEqual(
                _inactiveColor,
                bar.style.backgroundColor.value
            );
        }

        Assert.AreEqual("000", _enemyHealthText.text);
    }

    [Test]
    public void DisplayEnemyHealth_AtFullHealth_ActivatesAllBars()
    {
        _uiController.DisplayEnemyHealth(1f);

        foreach (VisualElement bar in _enemyHealthBars)
        {
            Assert.AreEqual(
                _activeColor,
                bar.style.backgroundColor.value
            );
        }

        Assert.AreEqual("100", _enemyHealthText.text);
    }

    [Test]
    public void DisplayEnemyHealth_AboveMaximum_ClampsToFullHealth()
    {
        _uiController.DisplayEnemyHealth(1.5f);

        Assert.AreEqual("100", _enemyHealthText.text);
    }

    [Test]
    public void DisplayEnemyHealth_BelowZero_ClampsToZero()
    {
        _uiController.DisplayEnemyHealth(-0.5f);

        Assert.AreEqual("000", _enemyHealthText.text);
    }
}
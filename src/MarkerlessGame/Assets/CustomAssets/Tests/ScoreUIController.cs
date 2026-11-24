
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UIElements;

public class ScoreUIControllerTests
{
    private ScoreUIController _uiController;
    private Label _scoreLabel;

    [SetUp]
    public void Setup()
    {

        _uiController = new ScoreUIController();

        // Create a mock UI label.
        _scoreLabel = new Label();

        // Inject the label into the controller.
        TestHelpers.SetPrivateField(_uiController, "_scoreLabel", _scoreLabel);
    }

    [Test]
    public void DisplayScore_Zero_DisplaysFourZeros()
    {
        _uiController.DisplayScore(0);

        Assert.AreEqual("0000", _scoreLabel.text);
    }

    [Test]
    public void DisplayScore_NormalScore_DisplaysCorrectly()
    {
        _uiController.DisplayScore(250);

        Assert.AreEqual("0250", _scoreLabel.text);
    }

    [Test]
    public void DisplayScore_FourDigitScore_DisplaysCorrectly()
    {
        _uiController.DisplayScore(1250);

        Assert.AreEqual("1250", _scoreLabel.text);
    }

    [Test]
    public void DisplayScore_AboveFourDigits_DisplaysAllDigits()
    {
        _uiController.DisplayScore(12500);

        Assert.AreEqual("12500", _scoreLabel.text);
    }

    [Test]
    public void DisplayScore_NegativeScore_DisplaysZero()
    {
        _uiController.DisplayScore(-50);

        Assert.AreEqual("0000", _scoreLabel.text);
    }
}

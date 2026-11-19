
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UIElements;
using System.Reflection;

public class ScoreUIControllerTests
{
    private GameObject testObject;
    private ScoreUIController uiController;
    private Label scoreLabel;

    [SetUp]
    public void Setup()
    {

        uiController = new ScoreUIController();

        // Create a mock UI label.
        scoreLabel = new Label();

        // Inject the label into the controller.
        SetPrivateField("_scoreLabel", scoreLabel);
    }


    private void SetPrivateField(string fieldName, object value)
    {
        FieldInfo field = typeof(ScoreUIController).GetField(
            fieldName,
            BindingFlags.NonPublic | BindingFlags.Instance
        );

        Assert.IsNotNull(
            field,
            $"Could not find private field '{fieldName}'."
        );

        field.SetValue(uiController, value);
    }

    [Test]
    public void DisplayScore_Zero_DisplaysFourZeros()
    {
        uiController.DisplayScore(0);

        Assert.AreEqual("0000", scoreLabel.text);
    }

    [Test]
    public void DisplayScore_NormalScore_DisplaysCorrectly()
    {
        uiController.DisplayScore(250);

        Assert.AreEqual("0250", scoreLabel.text);
    }

    [Test]
    public void DisplayScore_FourDigitScore_DisplaysCorrectly()
    {
        uiController.DisplayScore(1250);

        Assert.AreEqual("1250", scoreLabel.text);
    }

    [Test]
    public void DisplayScore_AboveFourDigits_DisplaysAllDigits()
    {
        uiController.DisplayScore(12500);

        Assert.AreEqual("12500", scoreLabel.text);
    }

    [Test]
    public void DisplayScore_NegativeScore_DisplaysZero()
    {
        uiController.DisplayScore(-50);

        Assert.AreEqual("0000", scoreLabel.text);
    }
}

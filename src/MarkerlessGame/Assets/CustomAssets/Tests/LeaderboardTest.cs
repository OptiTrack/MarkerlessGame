using NUnit.Framework;
using System.IO;
using System.Linq;
using UnityEngine;

public class LeaderboardTest
{
    private const string RelativeTestDataPath = "CustomAssets/Tests/TestData/TestLeaderboardData.json";
    private const string RelativeBaseDataPath = "CustomAssets/Tests/TestData/BaseTestLeaderboardData.json";

    private GameObject managerObject;
    private LeaderboardManager leaderboardManager;
    private string fullTestDataSavePath;
    private string fullBaseDataSavePath;

    [SetUp]
    public void Setup()
    {
        // Create new absolute paths for the test data files that route through Assets directory
        fullTestDataSavePath = Path.Combine(new[] 
            { Application.dataPath }.Concat(RelativeTestDataPath.Split('/')).ToArray());
        fullBaseDataSavePath = Path.Combine(new[] 
            { Application.dataPath }.Concat(RelativeBaseDataPath.Split('/')).ToArray());

        // Creates the directory this file should live in if it doesn't already exist
        string testDir = Path.GetDirectoryName(fullTestDataSavePath);
        if (!string.IsNullOrEmpty(testDir) && !Directory.Exists(testDir))
        {
            Directory.CreateDirectory(testDir);
        }

        // Leaderboard manager game object setup
        managerObject = new GameObject("LeaderboardManager_Test");
        managerObject.SetActive(false);

        // Config setup for manager
        leaderboardManager = managerObject.AddComponent<LeaderboardManager>();
        leaderboardManager.editorSavePath = fullBaseDataSavePath;
        leaderboardManager.loadFromEditorPath = true;
        leaderboardManager.saveToEditorPath = true;
        leaderboardManager.loadFromLocalPath = false;
        leaderboardManager.saveToLocalPath = false;
        leaderboardManager.saveOnExit = false;
        leaderboardManager.saveOnAdd = false;

        managerObject.SetActive(true);
    }

    // Clean-up after tests
    [TearDown]
    public void TearDown()
    {
        if (managerObject != null)
        {
            Object.DestroyImmediate(managerObject);
            managerObject = null;
            leaderboardManager = null;
        }

        // Delete test file we created for saving data tests
        if (File.Exists(fullTestDataSavePath))
        {
            File.Delete(fullTestDataSavePath);
        }
    }

    [Test]
    public void Leaderboard_LoadsDataFromFile()
    {
        // Make sure the file we are loading from actually exists
        Assert.IsTrue(
            File.Exists(fullBaseDataSavePath), 
            $"Missing base data file at: {fullBaseDataSavePath}"
        );

        // Get the json leaderboard data from the base save file we loaded from
        string expectedJson = File.ReadAllText(fullBaseDataSavePath);
        LeaderboardData expectedData = JsonUtility.FromJson<LeaderboardData>(expectedJson);

        // Load the scores from the base save file and retrieve the data again from the manager
        leaderboardManager.LoadScores();
        LeaderboardData actualData = leaderboardManager.GetScores();

        // Ensure the data was actually retrieved and that the number of entries is equal
        Assert.NotNull(actualData, "Manager's Leaderboard data is null after LoadScores().");
        Assert.AreEqual(
            expectedData.entries.Count, 
            actualData.entries.Count, 
            "Entry count does not match expected."
        );

        // Call to function that will check for any discrepencies between both sets of data
        CompareEntries(expectedData, actualData);
    }

    [Test]
    public void Leaderboard_LoadsAndSavesDataToFile()
    {
        // Make sure the file we are loading from actually exists
        Assert.IsTrue(
            File.Exists(fullBaseDataSavePath), 
            $"Missing base data file at: {fullBaseDataSavePath}"
        );

        // Loads scores from base save file, then switches target path and saves to a new file
        leaderboardManager.LoadScores();
        leaderboardManager.editorSavePath = fullTestDataSavePath;
        leaderboardManager.SaveScores();

        Assert.IsTrue(File.Exists(fullTestDataSavePath), "Saved test file was not created.");

        // Get the json leaderboard data from the base save file we loaded from
        string expectedJson = File.ReadAllText(fullBaseDataSavePath);
        LeaderboardData expectedData = JsonUtility.FromJson<LeaderboardData>(expectedJson);

        // Get the json leaderboard data from the newly created save file we just saved data to
        string actualJson = File.ReadAllText(fullTestDataSavePath);
        LeaderboardData actualData = JsonUtility.FromJson<LeaderboardData>(actualJson);

        // Make sure new file was created and has the same number of entries
        Assert.NotNull(actualData);
        Assert.AreEqual(
            expectedData.entries.Count, 
            actualData.entries.Count, 
            "Saved entry count does not match."
        );
        
        // Call to function that will check for any discrepencies between both sets of data
        CompareEntries(expectedData, actualData);
    }

    [Test]
    public void Leaderboard_AddsNewEntry()
    {
        // Test data for new entry
        // Note: we typically created new LeaderboardEntry instances, but that is not needed for this test
        string newName = "Tester";
        int newScore = 1234;

        // Add the entry and retrieve the leaderboard data
        leaderboardManager.AddEntry(newName, newScore);
        LeaderboardData actualData = leaderboardManager.GetScores();

        // Make sure the leaderboard data includes an entry with the test name and score we defined
        Assert.IsTrue(
            actualData.entries.Exists(e => e.name == newName && e.score == newScore),
            "New entry was not added or its values do not match expected."
        );
    }

    private void CompareEntries(LeaderboardData expectedData, LeaderboardData actualData)
    {
        // Loops through each expected entry to compare against actual
        for (int i = 0; i < expectedData.entries.Count; i++)
        {
            // Make sure there is not an entry missing or of value null
            Assert.NotNull(actualData.entries[i], $"Entry is null at index {i}.");

            // Compare the names of the entries
            Assert.AreEqual(
                expectedData.entries[i].name,
                actualData.entries[i].name,
                $"Name mismatch at index {i}."
            );

            // Compare the scores of the entries
            Assert.AreEqual(
                expectedData.entries[i].score, actualData.entries[i].score,
                $"Score mismatch at index {i}."
            );

            // If entries end up containing more data, can easily add checks below
        }
    }
}

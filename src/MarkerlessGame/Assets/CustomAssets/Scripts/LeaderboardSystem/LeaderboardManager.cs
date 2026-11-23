using System.IO;
using System.Linq;
using UnityEngine;

public class LeaderboardManager : MonoBehaviour
{
    private string _savePath;
    private int _maxEntries = 100;
    public LeaderboardData leaderboard = new LeaderboardData();

    private void Awake()
    {
        _savePath = Path.Combine(Application.persistentDataPath, "leaderboardEntries.json");
        LoadScores();
        GetScores();
    }

    // Validation will be done outside of the manager by whatever collects the name and score and sends here
    public void AddEntry(string newName, int newScore)
    {
        leaderboard.entries.Add(new LeaderboardEntry { name = newName, score = newScore });
        leaderboard.entries = leaderboard.entries
            .OrderByDescending(e => e.score)
            .Take(_maxEntries)
            .ToList();

        SaveScores();
    }

    public LeaderboardData GetScores()
    {
        return leaderboard;
    }

    private void SaveScores()
    {
        string data = JsonUtility.ToJson(leaderboard, true);
        File.WriteAllText(_savePath, data);
    }

    private void LoadScores()
    {
        if (File.Exists(_savePath))
        {
            string data = File.ReadAllText(_savePath);
            leaderboard = JsonUtility.FromJson<LeaderboardData>(data);
        }
    }
}

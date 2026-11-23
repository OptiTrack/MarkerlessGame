using TMPro;
using UnityEngine;

public class TestLeaderboardUI : MonoBehaviour
{
    public TextMeshPro text;
    public LeaderboardManager leaderboard;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        string newText = "";
        LeaderboardData data = leaderboard.GetScores();
        foreach (LeaderboardEntry entry in data.entries)
        {
            newText += (entry.name + ": " + entry.score + "\n");
        }

        text.text = newText;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

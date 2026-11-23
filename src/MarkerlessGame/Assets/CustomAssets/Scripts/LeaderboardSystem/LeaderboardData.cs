using System.Collections.Generic;

[System.Serializable]
public class LeaderboardEntry
{
    public string name; // or initials?
    public int score;

    public override string ToString()
    {
        return $"{name}: {score}"; // Change format as needed or wanted for leaderboard UI
    }
}

[System.Serializable]
public class LeaderboardData
{
    public List<LeaderboardEntry> entries = new List<LeaderboardEntry>();
}


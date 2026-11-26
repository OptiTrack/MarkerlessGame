using System.IO;
using System.Linq;
using UnityEngine;

public class LeaderboardManager : MonoBehaviour
{
    public LeaderboardData leaderboard = new LeaderboardData();

    [Space(10)]

    [Header("Editor-Only File Config")]

    [Tooltip("This relative file path will only be used to load and save data to a file in the editor. " +
             "File path is relative to the `Assets/` directory.")]
    public string editorSavePath;

    [Tooltip("On start-up, load the scores from to the editor-only file path. Disabled in game builds. " +
             "Can only load from either local or editor path, with editor path taking presidence in editor.")]
    public bool loadFromEditorPath = false;

    [Tooltip("Save the data to the editor-only file path. Disabled in game builds.")]
    public bool saveToEditorPath = false;

    [Space(10)]

    [Tooltip("Load the data from the local file path. Defaults to true in game builds. " +
             "Can only load from either local or editor path, with editor path taking presidence in editor.")]
    public bool loadFromLocalPath = true;

    [Tooltip("Save the data to the local file path. Defaults to true in game builds.")]
    public bool saveToLocalPath = true;

    [Space(10)]
    [Header("General Config")]
    [Tooltip("Useful for testing, or when needing to save from one file path to another.")]
    public bool saveOnExit = false;
    [Tooltip("If true, will save changes made to the leaderboard to the designated file.")]
    public bool saveOnAdd = true;

    private string _savePath;
    private int _maxEntries = 100;

    private void Awake()
    {
        if (Application.isEditor)
        {
            if (editorSavePath != null)
            {
                // File path is absolute and routes through the Asset folder inside the project directory
                editorSavePath = Path.Combine(new[] { Application.dataPath }.Concat(editorSavePath.Split('/')).ToArray());
            }
        } else
        {
            loadFromEditorPath = false;
            saveToEditorPath = false;
            loadFromLocalPath = true;
            saveToLocalPath = true;
        }

        // Main save path that will be used in builds of the game so data can persist across play sessions
        // File is local to each machine, and can be found in AppData/LocalLow/[CompanyName]/[ProjectName]
        _savePath = Path.Combine(Application.persistentDataPath, "leaderboardEntries.json");

        LoadScores();
        GetScores();
    }

    private void OnDisable()
    {
        if (saveOnExit)
        {
            SaveScores();
        }
    }

    public void AddEntry(string newName, int newScore)
    {
        // Input/data validation will be done outside of the manager by whatever collects the input and sends here

        // Adds a new entry with the given name and score, then sorts the entries based on score whilst prioritizing
        // older entries in the case of ties
        leaderboard.entries.Add(new LeaderboardEntry { name = newName, score = newScore });
        leaderboard.entries = leaderboard.entries
            .OrderByDescending(e => e.score)
            .Take(_maxEntries)
            .ToList();

        if (saveOnAdd)
        {
            SaveScores();
        }
    }

    public LeaderboardData GetScores()
    {
        return leaderboard;
    }

    public void SaveScores()
    {
        // Scores will only save to one file or neither based on config
        if (Application.isEditor && saveToEditorPath)
        {
            string data = JsonUtility.ToJson(leaderboard, true);
            File.WriteAllText(editorSavePath, data);
        }
        else if (saveToLocalPath)
        {
            string data = JsonUtility.ToJson(leaderboard, true);
            File.WriteAllText(_savePath, data);
        }
    }

    public void LoadScores()
    {
        if (Application.isEditor && File.Exists(editorSavePath) && loadFromEditorPath)
        {
            Debug.Log("Loading data from editor file");
            string data = File.ReadAllText(editorSavePath);
            leaderboard = JsonUtility.FromJson<LeaderboardData>(data);
        }
        else if (File.Exists(_savePath) && loadFromLocalPath)
        {
            Debug.Log("Loading data from local file");
            string data = File.ReadAllText(_savePath);
            leaderboard = JsonUtility.FromJson<LeaderboardData>(data);
        }
        else
        {
            Debug.LogWarning("JSON file(s) cannot be found and data can't be loaded. Check config and file paths.");
        }
    }
}

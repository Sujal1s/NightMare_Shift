using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    private static string saveFilePath => Path.Combine(Application.persistentDataPath, "gamesave.json");
    
    // Save the game state
    public static void SaveGame(GameSaveData data)
    {
        
        
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(saveFilePath, json);
        Debug.Log("Game Saved to: " + saveFilePath);
    }

    // Load the game state
    public static GameSaveData LoadGame()
    {
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            GameSaveData data = JsonUtility.FromJson<GameSaveData>(json);
            Debug.Log("Game Loaded from: " + saveFilePath);
            return data;
        }
        Debug.LogWarning("No save file found. Returning new Game Save Data.");
        return new GameSaveData();
    }
}
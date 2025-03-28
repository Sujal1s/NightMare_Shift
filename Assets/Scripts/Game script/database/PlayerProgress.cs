using UnityEngine;

public class PlayerProgress : MonoBehaviour
{
    public int currentCheckpoint = 0;
    public int coins = 0;

    void Update()
    {
        // For demonstration, assume pressing S saves and pressing L loads
        if (Input.GetKeyDown(KeyCode.S))
        {
            Save();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            Load();
        }
    }

    public void Save()
    {
        GameSaveData data = new GameSaveData
        {
            checkpointID = currentCheckpoint,
            coinCount = coins
        };

        SaveManager.SaveGame(data);
    }

    public void Load()
    {
        GameSaveData data = SaveManager.LoadGame();
        currentCheckpoint = data.checkpointID;
        coins = data.coinCount;
    }
}
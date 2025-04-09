using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System.Linq;

public class SaveController : MonoBehaviour
{
    private  string savePath;
    private Chest[] chests;
    void Start()
    {   
        // C:\Users\UserName\AppData\LocalLow\DefaultCompany\ProjectName\save.json
        savePath = Path.Combine(Application.persistentDataPath, "save.json");
        chests = FindObjectsByType<Chest>(FindObjectsSortMode.None);

        // Wait for 0.1 seconds to ensure all objects are initialized
        // TODO: This should be done in a better way, but for now it works
        Invoke("LoadGame", 0.1f);
    }
    
    // Update is called once per frame
    public void SaveGame()
    {
        SaveData saveData = new SaveData
        {
            playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position,
            chestSaveData = GetChestsState()
        };

        string json = JsonUtility.ToJson(saveData, true);
        File.WriteAllText(savePath, json);
    }

    public void LoadGame()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            SaveData saveData = JsonUtility.FromJson<SaveData>(json);

            GameObject.FindGameObjectWithTag("Player").transform.position = saveData.playerPosition;
            
            LoadChestsState(saveData.chestSaveData);
        }
        else
        {   
            // First time loading the game, create a new save file
            SaveGame();
        }
    }

    private List<ChestSaveData> GetChestsState() {
        List<ChestSaveData> chestStates = new List<ChestSaveData>();
        foreach (Chest chest in chests)
        {
            ChestSaveData chestSaveData = new ChestSaveData
            {
                isOpened = chest.isOpened,
                chestID = chest.chestID
            };
            chestStates.Add(chestSaveData);
        }

        return chestStates;
    }

    private void LoadChestsState(List<ChestSaveData> chestStates) {
        foreach (Chest chest in chests)
        {   
            ChestSaveData chestSaveData = chestStates.FirstOrDefault(c => c.chestID == chest.chestID);
            if (chestSaveData != null)
            {
                chest.SetOpened(chestSaveData.isOpened);
            }
        }
    }
}

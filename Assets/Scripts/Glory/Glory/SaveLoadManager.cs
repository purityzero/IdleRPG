
using UnityEngine;

public class SaveLoadManager
{
    public static void SaveData<T>(string key, T data)
    {
        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString(key, json);
        PlayerPrefs.Save();
    }

    public static T LoadData<T>(string key)
    {
        if (!PlayerPrefs.HasKey(key)) return default;
        string json = PlayerPrefs.GetString(key);
        return JsonUtility.FromJson<T>(json);
    }
}

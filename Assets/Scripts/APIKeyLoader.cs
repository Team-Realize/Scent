using UnityEngine;

public class ApiKeyLoader : MonoBehaviour
{
    public string apiKey;

    void Awake()
    {
        TextAsset configFile = Resources.Load<TextAsset>("config");
        if (configFile != null)
        {
            var config = JsonUtility.FromJson<ConfigData>(configFile.text);
            apiKey = config.OpenAIKey;
        }
        else
        {
            Debug.LogError("Config file not found!");
        }
    }

    [System.Serializable]
    private class ConfigData
    {
        public string OpenAIKey;
    }
}
using UnityEngine;
using System;

public class Setting
{
    public string version;
    public float soundVolume;
    public bool enableTutorial;
    public int rate;
    public int highScore;
    public int star;

    public void updateHighScore(int newScore)
    {
        if (newScore > highScore)
        {
            highScore = newScore;
        }
    }
    public void updateStar(int _star)
    {
        star = Mathf.Clamp(star + _star, 0, Int32.MaxValue);
    }
    public Setting()
    {
        version = GameConstants.gameVersion;
        soundVolume = 1.0f;
        enableTutorial = true;
        rate = 0;
        highScore = 0;
        star = 0;
    }
}

public class GamePreferences : MonoBehaviour
{

    static GamePreferences _instance;
    public static GamePreferences Instance { get { return _instance; } }
    void Awake()
    {
        _instance = this;
    }
    /// <summary>
    /// Setting
    /// </summary>
    public Setting setting { get; set; }
    public Setting LoadSetting()
    {
        setting = SaveGameManager.loadData<Setting>(GameTags.settingDataKey);
        if (setting == null)
        {
            setting = new Setting();
            SaveSetting();
        }
        return setting;
    }
    public void SaveSetting()
    {
        SaveGameManager.saveData<Setting>(GameTags.settingDataKey, setting);
    }
}
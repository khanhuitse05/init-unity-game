using UnityEngine;
using System;
namespace Ping
{
    public class Setting
    {
        public string version;
        public float sfxVolume;
        public float musicVolume;
        public bool enableTutorial;
        public int rate;
        public int highScore;
        public int star;

        public void updateHighScore(int newScore)
        {
            if (newScore > highScore)
            {
                highScore = newScore;
                GamePreferences.SaveSetting();
            }
        }
        public void updateStar(int _star)
        {
            star = Mathf.Clamp(star + _star, 0, Int32.MaxValue);
        }
        public Setting()
        {
            version = GameConstants.gameVersion;
            musicVolume = 0.75f;
            sfxVolume = 0.75f;
            enableTutorial = true;
            rate = 0;
            highScore = 0;
            star = 0;
        }
    }

    public class GamePreferences : MonoBehaviour
    {
        /// <summary>
        /// Setting
        /// </summary>
        public static Setting setting { get; set; }
        public static Setting LoadSetting()
        {
            setting = SaveGameManager.loadData<Setting>(GameTags.settingDataKey);
            if (setting == null)
            {
                setting = new Setting();
                SaveSetting();
            }
            return setting;
        }
        public static void SaveSetting()
        {
            SaveGameManager.saveData<Setting>(GameTags.settingDataKey, setting);
        }
    }
}
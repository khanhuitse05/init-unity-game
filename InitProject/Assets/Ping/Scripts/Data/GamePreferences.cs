using UnityEngine;
using SimpleJSON;
using System;

    public class Profile
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
        public Profile()
        {
        }
        public void reset()
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
    static Profile _profile;
    public static Profile initProfile()
    {
        _profile = SaveGameManager.loadData<Profile>(GameTags.settingDataKey);
        if (_profile == null)
        {
            _profile = new Profile();
            _profile.reset();
            saveProfile();
        }
        return _profile;
    }
    public static Profile profile { get { return _profile; } }

    public static void saveProfile()
    {
        SaveGameManager.saveData<Profile>(GameTags.settingDataKey, _profile);
    }

    public static void submitScore(int _score)
    {
    }
   
}
using UnityEngine;
using System.Collections;

public class SettingButton : MonoBehaviour {

    public GameObject audioOn;
    public GameObject audioOff;
    void OnEnable()
    {
        Utils.setActive(audioOn, GamePreferences.profile.soundVolume > 0);
        Utils.setActive(audioOff, GamePreferences.profile.soundVolume == 0);
    }
    public void onBtnSettingsClick()
    {
        float value = 0;
        if (GamePreferences.profile.soundVolume != 0)
        {
            value = 0f;
            Utils.setActive(audioOn, false);
            Utils.setActive(audioOff, true);
        }
        else
        {
            value = 1.0f;
            Utils.setActive(audioOn, true);
            Utils.setActive(audioOff, false);
        }
        GamePreferences.profile.soundVolume = value;
        AudioManager.SetSFXVolume(value);
        GamePreferences.saveProfile();
    }
}

using UnityEngine;
using System.Collections;
namespace Ping
{
    public class SettingButton : MonoBehaviour
    {

        public GameObject audioOn;
        public GameObject audioOff;
        void OnEnable()
        {
            bool isOn = AudioManager.GetMusicVolume() > 0;
            Utils.setActive(audioOn, isOn);
            Utils.setActive(audioOff, !isOn);
        }
        public void onBtnSettingsClick()
        {
            float value = 0;
            if (AudioManager.GetMusicVolume() != 0)
            {
                value = 0f;
                Utils.setActive(audioOn, false);
                Utils.setActive(audioOff, true);
            }
            else
            {
                value = 0.75f;
                Utils.setActive(audioOn, true);
                Utils.setActive(audioOff, false);
            }
            AudioManager.SetSFXVolume(value);
            AudioManager.SetMusicVolume(value);
        }
    }
}
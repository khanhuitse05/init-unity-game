using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GSSetting : GSTemplateFade
{
    public static GSSetting Instance { get; private set; }
    protected override void Awake()
    {
        base.Awake();
        Instance = this;
    }
    protected override void init()
    {
        // Sound
        sliderMusicVolume.value = AudioManager.GetMusicVolume() * 100;
        sliderSFXVolume.value = AudioManager.GetSFXVolume() * 100;
        // Language
        string[] _knownLanguages = Localization.knownLanguages;
        List<string> _Languages = Utils.ConverArray<string>(_knownLanguages);
        dropLanguage.ClearOptions();
        dropLanguage.AddOptions(_Languages);
        Utils.SetTextDropDown(dropLanguage, Localization.language, false);
    }

    public override void onEnter()
    {
        base.onEnter();
    }
    public override void onResume()
    {
        base.onResume();
    }
    public override void onSuspend()
    {
        base.onSuspend();
    }
    public override void onExit()
    {
        base.onExit();
    }
    public override void onBackKey()
    {
    }

    //
    public Dropdown dropLanguage;
    public Slider sliderMusicVolume;
    public Slider sliderSFXVolume;

    public void OnMusicVolumeSlider()
    {
        if (Time.timeSinceLevelLoad > 0.5f)
            AudioManager.SetMusicVolume(sliderMusicVolume.value / 100);
    }
    public void OnSFXVolumeSlider()
    {
        if (Time.timeSinceLevelLoad > 0.5f)
            AudioManager.SetSFXVolume(sliderSFXVolume.value / 100);
    }
    public void OnLanguageDropdown()
    {
        Localization.language = dropLanguage.captionText.text;
    }
    public void OnClickFinish()
    {
        GameStatesManager.PopState(GSHome.Instance);
    }
}
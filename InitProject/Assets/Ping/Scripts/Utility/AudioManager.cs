using UnityEngine;

public enum SoundType
{
    Button,
    Coin
}

public class AudioManager : MonoSingleton<AudioManager>
{
    public AudioClip Background;

    public AudioClip audioButton;
    public AudioClip audioCoin;

    public AudioSource MusicAudioSource;
    public AudioSource SoundAudioSource;

    public bool isMusicOn { get; set; }
    public bool isSoundOn { get; set; }
    private bool canPlayMusic;

    public void Init()
    {
        MusicAudioSource.clip = Background;
        LoadMusicStatus();
        LoadSoundStatus();
    }

    public void ChangeMusicStatus()
    {
        isMusicOn = !isMusicOn;
        UpdateMusic();
        SaveMusicStatus();
    }

    public void ChangeSoundStatus()
    {
        isSoundOn = !isSoundOn;
        SaveSoundStatus();
    }

    public void PlayMusic()
    {
        canPlayMusic = true;
        UpdateMusic();
    }

    public void StopMusic()
    {
        canPlayMusic = false;
        UpdateMusic();
    }

    private void UpdateMusic()
    {
        if (isMusicOn)
        {
            if (canPlayMusic)
            {
                if (!MusicAudioSource.isPlaying)
                    MusicAudioSource.Play();
            }
            else
            {
                if (MusicAudioSource.isPlaying)
                    MusicAudioSource.Stop();
            }
        }
        else
        {
            if (MusicAudioSource.isPlaying)
                MusicAudioSource.Stop();
        }
    }

    public void PlaySound(SoundType soundType)
    {
        if (!isSoundOn)
            return;

        switch (soundType)
        {
            case SoundType.Button:
                SoundAudioSource.PlayOneShot(audioButton);
                break;

            case SoundType.Coin:
                SoundAudioSource.PlayOneShot(audioCoin);
                break;
        }
    }

    const string MusicStatusKey = "MusicStatusKey";
    const string SoundStatusKey = "SoundStatusKey";
    private void SaveMusicStatus()
    {
        PlayerPrefs.SetInt(MusicStatusKey, isMusicOn ? 1 : 0);
    }

    private void SaveSoundStatus()
    {
        PlayerPrefs.SetInt(SoundStatusKey, isSoundOn ? 1 : 0);
    }

    private void LoadMusicStatus()
    {
        int value = PlayerPrefs.GetInt(MusicStatusKey, 1);
        isMusicOn = value == 1;
    }

    private void LoadSoundStatus()
    {
        int value = PlayerPrefs.GetInt(SoundStatusKey, 1);
        isSoundOn = value == 1;
    }
}
using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    static private AudioObject[] audioObject;
    static public AudioManager audioManager;

    public float minFallOffRange = 10;

    public AudioClip[] musicList;
    public bool playMusic = true;
    public bool shuffle = false;
    private static AudioSource musicSource;

    static bool mute = false;

    public static bool Mute { get { return mute; } set { mute = value; } }

    private GameObject thisObj;

    void Awake()
    {
        thisObj = gameObject;
        if (playMusic)
        {
            GameObject musicObj = new GameObject();
            musicObj.name = "MusicSource";
            musicObj.transform.parent = this.transform;
            musicObj.transform.localPosition = Vector3.zero;
            musicSource = musicObj.AddComponent<AudioSource>();
            musicSource.loop = false;
            musicSource.playOnAwake = false;
            musicSource.ignoreListenerVolume = true;
        }

        audioObject = new AudioObject[10];
        for (int i = 0; i < audioObject.Length; i++)
        {
            GameObject obj = new GameObject();
            obj.name = "AudioSource";

            AudioSource src = obj.AddComponent<AudioSource>();
            src.playOnAwake = false;
            src.loop = false;
            src.minDistance = minFallOffRange;

            Transform t = obj.transform;
            t.parent = thisObj.transform;

            audioObject[i] = new AudioObject(src, t);
        }
        if (audioManager == null) audioManager = this;
    }

    static public void Init()
    {
        if (audioManager == null)
        {
            GameObject objParent = new GameObject();
            objParent.name = "AudioManager";
            audioManager = objParent.AddComponent<AudioManager>();
        }
    }

    //check for the next free, unused audioObject
    static private int GetUnusedAudioObject()
    {
        for (int i = 0; i < audioObject.Length; i++)
        {
            if (!audioObject[i].inUse)
            {
                return i;
            }
        }
        //if everything is used up, use item number zero
        return 0;
    }

    //this is a 3D sound that has to be played at a particular position following a particular event
    static public void PlaySound(AudioClip clip, Vector3 pos)
    {
        if (audioManager == null) Init();
        if (mute)
            return;
        int ID = GetUnusedAudioObject();
        audioObject[ID].inUse = true;
        audioObject[ID].thisT.position = pos;
        audioObject[ID].source.clip = clip;
        audioObject[ID].source.Play();

        float duration = audioObject[ID].source.clip.length;

        audioManager.StartCoroutine(audioManager.ClearAudioObject(ID, duration));
    }

    //this is a 3D sound that has to be played at a particular position following a particular event
    static public int PlaySound(AudioClip clip, Transform t, bool loop)
    {
        if (audioManager == null) Init();
        int ID = GetUnusedAudioObject();

        audioObject[ID].inUse = true;
        audioObject[ID].thisT.parent = t;
        audioObject[ID].thisT.localPosition = Vector3.zero;
        audioObject[ID].source.loop = loop;
        audioObject[ID].source.clip = clip;
        audioObject[ID].source.Play();

        if (!loop)
        {
        }

        return ID;
    }

    //this no position has been given, assume this is a 2D sound
    static public int PlaySound(AudioClip clip)
    {
        if (audioManager == null) Init();

        int ID = GetUnusedAudioObject();

        audioObject[ID].inUse = true;
        audioObject[ID].source.loop = false;
        audioObject[ID].source.clip = clip;
        audioObject[ID].source.Play();
        return ID;
    }

    static public int PlayMusic(AudioClip clip, bool loop)
    {
        if (clip == null)
            return -1;
        if (audioManager == null) Init();
        if (mute)
            return -1;

        if (musicSource != null)
        {
            musicSource.clip = clip;
            musicSource.loop = loop;
            musicSource.Play();
            return 1;
        }

        return -1;
    }

    static public void StopMusic()
    {
        if (musicSource != null)
        {
            musicSource.Stop();
        }
    }

    static public int PlayLoopedSound(AudioClip clip)
    {
        if (audioManager == null) Init();
        if (mute)
            return -1;

        int ID = GetUnusedAudioObject();

        audioObject[ID].inUse = true;

        audioObject[ID].source.clip = clip;
        audioObject[ID].source.loop = true;
        audioObject[ID].source.Play();

        return ID;
    }

    static public bool IsPlaying(int index)
    {
        if (index >= 0 && index < audioObject.Length)
        {
            return audioObject[index].source.isPlaying;
        }
        return false;
    }

    static public AudioObject GetAudioAt(int index)
    {
        if (index >= 0 && index < audioObject.Length)
        {
            return audioObject[index];
        }
        return null;
    }

    static public void StopAudioAt(int index)
    {
        if (index >= 0 && index < audioObject.Length)
        {
            if (audioObject[index].inUse)
            {
                audioObject[index].source.Stop();
                audioObject[index].inUse = false;
                audioObject[index].thisT.parent = audioManager.transform;
            }
        }
    }

    //function call to clear flag of an audioObject, indicate it's is free to be used again
    private IEnumerator ClearAudioObject(int ID, float duration)
    {
        yield return new WaitForSeconds(duration);

        audioObject[ID].inUse = false;
        audioObject[ID].thisT.parent = audioManager.transform;
    }

    static public void SetSFXVolume(float val)
    {
        AudioListener.volume = val;
        if (AudioManager.musicSource != null)
        {
            AudioManager.musicSource.volume = val;
        }
    }
}
[System.Serializable]
public class AudioObject
{
    public AudioSource source;
    public bool inUse = false;
    public Transform thisT;

    public AudioObject(AudioSource src, Transform t)
    {
        source = src;
        thisT = t;
    }
}

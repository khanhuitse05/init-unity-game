using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Ping
{
    public class AudioManager : MonoBehaviour
    {
        private List<AudioSource> audioSourceList = new List<AudioSource>();

        private static float musicVolume = .75f;
        private static float sfxVolume = .75f;

        public List<AudioClip> musicList;
        public bool playMusic = true;
        public bool shuffle = false;
        private int currentTrackID = 0;
        private AudioSource musicSource;


        private static AudioManager instance;

        public static void Init()
        {
            if (instance != null) return;
            GameObject obj = new GameObject();
            obj.name = "AudioManager";
            obj.AddComponent<AudioManager>();
        }

        void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
                return;
            }
            instance = this;

            DontDestroyOnLoad(gameObject);

            if (playMusic && musicList != null && musicList.Count > 0)
            {
                musicSource = gameObject.AddComponent<AudioSource>();
                musicSource.loop = false;
                musicSource.playOnAwake = false;
                musicSource.volume = musicVolume;

                musicSource.ignoreListenerVolume = true;

                if (shuffle) currentTrackID = Random.Range(0, musicList.Count);
                musicSource.clip = musicList[currentTrackID];
                musicSource.Play();
            }

            audioSourceList = new List<AudioSource>();
            for (int i = 0; i < 10; i++)
            {
                GameObject obj = new GameObject();
                obj.name = "AudioSource" + (i + 1);

                AudioSource src = obj.AddComponent<AudioSource>();
                src.playOnAwake = false;
                src.loop = false;

                obj.transform.parent = transform;
                obj.transform.localPosition = Vector3.zero;

                audioSourceList.Add(src);
            }

            AudioListener.volume = sfxVolume;
        }


        void Update()
        {
            if (musicSource != null && !musicSource.isPlaying)
            {
                if (shuffle) musicSource.clip = musicList[Random.Range(0, musicList.Count)];
                else
                {
                    musicSource.clip = musicList[currentTrackID];
                    currentTrackID += 1;
                    if (currentTrackID == musicList.Count) currentTrackID = 0;
                }
                musicSource.Play();
            }
        }

        void OnPlayClick() { _PlaySound(soundClick); }
        public AudioClip soundClick;

        //check for the next free, unused audioObject
        private int GetUnusedAudioSourceID()
        {
            for (int i = 0; i < audioSourceList.Count; i++)
            {
                if (!audioSourceList[i].isPlaying) return i;
            }
            return 0;   //if everything is used up, use item number zero
        }

        //call to play a specific clip
        public static void PlaySound(AudioClip clip)
        {
            if (instance == null) Init();
            instance._PlaySound(clip);
        }
        public void _PlaySound(AudioClip clip)
        {
            int ID = GetUnusedAudioSourceID();
            audioSourceList[ID].clip = clip;
            audioSourceList[ID].Play();
        }

        public static void SetSFXVolume(float val)
        {
            sfxVolume = val;
            AudioListener.volume = val;
        }

        public static void SetMusicVolume(float val)
        {
            musicVolume = val;
            if (instance && instance.musicSource) instance.musicSource.volume = val;
        }

        public static float GetMusicVolume() { return musicVolume; }
        public static float GetSFXVolume() { return sfxVolume; }
    }
}
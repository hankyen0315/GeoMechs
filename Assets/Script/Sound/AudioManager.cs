using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public Sound[] musicSounds, sfxSounds, PlayerSounds;
    public AudioSource musicSource, sfxSource, PlayerSource;

    public int PlayerMaxSimultaneousSounds = 15;
    public int EnemyMaxSimultaneousSounds = 30;
    private List<AudioSource> playingSounds = new List<AudioSource>();
    private List<AudioSource> EnemyPlayingSounds = new List<AudioSource>();

    private void Start()
    {
        PlayMusic("BGM(Prepare)");
    }

    private void Awake() 
    {
        if (Instance == null) 
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);

        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 在Update函數中移除已經停止播放的聲音
    private void Update()
    {
        playingSounds.RemoveAll(source => !source.isPlaying);
        EnemyPlayingSounds.RemoveAll(source => !source.isPlaying);
    }




    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound Not Found");
        }
        else 
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }

    public void PlaySFX(string name)
    {
        if (EnemyPlayingSounds.Count >= EnemyMaxSimultaneousSounds)
        {
            //Debug.Log("Error while trying to play" + name + ". Max simultaneous sfx sounds reached");
            return;
        }

        Sound s = Array.Find(sfxSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound Not Found: " + name);
        }
        else
        {
            sfxSource.PlayOneShot(s.clip, s.volumeScale);
            EnemyPlayingSounds.Add(sfxSource);
        }
    }

    public void PlayPlayerSounds(string name)
    {
        if (playingSounds.Count >= PlayerMaxSimultaneousSounds)
        {
            //Debug.Log("Error while trying to play" + name + ". Max simultaneous sfx sounds reached");
            return;
        }

        Sound s = Array.Find(PlayerSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound Not Found");
        }
        else
        {
            PlayerSource.PlayOneShot(s.clip, s.volumeScale);
            playingSounds.Add(PlayerSource);
        }
    }

}


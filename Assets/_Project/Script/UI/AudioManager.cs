using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioClip gameLoop;
    [SerializeField] AudioClip gameOver;
    [SerializeField] AudioClip attackClip;
    [SerializeField] AudioClip hitClip;
    [SerializeField] float musicMultiplier = 0.5f;
    [SerializeField] float sfxMultiplier = 4f;
    public static AudioManager instance;
    private float musicVolume = 0.3f;
    private float sfxVolume = 6f;

    string currentAudioScene = "";
    AudioSource audioSource;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true;

        musicVolume = PlayerPrefs.GetFloat("MasterVolume", 1f);
        audioSource.volume = musicVolume * musicMultiplier;
        sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 1f);


        SceneManager.sceneLoaded += OnSceneLoaded;
    }


    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }


    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        LoopManager(scene.name);
    }


    void LoopManager(string sceneName)
    {
        if (sceneName == currentAudioScene)
            return;

        currentAudioScene = sceneName;

        if (sceneName == "Game" || sceneName == "HomeScreen" || sceneName == "SettingScreen")
        {
            PlayAudio(gameLoop);
        }
        else if (sceneName == "GameOver")
        {
            PlayAudio(gameOver);
        }
    }


    void PlayAudio(AudioClip audioClip)
    {
        if (audioSource == null || audioClip == null)
            return;

        if (audioSource.clip == audioClip && audioSource.isPlaying)
            return;

        audioSource.clip = audioClip;
        audioSource.Play();
    }


    public void SetMasterVolume(float value)
    {
        musicVolume = value;
        PlayerPrefs.SetFloat("MasterVolume", value);
        PlayerPrefs.Save();
        UpdateAllAudioSources();
    }


    public float GetMasterVolume()
    {
        return musicVolume;
    }


    void UpdateAllAudioSources()
    {
        audioSource.volume = musicVolume * musicMultiplier;
    }


    public void PlayOnce(string nameClip)
    {
        AudioClip audioClip;

        switch (nameClip)
        {
            case "attackClip":
                audioClip = attackClip;
                audioSource.PlayOneShot(audioClip, sfxVolume);
                break;
            case "hitClip":
                audioClip = hitClip;
                audioSource.PlayOneShot(audioClip, sfxVolume * sfxMultiplier); // Los volumenes son muy bajos
                break;
            default:
                return;
        }
    }


    public void SetSFXVolume(float value)
    {
        sfxVolume = value;
        PlayerPrefs.SetFloat("SFXVolume", value);
        PlayerPrefs.Save();
    }

    public float GetSFXVolume()
    {
        return sfxVolume;
    }
}
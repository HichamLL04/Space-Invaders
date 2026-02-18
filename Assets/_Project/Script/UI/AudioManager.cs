using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioClip gameLoop;
    [SerializeField] AudioClip gameOver;

    public static AudioManager instance;
    private float masterVolume = 1f;
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

        masterVolume = PlayerPrefs.GetFloat("MasterVolume", 1f);
        audioSource.volume = masterVolume;

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
        masterVolume = value;
        PlayerPrefs.SetFloat("MasterVolume", value);
        PlayerPrefs.Save();
        UpdateAllAudioSources();
    }

    public float GetMasterVolume()
    {
        return masterVolume;
    }

    void UpdateAllAudioSources()
    {
        AudioSource[] allAudioSources = FindObjectsByType<AudioSource>(FindObjectsSortMode.None);
        foreach (AudioSource source in allAudioSources)
        {
            source.volume = masterVolume;
        }
    }
}
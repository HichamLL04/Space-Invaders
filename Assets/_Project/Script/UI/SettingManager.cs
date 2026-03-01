using UnityEngine;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour
{
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider;


    void Start()
    {
        if (musicSlider != null)
        {
            musicSlider.value = AudioManager.instance.GetMasterVolume();
            musicSlider.onValueChanged.AddListener(OnVolumeChanged);
        }

        if (sfxSlider != null)
        {
            sfxSlider.value = AudioManager.instance.GetSFXVolume();
            sfxSlider.onValueChanged.AddListener(OnSFXChanged);
        }
    }


    void OnVolumeChanged(float value)
    {
        AudioManager.instance.SetMasterVolume(value);
    }


    void OnSFXChanged(float value)
    {
        AudioManager.instance.SetSFXVolume(value);
    }


    void OnDestroy()
    {
        if (musicSlider != null)
            musicSlider.onValueChanged.RemoveListener(OnVolumeChanged);

        if (sfxSlider != null)
            sfxSlider.onValueChanged.RemoveListener(OnSFXChanged);
    }
}
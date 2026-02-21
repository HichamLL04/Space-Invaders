using UnityEngine;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;


    void Start()
    {
        if (volumeSlider != null)
        {
            volumeSlider.value = AudioManager.instance.GetMasterVolume();
            volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
        }
    }


    void OnVolumeChanged(float value)
    {
        AudioManager.instance.SetMasterVolume(value);
    }


    void OnDestroy()
    {
        if (volumeSlider != null)
        {
            volumeSlider.onValueChanged.RemoveListener(OnVolumeChanged);
        }
    }
}
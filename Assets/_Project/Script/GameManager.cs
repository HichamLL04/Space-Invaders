using UnityEngine;

public class GameManager : MonoBehaviour
{
    AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        PlayerPrefs.SetInt("Direccion", -1);
    }

    void Update()
    {
        
    }

    public void PlayOnce(AudioClip audioClip)
    {
        audioSource.PlayOneShot(audioClip);
    }
}

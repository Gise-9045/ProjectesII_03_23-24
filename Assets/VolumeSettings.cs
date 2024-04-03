using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider effectsSlider;
    
    [SerializeField] private TextMeshProUGUI musicTextNumber;
    [SerializeField] private TextMeshProUGUI effectsTextNumber;
    
    private void Start()
    {
        StartVolumeSettings();
    }

    public void StartVolumeSettings()
    {
        if (PlayerPrefs.HasKey("MusicVolume") && PlayerPrefs.HasKey("SFXVolume"))
        {
            LoadVolume();
            LoadEffects();
        }
        else
        {
            SetVolumeMusic();
            SetEffectsMusic();

        }
    }
    public void SetVolumeMusic()
    {
        float volume = musicSlider.value; 
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(volume)*20);
        musicTextNumber.text = Mathf.RoundToInt(volume).ToString() + " %";
        PlayerPrefs.SetFloat(("MusicVolume"), volume);
    }
    
    public void SetEffectsMusic()
    {
        float volume = effectsSlider.value; 
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(volume)*20);
        effectsTextNumber.text = Mathf.RoundToInt(volume).ToString() + " %";
        PlayerPrefs.SetFloat(("SFXVolume"),volume);
    }

    private void LoadVolume()
    {
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        SetVolumeMusic();
    }
    private void LoadEffects()
    {
        effectsSlider.value = PlayerPrefs.GetFloat("SFXVolume");
        SetEffectsMusic();
    }
    
  
}

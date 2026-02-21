using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    private static readonly string MASTER_VOLUME_PARAMETER = "MasterVolume";
    private static readonly string SFX_VOLUME_PARAMETER = "SFXVolume";

    [SerializeField] private AudioMixerGroup mainMixer;
    [SerializeField] private AudioMixerGroup sfxMixer;
    [SerializeField] private AudioMixerGroup musicMixer;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void MasterVolumeSliderChanged(float newValue)
    {
        float actualVolumeValue = (1 - newValue) * -40;
        mainMixer.audioMixer.SetFloat(MASTER_VOLUME_PARAMETER, actualVolumeValue);
    }

    public void SFXVolumeSliderChanged(float newValue)
    {
        float actualVolumeValue = (1 - newValue) * -40;
        mainMixer.audioMixer.SetFloat(SFX_VOLUME_PARAMETER, actualVolumeValue);
    }
}

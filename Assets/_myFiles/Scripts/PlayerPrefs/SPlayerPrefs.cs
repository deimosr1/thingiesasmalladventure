using UnityEngine;
using UnityEngine.Audio;

public class SPlayerPrefs : MonoBehaviour
{
    [SerializeField] private AudioMixer mAudioMixer;
    public float Volume;
    public int Resolution;
    public int Quality;

    public void GetRefs()
    {
        Volume = PlayerPrefs.GetFloat("Volume");
        Quality = PlayerPrefs.GetInt("Quality");
        Resolution = PlayerPrefs.GetInt("Resolution");
    }

    public void SaveVolume(float volume)
    {
        Debug.Log($"Saving volume to: {volume}");
        PlayerPrefs.SetFloat("Volume", volume);
    }

    public void SaveQuality(int quality)
    {
        PlayerPrefs.SetInt("Quality", quality);
    }

    public void SaveRes(int resolution)
    {
        PlayerPrefs.SetInt("Resolution", resolution);
    }
}

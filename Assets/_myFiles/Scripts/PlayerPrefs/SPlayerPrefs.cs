using UnityEngine;
using UnityEngine.Audio;

public class SPlayerPrefs : MonoBehaviour
{
    [SerializeField] private AudioMixer mAudioMixer;
    public float Volume;
    public int Resolution;
    public int Quality;

    private void Start()
    {
        PlayerPrefs.GetFloat("Volume", Volume);
        PlayerPrefs.GetInt("Quality", Quality);
        PlayerPrefs.GetInt("Resolution", Resolution);
    }

    public void SaveVolume(float volume)
    {
        PlayerPrefs.SetFloat("Volume", volume);
    }

    public void SaveQuality(int quality)
    {
        PlayerPrefs.SetInt("Quality", Quality);
    }

    public void SaveRes(int resolution)
    {
        PlayerPrefs.SetInt("Resolution", resolution);
    }
}

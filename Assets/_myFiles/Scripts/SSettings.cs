using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.Rendering;

public class SSettings : MonoBehaviour
{

    [SerializeField] private AudioMixer mAudioMixer;
    [SerializeField] private Resolution[] mResolutions;
    [SerializeField] private TMP_Dropdown mResDropdown;

    [SerializeField] private SPlayerPrefs mSettingsPrefs;

    private void Start()
    {
        //Getting Refs
        mSettingsPrefs = GetComponent<SPlayerPrefs>();
        mSettingsPrefs.GetRefs();
        //Establishing Player Prefs
        mAudioMixer.SetFloat("Volume", mSettingsPrefs.Volume);
        QualitySettings.SetQualityLevel(mSettingsPrefs.Quality);
        

       //Setup for Settings
       mResolutions = Screen.resolutions;
       mResDropdown.ClearOptions();
       SetRes(mSettingsPrefs.Resolution);

        List<string> options = new List<string>();

        int currentResIndex = 0;
        Debug.Log(mResolutions.Length);

        for (int i = 0; i < mResolutions.Length; i++)
        {
            string option = mResolutions[i].width + "x" + mResolutions[i].height;
            options.Add(option);

            if (mResolutions[i].width == Screen.currentResolution.width && mResolutions[i].height == Screen.currentResolution.height)
            {
                currentResIndex = i;
            }
        }
        mResDropdown.AddOptions(options);
        mResDropdown.value = currentResIndex;
        mResDropdown.RefreshShownValue();
    }
    public void SetVolume(float volume)
    {
        mAudioMixer.SetFloat("Volume", volume);
        //Debug.Log(volume);
        mSettingsPrefs.SaveVolume(volume);
    }

    public void SetFullScreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        mSettingsPrefs.SaveQuality(qualityIndex);
    }

    public void SetRes(int resIndex)
    {
        Resolution resolution = mResolutions[resIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        mSettingsPrefs.SaveRes(resIndex);
    }

    public void ResetPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
}

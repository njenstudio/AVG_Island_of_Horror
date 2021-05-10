using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour
{
    public static SettingManager instance;
    public static float TextSpeed = 60;
    public static float AutoPlaySpeed = 1;

    public static bool isClothAlpha = true;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this);
        Inint();
    }

    private void Inint()
    {
        SetMasterVolume(PlayerPrefs.GetFloat("MasterVolume", 0));
        SetMusicVolume(PlayerPrefs.GetFloat("MusicVolume", -40));
        SetSoundEffectVolume(PlayerPrefs.GetFloat("SoundEffectVolume", -40));
        SetTextSpeed(PlayerPrefs.GetFloat("TextSpeed", 60));
        SetAutoPlaySpeed(PlayerPrefs.GetFloat("AutoPlaySpeed", 1));
    }

    public void SetSettingToDefault()
    {
        SetMasterVolume(0);
        SetMusicVolume(-40);
        SetSoundEffectVolume(-40);
        SetTextSpeed(60);
        SetAutoPlaySpeed(1);
        isClothAlpha = true;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    #region TextControl
    /// <summary>
    /// TextControl Text Default Speed is 60 auto speed is 1
    /// </summary>
    public Slider[] TextSliders;
    public void SetTextSpeed(float speed)
    {
        TextSpeed = speed;
        TextSliders[0].value = speed;
        PlayerPrefs.SetFloat("TextSpeed", speed);
    }

    public void SetAutoPlaySpeed(float speed)
    {
        AutoPlaySpeed = speed;
        TextSliders[1].value = speed;
        PlayerPrefs.SetFloat("AutoPlaySpeed", speed);
    }
    #endregion
    #region ScreenControl
    /// <summary>
    /// ScreenControl 
    /// </summary>
    public void SetFullScreen()
    {
        Screen.SetResolution(1920, 1080, true);
    }

    public void SetWindow1920()
    {
        Screen.SetResolution(1920, 1080, false);
    }

    public void SetWindow1600()
    {
        Screen.SetResolution(1600, 900, false);
    }
    #endregion
    #region AudioSettingControl
    /// <summary>
    /// AudioSettingControl Default volume is -40dB
    /// </summary>
    public AudioMixer audioMixer;
    public Slider[] SoundSliders;
    public void SetMasterVolume(float volume)    
    {
        audioMixer.SetFloat("MasterVolume", volume);
        SoundSliders[0].value = volume;
        PlayerPrefs.SetFloat("MasterVolume", volume);
    }

    public void SetMusicVolume(float volume)   
    {
        audioMixer.SetFloat("MusicVolume", volume);
        SoundSliders[1].value = volume;
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    public void SetSoundEffectVolume(float volume)    
    {
        audioMixer.SetFloat("SoundEffectVolume", volume);
        SoundSliders[2].value = volume;
        PlayerPrefs.SetFloat("SoundEffectVolume", volume);
    }
    #endregion

}

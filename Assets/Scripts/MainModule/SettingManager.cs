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
        SetTextSpeed(PlayerPrefs.GetFloat("TextSpeed", 3));
        SetAutoPlaySpeed(PlayerPrefs.GetFloat("AutoPlaySpeed", 1));
    }

    public void SetSettingToDefault()
    {
        SetMasterVolume(0);
        SetMusicVolume(-40);
        SetSoundEffectVolume(-40);
        SetTextSpeed(3);
        SetAutoPlaySpeed(1);
        isClothAlpha = true;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    #region TextControl
    /// <summary>
    /// TextControl Text Default Speed is 120/5 X3 = 72 auto speed is 1
    /// </summary>
    public Slider[] TextSliders;
    public SliderGridView[] TextGridView;
    float MaxSpeed = 120;
    public void SetTextSpeed(float speed)
    {
        TextSpeed = speed*(MaxSpeed/ TextGridView[0].imageViews.Length);
        TextSliders[0].value = speed;
        TextGridView[0].Set(Mathf.RoundToInt(TextSliders[0].value));
        PlayerPrefs.SetFloat("TextSpeed", speed);
    }

    public void SetAutoPlaySpeed(float speed)
    {
        AutoPlaySpeed = speed;
        TextSliders[1].value = speed;
        TextGridView[1].Set(Mathf.RoundToInt(TextSliders[1].value));
        PlayerPrefs.SetFloat("AutoPlaySpeed", speed);
    }
    #endregion

    #region AudioSettingControl
    /// <summary>
    /// AudioSettingControl Default volume is -40dB
    /// </summary>
    public AudioMixer audioMixer;
    public Slider[] SoundSliders;
    public SliderGridView[] SoundGridView;
    public void SetMasterVolume(float volume)    
    {
        audioMixer.SetFloat("MasterVolume", volume);
        SoundSliders[0].value = volume;
        SoundGridView[0].Set(Mathf.RoundToInt(SoundSliders[0].value));
        PlayerPrefs.SetFloat("MasterVolume", volume);
    }

    public void SetMusicVolume(float volume)   
    {
        audioMixer.SetFloat("MusicVolume", volume);
        SoundSliders[1].value = volume;
        SoundGridView[1].Set(Mathf.RoundToInt(SoundSliders[1].value));
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    public void SetSoundEffectVolume(float volume)    
    {
        audioMixer.SetFloat("SoundEffectVolume", volume);
        SoundSliders[2].value = volume;
        SoundGridView[2].Set(Mathf.RoundToInt(SoundSliders[2].value));
        PlayerPrefs.SetFloat("SoundEffectVolume", volume);
    }
    #endregion

}

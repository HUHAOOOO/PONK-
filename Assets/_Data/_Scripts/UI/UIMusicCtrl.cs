using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIMusicCtrl : MonoBehaviour
{
    public Slider _musicSlider, _sfxSlider;
    public TextMeshProUGUI _musicTxt, _sfxTxt;


    private void Start()
    {
        _musicSlider.value = AudioManager.Instance.deffaultVolume;
        _sfxSlider.value = AudioManager.Instance.deffaultVolume;
    }

    public void ToggleMusic()
    {
        AudioManager.Instance.ToggleMusic();
        if (_musicTxt.fontStyle != FontStyles.Strikethrough)
            _musicTxt.fontStyle = FontStyles.Strikethrough;
        else _musicTxt.fontStyle = FontStyles.Normal;
    }
    public void ToggleSFX()
    {
        AudioManager.Instance.ToggleSFX();

        if (_sfxTxt.fontStyle != FontStyles.Strikethrough)
            _sfxTxt.fontStyle = FontStyles.Strikethrough;
        else _sfxTxt.fontStyle = FontStyles.Normal;
    }
    public void MusicVolume()
    {
        AudioManager.Instance.MusicVolume(_musicSlider.value);
    }

    public void SFXVolume()
    {
        AudioManager.Instance.SFXVolume(_sfxSlider.value);
    }
}

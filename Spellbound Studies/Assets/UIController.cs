using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIController : MonoBehaviour
{
    public Slider _MusicSlider, _SFXSlider;
    public void ToggleMusic()
    {
        AudioMgr.Instance.ToggleMusic();
    }

    public void ToggleSFX()
    {
        AudioMgr.Instance.ToggleSFX();
    }
    public void MusicVolume()
    {
        AudioMgr.Instance.MusicVolume(_MusicSlider.value);
    }

    public void SFXVolume()
    {
        AudioMgr.Instance.SFXVolume(_SFXSlider.value);
    }
}

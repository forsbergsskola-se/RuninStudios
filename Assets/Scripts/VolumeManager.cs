using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;

    public void changeVolume()
    {
        AudioListener.volume = volumeSlider.value;
        Save();
        if (!PlayerPrefs.HasKey("Volume"))
        {
            PlayerPrefs.SetFloat("Volume", 1);
            Load();
        }
        else
        {
            Load();
        }
       
    }

    private void Load()
    {
       volumeSlider.value = PlayerPrefs.GetFloat("Volume");
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("Volume", volumeSlider.value);
    }
    
}

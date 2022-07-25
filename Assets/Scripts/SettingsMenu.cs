using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public Slider volSlider;
    public GameObject mainMenu;
    private void Awake()
    {
        LoadPrefs();
    }
    public void ChangeVolume()
    {
        float val = volSlider.value / 100.0f;
        PlayerPrefs.SetFloat("Volume", val);
        PlayerPrefs.Save();
        AudioListener.volume = val;
    }

    void LoadPrefs()
    {
        volSlider.value = PlayerPrefs.GetFloat("Volume") * 100.0f;
    }

    public void LoadMainMenu()
    {
        mainMenu.SetActive(true);
        gameObject.SetActive(false);
    }
}

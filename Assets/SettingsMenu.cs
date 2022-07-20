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
        PlayerPrefs.SetInt("Volume", Mathf.RoundToInt(volSlider.value));
        PlayerPrefs.Save();
    }

    void LoadPrefs()
    {
        volSlider.value = PlayerPrefs.GetInt("Volume");
    }

    public void LoadMainMenu()
    {
        mainMenu.SetActive(true);
        gameObject.SetActive(false);
    }
}

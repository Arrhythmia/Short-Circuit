using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject settingsMenu;
    public GameObject leaderboardsMenu;
    private void Awake()
    {
        Application.targetFrameRate = 60;
    }
    private void Start()
    {
        AudioListener.volume = PlayerPrefs.GetFloat("Volume");
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }
    public void LoadSettingsMenu()
    {
        settingsMenu.SetActive(true);
        gameObject.SetActive(false);
    }
    public void LoadLeaderboardsMenu()
    {
        leaderboardsMenu.SetActive(true);
        gameObject.SetActive(false);
    }
}

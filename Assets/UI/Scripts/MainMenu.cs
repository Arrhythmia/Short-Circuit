using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject settingsMenu;
    public GameObject leaderboardsMenu;
    public GameObject creditsMenu;

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }
    private void Start()
    {
        AudioListener.volume = PlayerPrefs.GetFloat("Volume");
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
    public void LoadCreditsMenu()
    {
        creditsMenu.SetActive(true);
        gameObject.SetActive(false);
    }
}

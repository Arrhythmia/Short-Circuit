using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardsMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject localHighscore;
    TMPro.TextMeshProUGUI textComponent;
    private void Start()
    {
        textComponent = localHighscore.GetComponent<TMPro.TextMeshProUGUI>();
        textComponent.text += PlayerPrefs.GetInt("Highscore");
    }
    public void LoadMainMenu()
    {
        mainMenu.SetActive(true);
        gameObject.SetActive(false);
    }
}

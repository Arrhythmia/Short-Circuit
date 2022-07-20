using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardsMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public void LoadMainMenu()
    {
        mainMenu.SetActive(true);
        gameObject.SetActive(false);
    }
}

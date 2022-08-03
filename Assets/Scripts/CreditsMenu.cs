using UnityEngine;

public class CreditsMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public void OpenLink(string url)
    {
        Application.OpenURL(url);
    }
    public void LoadMainMenu()
    {
        mainMenu.SetActive(true);
        gameObject.SetActive(false);
    }
}

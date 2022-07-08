using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject totalScore;
    public GameObject restartButton;
    TextMeshProUGUI scoreText;

    public GameObject[] elementsToDisableOnDeath;
    private void Start()
    {
        scoreText = totalScore.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }
    public void OnDeath()
    {
        foreach (GameObject element in elementsToDisableOnDeath)
        {
            element.SetActive(false);
        }
        scoreText.text = Mathf.Round(gameObject.GetComponent<ScoreCounter>().score).ToString();
        totalScore.SetActive(true);
        restartButton.SetActive(true);
    }

    public void OnRestart()
    {
        GetComponent<TimeManager>().ResetSpeed();
        SceneManager.LoadScene(0);
    }
}

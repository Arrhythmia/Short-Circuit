using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int framerate = 60;
    public GameObject totalScore;
    public GameObject restartButton;
    TextMeshProUGUI scoreText;

    public GameObject[] elementsToDisableOnDeath;

    private void Awake()
    {
        Application.targetFrameRate = framerate;
    }
    private void Start()
    {
        scoreText = totalScore.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }
    public void Die()
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

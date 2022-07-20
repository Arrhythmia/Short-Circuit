using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int framerate = 60;
    public GameObject totalScore;
    TextMeshProUGUI scoreText;

    public GameObject nameField;
    TextMeshProUGUI nameTMP;

    public GameObject[] elementsToDisableOnDeath;
    public GameObject[] elementsToEnableOnDeath;

    public bool takeScreenshot = false;
    public bool hasTaken = false;
    private void Awake()
    {
        Application.targetFrameRate = framerate;
    }
    private void Start()
    {
        scoreText = totalScore.transform.GetComponent<TextMeshProUGUI>();
        nameTMP = nameField.GetComponent<TextMeshProUGUI>();
    }
    private void Update()
    {
        if (takeScreenshot && !hasTaken)
        {
            hasTaken = true;
            ScreenCapture.CaptureScreenshot("SomeLevel.png", 3);
        }
    }

    public void LoadMainMenu()
    {
        GetComponent<TimeManager>().ResetSpeed();
        SceneManager.LoadScene(0);
    }
    public void SubmitScore()
    {
        string playerName = nameTMP.text;
        int score;
        int.TryParse(scoreText.text, out score);
        if (score > PlayerPrefs.GetInt("Highscore"))
        {
            PlayerPrefs.SetInt("Highscore", score);
        }
        Debug.Log(playerName + " scored: " + score.ToString());
        HighScores.UploadScore(playerName, score);
    }
    public void Die()
    {
        foreach (GameObject element in elementsToDisableOnDeath)
        {
            element.SetActive(false);
        }
        scoreText.text = Mathf.Round(gameObject.GetComponent<ScoreCounter>().score).ToString();
        foreach (GameObject element in elementsToEnableOnDeath)
        {
            element.SetActive(true);
        }
    }
}

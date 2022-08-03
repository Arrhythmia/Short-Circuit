using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public int framerate = 60;
    public GameObject totalScore;
    TextMeshProUGUI scoreText;

    public GameObject errorText;

    public GameObject nameField;
    TextMeshProUGUI nameTMP;

    public GameObject[] disableOnDeath;
    public GameObject[] enableOnDeath;

    public GameObject pauseMenu;
    public GameObject pauseButton;

    /*public bool takeScreenshot = false;
    public bool hasTaken = false;*/

    TimeManager timeManager;

    public bool isPaused = false;


    public UnityEvent onCompleteCallback;
    private void Awake()
    {
        Application.targetFrameRate = framerate;
    }
    private void Start()
    {
        scoreText = totalScore.transform.GetComponent<TextMeshProUGUI>();
        nameTMP = nameField.GetComponent<TextMeshProUGUI>();
        timeManager = GetComponent<TimeManager>();
        timeManager.ResetSpeed();

        AudioListener.volume = PlayerPrefs.GetFloat("Volume"); //TEMPORARY
    }
    public void Pause()
    {
        isPaused = true;
        timeManager.Pause();
        pauseMenu.SetActive(true);
        LeanTween.scale(pauseButton, Vector3.zero, 0.1f).setDelay(0f).setOnComplete(OnCompletePauseAnim).setIgnoreTimeScale(true);
        Camera.main.GetComponent<CameraShake>().enabled = false;
    }
    public void Restart()
    {
        SceneManager.LoadScene(1);
    }
    public void OnCompletePauseAnim()
    {
        pauseButton.SetActive(false);
        //LeanTween.alphaCanvas(pauseMenu.transform.GetChild(0).GetComponent<CanvasGroup>(), 0f, 1f).setIgnoreTimeScale(true);
        LeanTween.moveLocalY(pauseMenu.transform.GetChild(0).gameObject, 109.4f, 0.05f).setIgnoreTimeScale(true);
        LeanTween.scale(pauseMenu.transform.GetChild(1).gameObject, Vector3.one, 0.1f).setDelay(0f).setOnComplete(OnCompleteResumeAnim).setIgnoreTimeScale(true);
    }
    public void OnCompleteResumeAnim()
    {
        LeanTween.scale(pauseMenu.transform.GetChild(2).gameObject, Vector3.one, 0.1f).setDelay(0f).setIgnoreTimeScale(true);
    }
    public void Resume()
    {
        isPaused = false;
        timeManager.ResetSpeed();
        pauseButton.SetActive(true);
        pauseMenu.SetActive(false);
        LeanTween.scale(pauseButton, Vector3.one, 0.1f).setDelay(0f).setIgnoreTimeScale(true);
        LeanTween.moveLocalY(pauseMenu.transform.GetChild(0).gameObject, 230.5f, 0.05f).setIgnoreTimeScale(true);
        LeanTween.scale(pauseMenu.transform.GetChild(1).gameObject, Vector3.zero, 0.1f).setDelay(0f).setIgnoreTimeScale(true);
        LeanTween.scale(pauseMenu.transform.GetChild(2).gameObject, Vector3.zero, 0.1f).setDelay(0f).setIgnoreTimeScale(true);
    }
    private void Update()
    {
        /*if (takeScreenshot && !hasTaken)
        {
            hasTaken = true;
            ScreenCapture.CaptureScreenshot("SomeLevel.png", 3);
        }*/
        errorText.SetActive(HighScores.uploadAttempted && HighScores.uploadError);

        if (HighScores.uploadAttempted && !HighScores.uploadError)
        {
            UploadSuccessful();
        }
    }
    void UploadSuccessful()
    {
        timeManager.ResetSpeed();
        SceneManager.LoadScene(0);
    }
    public void LoadMainMenu()
    {
        GetComponent<TimeManager>().ResetSpeed();
        SceneManager.LoadScene(0);
    }
    public void UploadScore()
    {
        string playerName = nameTMP.text;
        int score;
        int.TryParse(scoreText.text, out score);
        HighScores.UploadScore(playerName, score);
    }
    void SaveLocalScore()
    {
        int score;
        int.TryParse(scoreText.text, out score);
        if (score > PlayerPrefs.GetInt("Highscore"))
        {
            PlayerPrefs.SetInt("Highscore", score);
        }
    }
    public void Die()
    {
        isPaused = true;
        foreach (GameObject element in disableOnDeath)
        {
            element.SetActive(false);
        }
        scoreText.text = Mathf.Round(gameObject.GetComponent<ScoreCounter>().score).ToString();
        foreach (GameObject element in enableOnDeath)
        {
            element.SetActive(true);
        }
        SaveLocalScore();
    }
}

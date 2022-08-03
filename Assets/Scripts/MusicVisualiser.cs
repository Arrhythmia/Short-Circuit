using System.Collections;
using UnityEngine;
public class MusicVisualiser : MonoBehaviour
{
    public AudioSource bgMusicAudioSource;

    public float duration;
    public float magnitude;
    public float threshold = 9f;
    public float multiplier = 30f;
    public float lerpValue;

    Material mat;
    Material mat2;
    public Renderer[] rendererArray;
    GameManager gameManager;

    float vol;
    void Start()
	{
        gameManager = GetComponent<GameManager>();

        mat = rendererArray[0].material;
        mat2 = rendererArray[1].material;
        originalCol = mat.GetColor("_EmissionColor");

        vol = PlayerPrefs.GetFloat("Volume");
    }



	void FixedUpdate()
	{
        float[] spectrumData = new float[256];
        bgMusicAudioSource.GetSpectrumData(spectrumData, 0, FFTWindow.Rectangular);
		for (int i = 0; i < spectrumData.Length; i++)
        {
			float temp = spectrumData[i] * multiplier * 1 / vol;
			if (temp >= threshold)
            {
                if (!gameManager.isPaused)
                {
                    StartCoroutine(AdjustEmission(duration, magnitude));
                }
            }
        }
	}

    Color originalCol;
    public IEnumerator AdjustEmission(float duration, float magnitude)
    {
        float elapsed = 0.0f;
        Color resultColor = originalCol + originalCol * magnitude;
        while (elapsed < duration)
        {
            mat.SetColor("_EmissionColor", Color.Lerp(mat.GetColor("_EmissionColor"), resultColor, lerpValue));
            mat2.SetColor("_EmissionColor", Color.Lerp(mat.GetColor("_EmissionColor"), resultColor, lerpValue));

            elapsed += Time.deltaTime;

            yield return null;
        }
        float elapsed2 = 0.0f;
        while (elapsed2 < duration)
        {
            mat.SetColor("_EmissionColor", Color.Lerp(mat.GetColor("_EmissionColor"), originalCol, lerpValue));
            mat2.SetColor("_EmissionColor", Color.Lerp(mat.GetColor("_EmissionColor"), originalCol, lerpValue));

            elapsed2 += Time.deltaTime;

            yield return null;
        }
    }

    public void ResetEmission()
    {
        StopAllCoroutines();
        mat.SetColor("_EmissionColor", originalCol);
        mat2.SetColor("_EmissionColor", originalCol);
    }
}

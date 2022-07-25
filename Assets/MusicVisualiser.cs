using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class MusicVisualiser : MonoBehaviour
{
	Camera camera;
    float originalFov;
    Vector3 originalPos;
    public float duration;
    public float magnitude;
    public float targetFov;
    public float threshold = 1.5f;
    public float multiplier = 30f;

    Volume volume;
    ChromaticAberration chromatic;
    PaniniProjection panini;

    float defaultDistance;
    void Start()
	{
		camera = Camera.main;
        originalFov = camera.fieldOfView;
        originalPos = camera.transform.position;

        volume = camera.GetComponent<Volume>();
        volume.profile.TryGet<PaniniProjection>(out panini);
        defaultDistance = panini.distance.value;
    }
    int count = 0;
	// Update is called once per frame
	void FixedUpdate()
	{
		float[] spectrumData = new float[256];
		AudioListener.GetSpectrumData(spectrumData, 0, FFTWindow.Rectangular);

		for (int i = 0; i < spectrumData.Length; i++)
        {
			float temp = spectrumData[i] * multiplier;
			if (temp >= threshold)
            {
                Debug.Log(count++);
                if (!running)
                    StartCoroutine(Shake(duration, magnitude));
            }
        }
	}
    bool running = false;
    public IEnumerator Shake(float duration, float magnitude)
    {
        running = true;
        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            panini.distance.value = Mathf.Lerp(panini.distance.value, 1f, magnitude);

            elapsed += Time.deltaTime;

            yield return null;
        }

        panini.distance.value = defaultDistance;
        running = false;
    }
}

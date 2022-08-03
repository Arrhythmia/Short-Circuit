using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class FadeInScene : MonoBehaviour
{
    public float duration;
    Image image;
    private void Start()
    {
        image = GetComponent<Image>();
        FadeIn();
    }

    public void FadeIn()
    {
        StartCoroutine(FadeIn(duration));
    }
    IEnumerator FadeIn(float duration)
    {
        while (image.color.a > 0)
        {
            image.color = Color.Lerp(image.color, new Color(0f, 0f, 0f, 0f), duration * Time.deltaTime);
            yield return null;
        }
    }
}

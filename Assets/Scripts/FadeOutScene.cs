using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class FadeOutScene : MonoBehaviour
{
    public float duration;
    Image image;
    private void Start()
    {
        image = GetComponent<Image>();
    }

    public void FadeOut()
    {
        StartCoroutine(FadeOut(duration));
    }
    IEnumerator FadeOut(float duration)
    {
        while (image.color.a < 1f)
        {
            image.color = Color.Lerp(image.color, new Color(0f, 0f, 0f, 1f), duration * Time.deltaTime);
            yield return null;
        }
    }
}

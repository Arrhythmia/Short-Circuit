using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    public Image image;
    public void LoadLevel(int sceneIndex)
    {
        image.raycastTarget = true;
        StartCoroutine(LoadAsync(sceneIndex));
    }

    IEnumerator LoadAsync(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        while (!operation.isDone)
        {
            //Debug.Log(operation.progress);

            yield return null;
        }
    }
}

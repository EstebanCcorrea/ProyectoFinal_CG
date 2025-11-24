using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeManager : MonoBehaviour
{
    public static FadeManager Instance;

    public Image fadeImage;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public IEnumerator FadeOut(float duration)
    {
        float t = 0;
        Color c = fadeImage.color;

        while (t < duration)
        {
            t += Time.deltaTime;
            c.a = Mathf.Lerp(0f, 1f, t / duration);
            fadeImage.color = c;
            yield return null;
        }

        // Asegurar alpha final
        c.a = 1f;
        fadeImage.color = c;
    }


    public IEnumerator FadeIn(float duration)
    {
        float t = 0;
        Color c = fadeImage.color;

        while (t < duration)
        {
            t += Time.deltaTime;
            c.a = Mathf.Lerp(1f, 0f, t / duration);
            fadeImage.color = c;
            yield return null;
        }

        // Asegurar alpha final
        c.a = 0f;
        fadeImage.color = c;
    }
}

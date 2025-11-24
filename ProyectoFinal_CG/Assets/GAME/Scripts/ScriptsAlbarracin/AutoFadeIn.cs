using UnityEngine;

public class AutoFadeIn : MonoBehaviour
{
    public float duration = 1f;

    private void Start()
    {
        if (FadeManager.Instance != null)
        {
            StartCoroutine(FadeManager.Instance.FadeIn(duration));
        }
        else
        {
            Debug.LogWarning("No hay FadeManager en la escena.");
        }
    }
}

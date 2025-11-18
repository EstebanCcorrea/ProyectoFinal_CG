using UnityEngine;

public class EmissionPulse : MonoBehaviour
{
    public Color emissionColor = Color.red;
    public float pulseSpeed = 2f;
    public float intensityMin = 0.2f;
    public float intensityMax = 2f;

    private Material mat;

    void Start()
    {
        Renderer renderer = GetComponentInChildren<Renderer>();
        if (renderer != null)
        {
            mat = renderer.material;
            mat.EnableKeyword("_EMISSION");
        }
        else
        {
            Debug.LogError("No se encontró Renderer en el objeto ni en sus hijos.");
        }
    }

    void Update()
    {
        if (mat == null) return;

        float pulse = Mathf.Lerp(intensityMin, intensityMax, (Mathf.Sin(Time.time * pulseSpeed) + 1f) / 2f);
        Color finalColor = emissionColor * pulse;
        mat.SetColor("_EmissionColor", finalColor);
    }
}

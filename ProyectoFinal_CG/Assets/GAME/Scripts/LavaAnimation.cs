using UnityEngine;

public class LavaAnimation : MonoBehaviour
{
    [Header("Velocidad del flujo")]
    public float scrollX= 0.25f;
    public float scrollY = 0.1f;

    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void Update()
    {

        float offsetX = Time.time * scrollX;
        float offsetY = Time.time * scrollY;
        rend.material.SetTextureOffset("_BaseMap", new Vector2(offsetX, offsetY));


        if (rend.material.IsKeywordEnabled("_EMISSION"))
        {
            rend.material.SetTextureOffset("_EmissionMap", new Vector2(offsetX, offsetY));
        }
    }
}

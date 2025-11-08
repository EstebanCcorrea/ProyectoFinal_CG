using UnityEngine;

public class LavaAnimation : MonoBehaviour
{
    [Header("Velocidad del flujo")]
    public float scrollSpeed = 0.2f;
    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void Update()
    {

        float offset = Time.time * scrollSpeed;
        Vector2 newOffset = new Vector2(offset, 0); // movimiento solo en X

       
        if (rend.material.IsKeywordEnabled("_EMISSION"))
        {
            rend.material.mainTextureOffset = newOffset;
        }
    }
}

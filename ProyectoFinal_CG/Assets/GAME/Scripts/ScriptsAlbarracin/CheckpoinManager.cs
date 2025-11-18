using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager Instance;
    private Vector3 lastCheckpointPos; // <-- ESTA VARIABLE ES LA IMPORTANTE

    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void SetCheckpoint(Vector3 pos)
    {
        lastCheckpointPos = pos;
        Debug.Log("[CheckpointManager] Checkpoint guardado: " + lastCheckpointPos);
    }

    public void RespawnPlayer(GameObject player)
    {
        CharacterController controller = player.GetComponent<CharacterController>();

        Debug.Log("[CheckpointManager] Respawn solicitado.");

        // Desactivar CharacterController antes de mover
        if (controller != null)
        {
            controller.enabled = false;
            Debug.Log("[CheckpointManager] CharacterController DESACTIVADO.");
        }

        // Mover jugador al checkpoint
        player.transform.position = lastCheckpointPos;
        Debug.Log("[CheckpointManager] Nueva posición aplicada: " + lastCheckpointPos);

        // Activar de nuevo
        if (controller != null)
        {
            controller.enabled = true;
            Debug.Log("[CheckpointManager] CharacterController ACTIVADO.");
        }
    }
}

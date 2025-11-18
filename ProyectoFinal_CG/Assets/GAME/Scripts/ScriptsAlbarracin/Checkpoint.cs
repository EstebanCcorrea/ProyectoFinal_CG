using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("CHECKPOINT ACTIVADO en posición: " + transform.position);
            CheckpointManager.Instance.SetCheckpoint(transform.position);
        }
    }
}

using UnityEngine;

public class KeyFollowPlayer : MonoBehaviour
{
    public Vector3 offset = new Vector3(0, 0.5f, -1f); 
    public AudioClip pickUpSound; 

    private Transform player;
    private bool isFollowing = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.transform;
            transform.SetParent(player);
            transform.localPosition = offset;
            isFollowing = true;

          
            if (TryGetComponent<Collider>(out Collider col))
            {
                col.enabled = false;
            }

            if (TryGetComponent<Rigidbody>(out Rigidbody rb))
            {
                rb.isKinematic = true;
                rb.useGravity = false;
            }

          
            if (pickUpSound != null)
            {
                GameObject tempAudio = new GameObject("TempAudio");
                AudioSource audioSource = tempAudio.AddComponent<AudioSource>();
                audioSource.clip = pickUpSound;
                audioSource.spatialBlend = 0f; // 2D
                audioSource.Play();
                Destroy(tempAudio, pickUpSound.length);
            }
        }
    }

    void Update()
    {
        if (isFollowing)
        {
           
            transform.localPosition = offset;
        }
    }
}

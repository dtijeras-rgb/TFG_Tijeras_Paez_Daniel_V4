using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private AudioClip checkpointSound;

    private AudioSource audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (audioSource != null && checkpointSound != null)
            {
                audioSource.PlayOneShot(checkpointSound);
            }
            collision.GetComponent<PlayerRespawn>().SetCheckpoint(transform.position.x, transform.position.y);
            GetComponent<Animator>().enabled = true;
        }
    }
}

using System.Runtime.CompilerServices;
using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
  public Animator animator;
    [SerializeField] private AudioClip plataform;

    private AudioSource audioSource;    
    public float jumpForce = 5f;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        
        if (collision.transform.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Rigidbody2D>().linearVelocity = (Vector2.up * jumpForce);
            animator.Play("JumpTrampolin");

            if (audioSource != null && plataform != null)
            {
                audioSource.PlayOneShot(plataform);
            }
        }
    }

}

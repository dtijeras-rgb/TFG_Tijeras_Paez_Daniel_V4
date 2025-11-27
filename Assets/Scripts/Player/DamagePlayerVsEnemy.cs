using System;
using UnityEngine;

public class DamagePlayerVsEnemy : MonoBehaviour
{

    [SerializeField] private Animator animator;

    [SerializeField] private SpriteRenderer spriteRenderer;

    [SerializeField] private GameObject destroyParticle;

    [SerializeField] private float jumForce = 2.5f;

    [SerializeField] private int lifes = 2;

    [SerializeField] private AudioClip hitSound;
    
    private AudioSource audioSource;
    private void Awake()
    {
       audioSource = GetComponentInParent<AudioSource>();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.transform.CompareTag("Player"))
        {
            
            return;

        }
        var rb = collision.rigidbody;
        bool playerAbove = collision.transform.position.y > transform.position.y + 0.1f;
        bool playerFalling = rb != null && rb.linearVelocity.y <= 0f;
        bool playerJumpingOnEnemy = playerAbove && playerFalling;
        if (playerJumpingOnEnemy)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumForce);
            LoseLife();
            CheckLife();
        }
        else
        {

        LoseLife();
        CheckLife();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
    if (collision.CompareTag("Bullet"))
    {
           
           LoseLife();
           CheckLife();
           
       }
    }
    public void CheckLife()
    {
        if (lifes == 0)
        {
            
            GetComponent<Collider2D>().enabled = false;
            Invoke("ocultarSprite", 0.15f);
            destroyParticle.SetActive(true);
            Invoke("EnemyDie", 0.30f);
        }
    }

    public void ocultarSprite()
    {
        spriteRenderer.enabled = false;
    }
    public void LoseLife()
    {
        lifes--;

        if (audioSource != null && hitSound != null)
        {
            audioSource.PlayOneShot(hitSound);
        }

        animator.Play("Hit", 0, 0f);
    }

    public void EnemyDie()
    {
        Destroy(gameObject);
    }
}

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ToyPieceCollected : MonoBehaviour
{
    public GameObject messageFinishLevel;
    public float delayMessage = 10f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
      
        if (!collision.CompareTag("Player"))
        {
            return;
        }

        var move = collision.GetComponentInParent<PlayerController>();

            if (move != null)
            {
            move.enabled = false;
            }
        var shoot = collision.GetComponentInParent<PacifierBulletCollected>();

        if (shoot != null)
        {
            shoot.enabled = false;
        }
        Rigidbody2D rb = collision.GetComponentInParent<Rigidbody2D>();

        if ( rb != null)
        {
            rb.linearVelocityX = 0f; 
        }

        if (messageFinishLevel != null)
        {
            messageFinishLevel.SetActive(true);
        }

        Animator animator = collision.GetComponent<Animator>();
        if (animator != null)
        {

            animator.SetFloat("speedMove", 0f);
            animator.SetBool("isFloor", true);
            animator.SetBool("PlayerDobleJumpAnimator", false);
            animator.Play("PlayerIdleAnimator");

        }
        StartCoroutine(ChangeScene());
       
    }

    IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(delayMessage);
       
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        
    }
}


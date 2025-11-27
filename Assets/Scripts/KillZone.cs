using UnityEngine;

public class KillZone : MonoBehaviour
{
   private void OnTriggerEnter2D(Collider2D collision)
   {
       if (collision.CompareTag("Player"))
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

            if (rb != null)
            {
                rb.linearVelocity = Vector2.zero;
                rb.simulated = false;
            }

            var gameOver = FindAnyObjectByType<GameOver>(FindObjectsInactive.Include);

            if (gameOver != null)
            {
                gameOver.ShowGameOver();
            }

            collision.transform.root.gameObject.SetActive(false);

        }
    }
}

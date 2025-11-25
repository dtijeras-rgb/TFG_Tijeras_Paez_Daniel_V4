using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            PlayerHealthController health = collision.gameObject.GetComponent<PlayerHealthController>();
           // PacifierPlayerDamage playShooting = collision.gameObject.GetComponent<PacifierPlayerDamage>();
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();

            if (health != null)
            {
                health.DealDamage();
            }

            if(playerController != null)
            {
                float hitDirection = Mathf.Sign(collision.transform.position.x - transform.position.x);
                playerController.Knockback(hitDirection);
            }

          //  if (playShooting != null)
         //   {
                //playShooting.DisableShooting();
            //    UIController.instance.PacifierEnable(false);
           // }
        }
    }
}

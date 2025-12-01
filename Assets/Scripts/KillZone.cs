using UnityEngine;

public class KillZone : MonoBehaviour
{
   private void OnTriggerEnter2D(Collider2D collision)
   {
        if (!collision.CompareTag("Player")) { return; }


        PacifierPlayerDamage pacifier = collision.GetComponent<PacifierPlayerDamage>();

        if (pacifier == null) 
        { 
        pacifier = collision.GetComponentInChildren<PacifierPlayerDamage>();
        }
        if (pacifier != null)
        {
            pacifier.DisableShooting();
        }
            if (PlayerHealthController.instance != null)
             {
            PlayerHealthController.instance.KillPlayerFromKillZone();
            }
    }
}

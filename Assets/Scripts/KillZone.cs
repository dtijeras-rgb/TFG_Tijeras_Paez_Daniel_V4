using UnityEngine;

public class KillZone : MonoBehaviour
{
   private void OnTriggerEnter2D(Collider2D collision)
   {
       if (collision.CompareTag("Player"))
       {
        PlayerRespawn playerRespawn = collision.GetComponent<PlayerRespawn>();
            if (playerRespawn != null)
            {
                collision.transform.position = playerRespawn.GetCheckpoint();
            }
        }
    }
}

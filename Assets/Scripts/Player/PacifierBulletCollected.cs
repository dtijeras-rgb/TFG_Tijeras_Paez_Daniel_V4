using UnityEngine;

public class PacifierBulletCollected : MonoBehaviour
{
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            // Habilita el disparo en el script PacifierPlayerDamage
            PacifierPlayerDamage pacifierPlayerDamage = collision.GetComponent<PacifierPlayerDamage>();
            // Verifica si el componente existe antes de llamar al método
            if (pacifierPlayerDamage != null)
            {

                // Habilita la capacidad de disparar
                pacifierPlayerDamage.EnableShooting();
                UIController.instance.PacifierEnable(true);
            } 
                Destroy(gameObject);
        }
    }
}

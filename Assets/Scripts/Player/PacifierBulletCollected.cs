using UnityEngine;

public class PacifierBulletCollected : MonoBehaviour
{
   
    private bool collected = false; // Variable para evitar múltiples colecciones
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collected)
        {
            return;
        }

        if (collision.CompareTag("Player"))
        {
            collected = true; // Marca el objeto como coleccionado



            // Habilita el disparo en el script PacifierPlayerDamage
            PacifierPlayerDamage pacifierPlayerDamage = collision.GetComponent<PacifierPlayerDamage>();
            // Verifica si el componente existe antes de llamar al método
            if (pacifierPlayerDamage != null)
            {

                // Habilita la capacidad de disparar
                pacifierPlayerDamage.EnableShooting();
                UIController.instance.PacifierEnable(true);
            } 

            GetComponent<Collider2D>().enabled = false; // Desactiva el colisionador para evitar más colisiones
            GetComponent<SpriteRenderer>().enabled = false; // Desactiva el renderizador del sprite para hacer que el objeto desaparezca visualmente
            transform.GetChild(0).gameObject.SetActive(true); // Activa el primer hijo del objeto del juego
            // Destrueix l'objecte del joc després de 0.5 segons
            Destroy(gameObject, 0.5f);
        }
    }
}

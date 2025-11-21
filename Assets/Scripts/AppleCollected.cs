using UnityEngine;

public class AppleCollected : MonoBehaviour
{
    

    private bool collected = false; // Variable per evitar múltiples col·leccions
    // Mètode que s'activa quan un altre col·lisionador entra en contacte amb aquest objecte
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Si ja s'ha col·leccionat, no fa res
        if (collected)
        {
            return;
        }
        // Comprova si aquest objecte té l'etiqueta "BabyBottle"
        if (!CompareTag("Apple"))
        {
            return;
        }
        // Comprova si l'objecte que ha col·lisionat té l'etiqueta "Player"
        if (collision.CompareTag("Player"))
        {
            collected = true; // Marca l'objecte com col·leccionat 

            if(AppleController.instance != null)
            {
                AppleController.instance.CountApple(); // Afegeix una poma al comptador
            }
            GetComponent<Collider2D>().enabled = false; // Desactiva el col·lisionador per evitar més col·lisions
            // Desactiva el renderitzador de l'sprite per fer que l'objecte desaparegui visualment
            GetComponent<SpriteRenderer>().enabled = false;
            // Activa el primer fill de l'objecte del joc 
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            // Destrueix l'objecte del joc després de 0.5 segons
            Destroy(gameObject, 0.5f);
        }
    }
}

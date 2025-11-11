using UnityEngine;
// Classe per gestionar la col·lecció del biberó
public class BabyBottleCollected : MonoBehaviour
{
    // Mètode que s'activa quan un altre col·lisionador entra en contacte amb aquest objecte
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Comprova si l'objecte que ha col·lisionat té l'etiqueta "Player"
        if (collision.CompareTag("Player"))
        {
            // Desactiva el renderitzador de l'sprite per fer que l'objecte desaparegui visualment
            GetComponent<SpriteRenderer>().enabled = false;
            // Activa el primer fill de l'objecte del joc (pot ser una animació o efecte)
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            // Destrueix l'objecte del joc després de 0.5 segons
            Destroy(gameObject, 0.5f);
        }
    }
}

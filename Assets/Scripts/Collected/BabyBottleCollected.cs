using UnityEngine;

using UnityEngine.Audio;
// Classe per gestionar la col·lecció del biberó
public class BabyBottleCollected : MonoBehaviour
{
    [SerializeField] private int healAmount = 1; // Quantitat de vida que es recupera en col·leccionar el biberó

    private bool collected = false; // Variable per evitar múltiples col·leccions
    // Mètode que s'activa quan un altre col·lisionador entra en contacte amb aquest objecte

    public AudioSource collectSound; // So de col·lecció
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Si ja s'ha col·leccionat, no fa res
        if (collected)
        {
            return;
        }
        // Comprova si aquest objecte té l'etiqueta "BabyBottle"
        if (!CompareTag("BabyBottle"))
        {
            return;
        }
        // Comprova si l'objecte que ha col·lisionat té l'etiqueta "Player"
        if (collision.CompareTag("Player"))
        {
            collected = true; // Marca l'objecte com col·leccionat

            // Cura el jugador
            PlayerHealthController playerHealth = collision.GetComponent<PlayerHealthController>();
                if (playerHealth != null)
                {
                    playerHealth.HealPlayer(healAmount); 
                }
            
            PacifierPlayerDamage pacifier = collision.GetComponent<PacifierPlayerDamage>();

            if (pacifier == null)
            {
                pacifier = collision.GetComponentInChildren<PacifierPlayerDamage>();
            }
            if (pacifier != null)
            {
                pacifier.EnableShooting();
            }

            GetComponent<Collider2D>().enabled = false; // Desactiva el col·lisionador per evitar més col·lisions
            // Desactiva el renderitzador de l'sprite per fer que l'objecte desaparegui visualment
            GetComponent<SpriteRenderer>().enabled = false;
            // Activa el primer fill de l'objecte del joc 
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            // Destrueix l'objecte del joc després de 0.5 segons
            Destroy(gameObject, 0.5f);
            collectSound.Play();
        }
    }
}

using System.Collections;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;
    public int maxHealth = 4, currentHealth;

    [SerializeField] private float invincibilityDuration = 0.6f;
    [SerializeField] private float flashInterval = 6f;
    [SerializeField] private SpriteRenderer spriteToFlash;
    [SerializeField] private string enemyLayerName = "Enemy";

    [SerializeField] private AudioClip hitSound;
    private AudioSource audioSource;
    private bool invulnerable;

    public GameOver gameOver;

    
    public void Awake()
    {
        instance = this;


        if(spriteToFlash == null)
        {
            spriteToFlash = GetComponent<SpriteRenderer>();
        }

       audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        currentHealth = maxHealth;
        UIController.instance.UpdateHealthDisplay(currentHealth);
    }

    
    public void DealDamage()
    {
        if (invulnerable)
        {
            return;
        }
        if (audioSource != null && hitSound != null)
        {
            audioSource.PlayOneShot(hitSound, 3f);
        }
        currentHealth--;
        UIController.instance.PacifierEnable(false);
        UIController.instance.UpdateHealthDisplay(currentHealth);
        PacifierPlayerDamage playerShooting = GetComponent<PacifierPlayerDamage>();
        

        if (playerShooting != null)
        {
            playerShooting.DisableShooting();
            UIController.instance.PacifierEnable(false);
        }

        if (currentHealth <= 0)
        {
                
            if(gameOver != null)
            {
                gameOver.ShowGameOver();
            }
             
            gameObject.SetActive(false);
            return;
        }
        StartCoroutine(Invulnerability());


    }
   
    public void HealPlayer(int quantitylife)
    {
        if (currentHealth >= maxHealth)
        {
            return;
        }
        currentHealth += quantitylife;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        UIController.instance.UpdateHealthDisplay(currentHealth);
    }
    private IEnumerator Invulnerability()
    {
        invulnerable = true;

        int playerLayer = gameObject.layer;
        int enemyLayer = LayerMask.NameToLayer(enemyLayerName);
        if (enemyLayer == 0)
        {
            Physics2D.IgnoreLayerCollision(playerLayer, enemyLayer, true);
        }

        for (int i = 0; i < flashInterval; i++)
        {
            if (spriteToFlash != null)
            {
                spriteToFlash.enabled = false;
            }
            yield return new WaitForSeconds(invincibilityDuration / (flashInterval * 2));
            if (spriteToFlash != null)
            {
                spriteToFlash.enabled = true;
            }
            yield return new WaitForSeconds(invincibilityDuration / (flashInterval * 2));
        }

        if (enemyLayer >= 0)
        {
            Physics2D.IgnoreLayerCollision(playerLayer, enemyLayer, false);
        }

        invulnerable = false;

    }

    
}

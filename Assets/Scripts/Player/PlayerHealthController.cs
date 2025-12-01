using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;
    public int maxHealth = 3, currentHealth, maxLives = 3, currentLives;

    [SerializeField] private float invincibilityDuration = 0.6f;
    [SerializeField] private float flashInterval = 6f;
    [SerializeField] private SpriteRenderer spriteToFlash;
    [SerializeField] private string enemyLayerName = "Enemy";

    [SerializeField] private AudioClip hitSound;
    [SerializeField] private AudioSource mainMusic;
    private AudioSource audioSource;
    private bool invulnerable;

    public GameOver gameOver;

    [SerializeField] private PlayerController playerController;
    [SerializeField] private PacifierPlayerDamage pacifierPlayerDamage;
    [SerializeField] private Collider2D attackCollider;

    public static int sharedLives = -1;
    public static int sharedMaxLives = -1;
    public static int sharedHealth = -1;


    public void Awake()
    {
        instance = this;


        if(spriteToFlash == null)
        {
            spriteToFlash = GetComponent<SpriteRenderer>();
        }

       audioSource = GetComponent<AudioSource>();

        if (playerController == null)
        {
            playerController = GetComponent<PlayerController>();
        }

        if (pacifierPlayerDamage == null)
        {
            pacifierPlayerDamage = GetComponent<PacifierPlayerDamage>();
        }
    }

    void Start()
    {
        Time.timeScale = 1f;

        if (sharedMaxLives < 0)
        {
            sharedMaxLives = maxLives;
            sharedLives = maxLives;
        }

        if (sharedHealth < 0) {  sharedHealth = maxHealth; }

        maxLives = sharedMaxLives;
        
        currentLives = sharedLives;

        currentHealth = sharedHealth;
        UIController.instance.UpdateHealthDisplay(currentHealth);
        UIController.instance.UpdateLivesDisplay(currentLives);
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
        sharedHealth = currentHealth;

        PacifierPlayerDamage playerShooting = GetComponent<PacifierPlayerDamage>();
        
        if (playerShooting != null)
        {
            playerShooting.DisableShooting();
            UIController.instance.PacifierEnable(false);
        }

        if (currentHealth <= 0)
        {
            if (currentLives > 1)
            {
                StartCoroutine(ShowGameOver());
            }
            else {
                gameOver.ShowGameOver();
                gameObject.SetActive(false);
            }

               
            return;
        }
        StartCoroutine(Invulnerability());

    }
    private IEnumerator ShowGameOver()
    {
        invulnerable = true;

        if (playerController != null)
        {
            playerController.enabled = false;
        }

        if (pacifierPlayerDamage != null)
        {
            pacifierPlayerDamage.enabled = false;
        }

        if (attackCollider != null)
        {
            attackCollider.enabled = false;
        }

        int playerLayer = gameObject.layer;
        int enemyLayer = LayerMask.NameToLayer(enemyLayerName);
        if (enemyLayer >= 0)
        {
            Physics2D.IgnoreLayerCollision(playerLayer, enemyLayer, true);
        }

        Animator animator = GetComponent<Animator>();
        animator.SetTrigger("GameOver");
        if (mainMusic != null)
        {
            mainMusic.Stop();
        }

        if (gameOver != null && gameOver.gameOverSound != null)
        {
            gameOver.gameOverSound.Play();
        }


        yield return new WaitForSeconds(4f);

        LoseLifes();
    }

    private void LoseLifes()
    {
        int playerLayer = gameObject.layer;
        int enemyLayer = LayerMask.NameToLayer(enemyLayerName);
        if (enemyLayer >= 0)
        {
            Physics2D.IgnoreLayerCollision(playerLayer, enemyLayer, false);
        }
        currentLives--;

        if (currentLives < 0)
        {
            currentLives = 0;
        }

        sharedLives = currentLives;

        UIController.instance.UpdateLivesDisplay(currentLives);

        if (currentLives <= 0)
        {
            
                gameOver.ShowGameOver();
            
            gameObject.SetActive(false);
            return;
        }
        else
        {
            sharedHealth = maxHealth;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }


            
            //currentHealth = maxHealth;
            //UIController.instance.UpdateHealthDisplay(currentHealth);
            //StartCoroutine(Invulnerability());
        
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

        sharedHealth = currentHealth;
    }
    private IEnumerator Invulnerability()
    {
        invulnerable = true;

        int playerLayer = gameObject.layer;
        int enemyLayer = LayerMask.NameToLayer(enemyLayerName);

        if (enemyLayer >= 0)
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

    public void AddLife(int amount)
    {
        

        currentLives += amount;

        maxLives += amount;

        sharedMaxLives = maxLives;
        sharedLives = currentLives;

        UIController.instance.UpdateLivesDisplay(currentLives);
    }

    public void PlayGameOverSound()
    {
        if (gameOver != null && gameOver.gameOverSound != null)
        {
            gameOver.gameOverSound.Play();
           
        }
    }

    public static void ResetSharedLives() {
        sharedLives = -1;
        sharedMaxLives = -1;
        sharedHealth = -1;
    }

    public void KillPlayerFromKillZone()
    {
        if (invulnerable) return;

        currentHealth = 0;

        if (currentLives > 1)
        {
            StartCoroutine(ShowGameOver()); 
        }
        else
        {
            
            if (gameOver != null)
                gameOver.ShowGameOver();

            gameObject.SetActive(false);
        }
    }

}

using UnityEngine;
using UnityEngine.Audio;

public class PacifierPlayerDamage : MonoBehaviour
{

    
    public GameObject bullet;
    public Transform controlShoot;
    public float lifeTime = 0.5f;
    public float fireRate = 0.5f;
    private float nextFireTime = 0f;
    
    public SpriteRenderer player;
  
    public bool canShoot = false;
    private AudioSource audioSource;
    [SerializeField] private AudioClip shootSound;

    public static bool sharedHasPacifier = false;

    private void Awake()
    {
        audioSource = GetComponentInParent<AudioSource>();
    }

    private void Start()
    {
        canShoot = sharedHasPacifier;

        if (UIController.instance != null) {
            UIController.instance.PacifierEnable(canShoot);        
        }
    }
    void Update()
    {
        // comprueba si podemos disparar
        if (!canShoot)
        {
            return;
        }
        // comprueba si se ha pulsado el boton de disparo y si ha pasado el tiempo necesario entre disparos
        if (Input.GetButtonDown("Fire1") && Time.time >= nextFireTime)
        {
            Shoot();

            if (audioSource != null && shootSound != null)
            {
                audioSource.PlayOneShot(shootSound,3f);
            }
            nextFireTime = Time.time + fireRate;
        }
    }

    // instancia la bala y le da direccion
    public void Shoot()
    {
      
        GameObject newBullet = Instantiate(bullet, controlShoot.position, Quaternion.identity);

        BulletPlayer bulletPlayer = newBullet.GetComponent<BulletPlayer>();

        float direction = player.flipX ? -1f : 1f;
        bulletPlayer.SetDirection(direction);
        Destroy(newBullet, lifeTime);
    }

    // activa que podemos disparar
    public void EnableShooting()
    {
        canShoot = true;
        PacifierPlayerDamage.sharedHasPacifier = true;
        UIController.instance.PacifierEnable(true);
    }

    // desactiva que podemos disparar
    public void DisableShooting() {
        canShoot = false;

        sharedHasPacifier = false;

        if(UIController.instance != null)
        {
            UIController.instance.PacifierEnable(false);
        }
        
    }

    public static void ResetPacifier()
    {
        sharedHasPacifier = false;
    }
}

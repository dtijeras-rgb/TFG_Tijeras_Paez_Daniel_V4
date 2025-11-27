using UnityEngine;

public class BulletPlayer : MonoBehaviour
{

    [SerializeField] private float velocity;
   
    [SerializeField] private float distanceBullet = 2f;

    

   
    private float directionX = 1f;

    public Rigidbody2D rb;
    private Vector3 startPositionBullet;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        rb.freezeRotation = true;

       
    }

    public void SetDirection(float directionX)
    {
        this.directionX = Mathf.Sign(directionX);
        transform.localScale = new Vector3(directionX, 1f, 1f);
    }
    private void Start()
    {
        startPositionBullet = transform.position;
        rb.linearVelocity = new Vector2(directionX * velocity, 0f);
    }
    private void Update()
    {
        if (Vector3.Distance(startPositionBullet, transform.position) >= distanceBullet)
        {
            Destroy(gameObject);

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            Destroy(gameObject);
            return;
        }

        JumpBoxDamage box = collision.collider.GetComponentInParent<JumpBoxDamage>();
        if (box != null)
        {
            
            Destroy(gameObject);
            return;
        }
    }

    // Maneja la colisión con enemigos
    private void OnTriggerEnter2D(Collider2D collision)
    {
        JumpBoxDamage box = collision.GetComponentInParent<JumpBoxDamage>();
        if (box != null)
        {
            
            Destroy(gameObject);
            return;
        }

        // Verifica si el objeto colisionado es un enemigo
        if (collision.CompareTag("Enemy") || collision.transform.root.CompareTag("Enemy"))
        {
            // Aplica daño al enemigo
            DamagePlayerVsEnemy enemy = collision.GetComponentInParent<DamagePlayerVsEnemy>();
            // Verifica si el componente existe antes de llamar al método
            if (enemy != null) 
            {
                // Resta una vida al enemigo y verifica si debe morir
                enemy.LoseLife();
                enemy.CheckLife();
            }
            // Destruye la bala después de impactar
            Destroy(gameObject);
        }

        
    }
    // Destruye la bala cuando ya no es visible en la cámara
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

 
}

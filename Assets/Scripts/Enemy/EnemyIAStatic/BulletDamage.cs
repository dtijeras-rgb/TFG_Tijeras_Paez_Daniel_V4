using UnityEngine;

public class BulletDamage : MonoBehaviour
{
    public float speed = 2;
    public float lifeTime = 2;
    public bool left;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        if (left)
        {
            transform.Translate(Vector2.left*speed*Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet") || collision.CompareTag("EnemyBullet"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }

        if (collision.CompareTag("Ground"))
        {
            
            Destroy(gameObject);
        }

        if (collision.CompareTag("Player"))
        {
           
            PlayerHealthController.instance.DealDamage();
            Destroy(gameObject);
        }
    }
}

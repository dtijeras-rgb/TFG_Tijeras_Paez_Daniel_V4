using UnityEngine;

public class JumpBoxDamage : MonoBehaviour
{
    // Referència a l'animador de la caixa
    public Animator animator;
    // Referència al SpriteRenderer de la caixa
    public SpriteRenderer spriteRenderer;
    // Referència a l'objecte de la caixa trencada
    public GameObject brokenBox;
    // Força del salt aplicada al jugador
    public float jumpForce = 2f;
    // Vida inicial de la caixa
    public int life = 1;
    // Referència al col·lisionador de la caixa
    public GameObject boxCollider;
    // Referència al Collider2D de la caixa
    public Collider2D collider2D;
    // Referència a l'objecte de la caixa
    public GameObject boxObject;

    private void Start()
    {
        if(boxObject != null) { boxObject.SetActive(false); }
           
        
    }
    // Detecta la col·lisió amb el jugador
    private void OnCollisionEnter2D(Collision2D collision)
    {

       
        // Si l'objecte col·lisionat no és el jugador, surt del mètode
        if (!collision.transform.CompareTag("Player"))
        {
            return;
        }

        // Obtén el Rigidbody2D del jugador
        Rigidbody2D playerRB = collision.rigidbody;

        
        // Si el Rigidbody2D és nul, surt del mètode
        if (playerRB == null) 
        { 
            return; 
        }

        // Comprova si el jugador està per sobre de la caixa
        if (collision.transform.position.y > transform.position.y + 0.1f)
        {
            
            // Aplica la força de salt al jugador
            playerRB.linearVelocity = new Vector2(playerRB.linearVelocity.x, jumpForce);
            // Crida el mètode per perdre vida
            LosseLife();
        }
            
       
           
        
    }

    // Mètode per perdre vida
    public void LosseLife()
    {
        // Resta una vida
        life--;
        // Reprodueix l'animació de dany
        animator.Play("Hit");
        // Comprova si la caixa ha de ser destruïda
        CheckLife();
    }
    // Mètode per comprovar la vida restant
    public void CheckLife()
    {
        // Si la vida és 0 o menys, trenca la caixa
        if (life <= 0)
        {
            // Desemparenta l'objecte de la caixa
            boxObject.transform.SetParent(null);
            // Activa l'objecte de la caixa
            boxObject.SetActive(true);
            // Desactiva el col·lisionador de la caixa
            boxCollider.SetActive(false);
            // Desemparenta l'objecte de la caixa trencada
            brokenBox.transform.SetParent(null);
            // Activa la caixa trencada i desactiva la caixa original
            brokenBox.SetActive(true);
            // Desactiva el renderer de la caixa original
            spriteRenderer.enabled = false;
            // Destrueix la caixa després de 5 segons
            Invoke("DestroyBox", 0.5f);
        }
    }
    // Mètode per destruir la caixa
    public void DestroyBox()
    {
        // Destrueix l'objecte de la caixa
        Destroy(gameObject);

    }

}



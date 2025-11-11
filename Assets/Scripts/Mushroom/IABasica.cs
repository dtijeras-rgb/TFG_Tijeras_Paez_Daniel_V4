using UnityEngine;

public class IABasic : MonoBehaviour
{

    // Per cambiar de animació
    public Animator animator;

    // Per cambiar flip en la X d'esquerra a dreta en la animació
    public SpriteRenderer spriteRenderer;

    // Per la velocitat de moviment del enemic
    public float speed = 0.5f;

    // Per controlar el temps d'espera cuan cambia de direcció en la X
    private float waitTime;

    // Temps per controlar cuan arribi al punt
    public float startWaitTime = 2;

    // PEr controlar a quina pocició arriben
    private int i = 0;

    private Vector2 actualPos;

    // Array dels WayPoints
    public Transform[] moveSpots;

    void Start()
    {
        waitTime = startWaitTime;
    }

    
    void Update()
    {

        if (!ControlErrors()) return;

        bool estaEsperant = Moviment();
        estaParat(estaEsperant);
        Flip(estaEsperant);
       

    }

    private bool ControlErrors()
    {
        if (moveSpots == null || moveSpots.Length == 0) 
        { 
            return false; 
        }

        if (i < 0 || i >= moveSpots.Length)
        {
            i = 0;
        }
        if (moveSpots[i] == null) 
        { 
            return false; 
        }

        return true;
    }

    private void Flip(bool estaEsperant)
    {

        if (estaEsperant) return;
        // Flip segons la direcció
        Vector2 direction = moveSpots[i].position - transform.position;

        if (direction.x > 0.1f)
        {
            spriteRenderer.flipX = true;
            animator.SetBool("Idle",false);
        }
        else if (direction.x < -0.1f)
        {
            spriteRenderer.flipX = false;
            animator.SetBool("Idle", false);
        }
        
    }

    private bool Moviment()
    {

        // Moviment
        transform.position = Vector2.MoveTowards(transform.position, moveSpots[i].transform.position, speed * Time.deltaTime);
        if (Vector2.Distance(transform.position, moveSpots[i].transform.position) < 0.1f)
        {
            // hem pasat el temps despera
            if (waitTime <= 0)
            {
                // tenemos mas puntos a los que podamos ir
                if (moveSpots[i] != moveSpots[moveSpots.Length - 1])
                {
                    // incrementamos el punto
                    i++;

                }
                else
                {
                    // si no podemos ir a mas puntos reseteamos a 0
                    i = 0;
                }
                // el tiempo vuelve a ser 2
                waitTime = startWaitTime;
                // estamos parados
                return false;
            }
            else
            {
                // reta a nuestro tiempo 2 los segundos que van pasando
                waitTime -= Time.deltaTime;
                // estamos en movimiento
                return true;
            }
            
        }
        return false;
    }

    private void estaParat(bool estaEsperant)
    {
        animator.SetBool("Idle", estaEsperant);

    }
}

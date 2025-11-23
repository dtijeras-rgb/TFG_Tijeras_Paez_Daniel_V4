using Unity.VisualScripting;
using UnityEngine;

public class PlatformMoveControl : MonoBehaviour
{

   

    // Per la velocitat de moviment del enemic
    public float speed = 2f;

    // Per controlar el temps d'espera cuan cambia de direcció en la X
    private float waitTime;

    // Temps per controlar cuan arribi al punt
    public float startWaitTime = 2;

    // PEr controlar a quina pocició arriben
    private int i = 0;

 

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.collider.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.collider.transform.SetParent(null);
        }
    }
}


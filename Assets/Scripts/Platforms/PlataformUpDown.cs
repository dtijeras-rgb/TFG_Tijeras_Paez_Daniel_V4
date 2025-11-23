using UnityEngine;

public class PlataformUpDown : MonoBehaviour
{
    // Referencia al PlatformEffector2D del objecte
    private PlatformEffector2D platformEffector;

    // Temps d'espera abans de canviar l'estat del platformEffector
    public float WaitTime = 0.5f;
    // Estat actual del platformEffector
    private float timer;

    void Start()
    {
        platformEffector = GetComponent<PlatformEffector2D>();
        timer = WaitTime;
    }

    void Update()
    {
       
        if(Input.GetKey("s"))
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                
                platformEffector.rotationalOffset = 180f;
                timer = WaitTime;
            }
            else
            {
                timer -= Time.deltaTime;
            }
        }

        if(Input.GetKey("space"))
        {
            platformEffector.rotationalOffset = 0f;
            timer = WaitTime;
        }

    }
}

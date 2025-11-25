using UnityEngine;

public class IAEstatica : MonoBehaviour
{
    private float waitedTime;

    public float timeToAttack = 3;

    public Animator animator;

    public GameObject bulletPrefab;

    public Transform spawnPoint;

    

    private void Start()
    {
        //Ponemos el tiempo de ataque en 3
        waitedTime = timeToAttack;
    }

    private void Update()
    {

        
        if (waitedTime <= 0)
        {
            if (IsOnVisible())
            {
                //Haacemos animacion de atake
                animator.Play("Attack");
                //Realizamos el atacke de la bala
                Invoke("AttackBullet", 0.5f);
            }
            //Ponemos el tiempo de ataque en 3
            waitedTime = timeToAttack;
            
        }
        else
        {
            //sigue restando segundos hasta que sea menor igual a 0 que subiremos al if de arriba
            waitedTime -= Time.deltaTime;
        }
    }
    private bool IsOnVisible()
    {
        if (Camera.main == null)
        {
            return false;
        }

        Vector3 vector3 = Camera.main.WorldToViewportPoint(transform.position);

        return vector3.z > 0 && vector3.x > 0 && vector3.x < 1 && vector3.y > 0 && vector3.y < 1;
    }
    public void AttackBullet()
    {
        ////Instanciamos una bala, es decir creamos una bala
        GameObject newBullet;
        // Le pasamos a la bala la especificacion de la posicion
        newBullet = Instantiate(bulletPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}

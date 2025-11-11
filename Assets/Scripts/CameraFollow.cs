using UnityEngine;
// Classe per fer que la càmera segueixi un objectiu amb límits
public class CameraFollow : MonoBehaviour
{
    // Objectiu que la càmera ha de seguir
    public Transform target;
    // Distància addicional que la càmera segueix per davant de l'objectiu
    public float followAhead = 1f;
    // Límits mínims i màxims de la posició X de la càmera
    public float minPosX;
    // Límits màxims de la posició X de la càmera
    public float maxPosX;
    // Amplada de la càmera en unitats del món
    float camWhidth;
    // Transform que defineix el límit esquerre
    public Transform limitLeft;
    // Transform que defineix el límit dret
    public Transform limitRight;
    // Nombre de píxels per unitat per a l'ajust de píxels
    [SerializeField] private int pixelPerUnit = 0;
    void Start()
    {
        // Calcula l'amplada de la càmera en unitats del món
        camWhidth = Camera.main.orthographicSize * Camera.main.aspect;
        // Estableix els límits mínims i màxims de la posició X de la càmera basant-se en els límits definits
        minPosX = limitLeft.position.x + camWhidth;
        // Estableix els límits màxims de la posició X de la càmera basant-se en els límits definits
        maxPosX = limitRight.position.x - camWhidth;
        // Assegura que els límits siguin vàlids
        if (minPosX > maxPosX)
        {
            // Centra la càmera entre els límits si els límits són massa propers
            float mid = (limitLeft.position.x + limitRight.position.x * 0.5f);
            // Assigna la posició mitjana als límits mínim i màxim
            minPosX = maxPosX = mid;
        }
    }

  
    void Update()
    {
        // Si no hi ha un objectiu assignat, surt de la funció
        if (!target) return;
        // Calcula la nova posició X de la càmera seguint l'objectiu amb l'offset addicional
        float newPosx = target.position.x + followAhead;
        // Limita la nova posició X dins dels límits establerts
        newPosx = Mathf.Clamp(newPosx, minPosX, maxPosX);
        // Ajusta la posició X per a l'alineació de píxels si s'ha definit pixelPerUnit
        if (pixelPerUnit >0)
        {
            // Ajusta la posició X per a l'alineació de píxels
            newPosx = Mathf.Round(newPosx * pixelPerUnit) / pixelPerUnit;
        }
        // Actualitza la posició de la càmera amb la nova posició X calculada
        transform.position = new Vector3(newPosx, transform.position.y, transform.position.z);
    }
}

using UnityEngine;

public class BackgroundMovement : MonoBehaviour
{
    [SerializeField] private float speed = 0.01f;
    [SerializeField] private Vector2 direction = Vector2.left;

    private Material material;
    private Vector2 offset;

    private void Start()
    {
        material = GetComponent<Renderer>().material;
       
    }

    private void Update()
    {
        offset += direction.normalized * speed * Time.deltaTime;
        material.mainTextureOffset = offset;
    }
}

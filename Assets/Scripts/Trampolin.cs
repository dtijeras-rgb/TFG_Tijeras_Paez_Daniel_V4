using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
  public Animator animator;
    
  public float jumpForce = 5f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Rigidbody2D>().linearVelocity = (Vector2.up * jumpForce);
            animator.Play("JumpTrampolin");
        }
    }

}

using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    
    public static PlayerController instance;
    // Moviment Horitzontal
    public float moveSpeed;
    public float moveHorizontal;
    public Rigidbody2D rbPlayer;
    public SpriteRenderer srPlayer;

    // Salt
    [SerializeField] private float forceJump = 3f;
    public bool dobleJump;

    // Control terra
    [SerializeField] private Transform floorControl;
    [SerializeField] private float floorCheckRadius = 0.1f;   
    public LayerMask whatIsFloor;
    public bool isFloor;

    [SerializeField] private LayerMask wallMask;
    private Collider2D colPlayer;

    
    public Animator animator;

    [SerializeField] private float wallGraceTime = 0.10f;
    private float ignoreWallUntil = 0f;                       

    
    [SerializeField] private float airJumpCooldown = 0.18f;
    private float nextAirJumpTime = 0f;

    
    [SerializeField] private float groundedBuffer = 0.06f;
    private float groundedUntil = 0f;
    [SerializeField] private float floorCheckDistance = 0.08f; 

    public float knockbackForceX = 2f;
    public float knockbackForceY = 2f;
    public float knockbackLength = 0.15f;
    private float knockbackCounter;
    private float knockbackDir = -1;

    void Awake()
    {
        CheckComponentReferences();
        colPlayer = GetComponent<Collider2D>();
        instance = this;
    }
    
    void Update()
    {
        // Direcció del knockback
        if (knockbackCounter <= 0)
        {
            // Control de moviment horitzontal
            moveHorizontal = Input.GetAxis("Horizontal");

            // Control de terra
            isFloor = ControlFloor();
            // Buffer de terra
            if (isFloor) groundedUntil = Time.time + groundedBuffer;

            // Control de salt
            ControlJump();

            // Actualitza l'animador
            UpdateAnimator();
        }
        else 
        {
            // Direcció del knockback segons la posició del jugador respecte l'enemic
            knockbackCounter -= Time.deltaTime;
            
        }
        
    }

    void FixedUpdate()
    {
        if (knockbackCounter > 0)
        {
            knockbackCounter -= Time.deltaTime;
            rbPlayer.linearVelocity = new Vector2(knockbackDir * knockbackForceX, knockbackForceY);
            return;
        }
        ControlMoveHorizontal();
        
    }

    private void ControlJump()
    {
        if (!Input.GetButtonDown("Jump")) return;

        
        if (Time.time < nextAirJumpTime) return;

        bool onFloorBuffered = Time.time <= groundedUntil;

      
        bool ignoreWall = Time.time < ignoreWallUntil;

       
        bool onWall = !onFloorBuffered && !ignoreWall && rbPlayer.IsTouchingLayers(wallMask);

       
        if (onWall) return;

      
        if (onFloorBuffered)
        {
            dobleJump = true;
            rbPlayer.linearVelocity = new Vector2(rbPlayer.linearVelocity.x, forceJump);
            nextAirJumpTime = Time.time + airJumpCooldown;

            
            ignoreWallUntil = Time.time + wallGraceTime;
            return;
        }

       
        if (dobleJump)
        {
            dobleJump = false;
            rbPlayer.linearVelocity = new Vector2(rbPlayer.linearVelocity.x, forceJump);
            nextAirJumpTime = Time.time + airJumpCooldown;
        }
    }

    private void ControlMoveHorizontal()
    {
        rbPlayer.linearVelocity = new Vector2(moveHorizontal * moveSpeed, rbPlayer.linearVelocity.y);

        if (moveHorizontal > 0) srPlayer.flipX = false;
        else if (moveHorizontal < 0) srPlayer.flipX = true;
    }

    private void CheckComponentReferences()
    {
        if (!rbPlayer) rbPlayer = GetComponent<Rigidbody2D>();
        if (!srPlayer) srPlayer = GetComponent<SpriteRenderer>();
        if (!animator) animator = GetComponent<Animator>();
    }

    bool ControlFloor()
    {
       
        if (rbPlayer.linearVelocity.y > 0.05f) return false;

       
        RaycastHit2D hit = Physics2D.Raycast(floorControl.position, Vector2.down, floorCheckDistance, whatIsFloor);
        if (!hit) return false;

        return hit.normal.y > 0.5f;
    }


    private void UpdateAnimator()
    {
        animator.SetFloat("speedMove", MathF.Abs(rbPlayer.linearVelocity.x));
        animator.SetFloat("moveVertical", rbPlayer.linearVelocity.y);
        animator.SetBool("isFloor", isFloor);
        animator.SetBool("isDobleJump", !isFloor && !dobleJump);
    }

    void OnDrawGizmosSelected()
    {
        if (!floorControl) return;
        Gizmos.color = Color.green;
        Gizmos.DrawLine(floorControl.position, floorControl.position + Vector3.down * floorCheckDistance);
    }

    public void Knockback(float hitDirection)
    {
       
        knockbackDir = Mathf.Sign(hitDirection);
        knockbackCounter = knockbackLength;
       

    }

}






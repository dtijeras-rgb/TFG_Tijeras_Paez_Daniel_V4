using System;
using UnityEditor.Build.Reporting;
using UnityEngine;
using TMPro;

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

    [SerializeField] private AudioClip jumpSound;
    

    private AudioSource audioSource;    

    public float knockbackForceX = 2f;
    
    public float knockbackForceY = 2f;
    
    public float knockbackLength = 0.15f;
    
    private float knockbackCounter;
    
    private float knockbackDir = -1;

    // Cooldown de dash
    public float dashCooldown;
    // Partícules de dash
    public GameObject dashParticle;
    // Velocitat de dash
    public float dashSpeed = 30;
    // Durada de dash
    [SerializeField] private float dashDuration = 0.15f;
    // Estat de dash
    private bool isDashing = false;
    // Direcció de dash
    private float dashDirection = 1f;
    // Temps de dash
    private float dashTime;

    private float lastDashDirection = 1f ;

    public TextMeshProUGUI dashText;
    

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        CheckComponentReferences();
        colPlayer = GetComponent<Collider2D>();
        instance = this;
    }
    
    void Update()
    {

       dashCooldown -= Time.deltaTime;
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

        UpdateDashUI();
    }

    void FixedUpdate()
    {
        if (knockbackCounter > 0)
        {
            knockbackCounter -= Time.deltaTime;
            rbPlayer.linearVelocity = new Vector2(knockbackDir * knockbackForceX, knockbackForceY);
            return;
        }

        if (isDashing)
        {
            dashTime -= Time.fixedDeltaTime;

            rbPlayer.linearVelocity = new Vector2(dashDirection * dashSpeed, 0f);

            if (dashTime <= 0f)
            {
                isDashing = false;
            }
            return;
        }
        if (Input.GetKey("j") && dashCooldown <= 0)
        {
            Dash();
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

            if(audioSource != null && jumpSound != null)
            {
                audioSource.PlayOneShot(jumpSound);
            }

            ignoreWallUntil = Time.time + wallGraceTime;
            return;
        }

       
        if (dobleJump)
        {
            dobleJump = false;
            rbPlayer.linearVelocity = new Vector2(rbPlayer.linearVelocity.x, forceJump);
            nextAirJumpTime = Time.time + airJumpCooldown;

            if (audioSource != null && jumpSound != null)
            {
                audioSource.PlayOneShot(jumpSound);
            }
        }
    }

    private void ControlMoveHorizontal()
    {
        rbPlayer.linearVelocity = new Vector2(moveHorizontal * moveSpeed, rbPlayer.linearVelocity.y);

        if (moveHorizontal > 0.01f)
        {
            srPlayer.flipX = false;
            lastDashDirection = 1f;
        }
        else if (moveHorizontal < -0.01f)
        {
            srPlayer.flipX = true;
            lastDashDirection = -1f;
        }
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

    public void Dash()
    {
        GameObject dashObject;

        dashObject = Instantiate(dashParticle, transform.position, transform.rotation);
        Destroy(dashObject, 1f);

      dashDirection = lastDashDirection;

        if (dashDirection == 0f)
        {
            dashDirection = srPlayer.flipX ? -1f : 1f;
        }

        isDashing = true;
        dashTime = dashDuration;


        dashCooldown = 2f;

       
    }

    private void UpdateDashUI()
    {
        if (dashText == null)
        {
            return;
        }

        if (dashCooldown > 0f)
        {
            dashText.text = dashCooldown.ToString("F1");
        }
        else
        {
            dashText.text = "";
        }
    }
}






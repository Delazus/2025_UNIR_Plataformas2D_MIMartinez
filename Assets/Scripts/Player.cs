using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb2D;
    private Animator animator;
    private Collider2D col;

    [Header("Sistema de movimiento")]
    [SerializeField] float walkSpeed = 3f;
    [SerializeField] float jumpSpeed = 10f;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] int maxJumps = 2; // doble salto

    [Header("Sistema de combate")]
    [SerializeField] private Transform puntoAtaque;
    [SerializeField] private float radioAtaque;
    [SerializeField] private float danhoAtaque;
    [SerializeField] private LayerMask queEsDanhable;
    [SerializeField] private AudioSource shootAudioSource1;
    [SerializeField] private AudioSource shootAudioSource2;

    void Start()
    {

    }


    int jumpsLeft;
    bool wasGrounded;
    void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        col = GetComponent<Collider2D>();

        jumpsLeft = maxJumps;
        wasGrounded = true;
    }


    Vector2 rawMove = Vector2.zero;
    bool mustJump = false;
    bool doShoot = false;

    void Update()
    {
        UpdateRawMove();
        LanzarAtaque();

        // En el suelo
        bool grounded = col.IsTouchingLayers(groundLayer);
        animator.SetBool("IsGrounded", grounded);

        // Resetea saltos
        if (grounded)
        { jumpsLeft = maxJumps; }
        wasGrounded = grounded;

        //Move
        rb2D.linearVelocityX = rawMove.x * walkSpeed;

        if (rawMove.x != 0)
        {
            animator.SetBool("IsWalking", true);
            if (rawMove.x > 0) //Dcha.
            {
                transform.eulerAngles = Vector3.zero;
            }
            else //Izq.
            { transform.eulerAngles = new Vector3(0, 180, 0); }
        }
        else
        {
            animator.SetBool("IsWalking", false);
        }

        //Jump
        if (mustJump && jumpsLeft > 0)
        {
            mustJump = false;
            rb2D.linearVelocityY = jumpSpeed;
            animator.SetTrigger("Jump");
            jumpsLeft--;
            wasGrounded = false;
        }

        //Attack
        if (doShoot)
        {
            doShoot = false;
            if (animator.GetBool("IsWalking"))
            { animator.SetTrigger("Attack-Run"); }
            else
            { animator.SetTrigger("Attack"); }
        }

    }
    
    private void UpdateRawMove()
    {
        rawMove = Vector2.zero;

        if (Keyboard.current.aKey.isPressed)
            { rawMove += Vector2.left; }
        else if (Keyboard.current.dKey.isPressed)
            { rawMove += Vector2.right; }

        if (Keyboard.current.spaceKey.wasPressedThisFrame) 
        { 
            mustJump = true;
            // Reproducir el sonido
            if (shootAudioSource1 != null)
            {
                shootAudioSource1.Play();
            }
        }
    }

    private void LanzarAtaque()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        { 
            doShoot = true;
            if (shootAudioSource2 != null)
            {
                shootAudioSource2.Play();
            }
        }
    }

    //Se ejecuta desde evento de animación
    private void Ataque()
    {
        Collider2D[] collidersTocados = Physics2D.OverlapCircleAll(puntoAtaque.position, radioAtaque, queEsDanhable);
        foreach (Collider2D item in collidersTocados)
        {
            SistemaVidas sistemaVidas = item.gameObject.GetComponent<SistemaVidas>();
            sistemaVidas.RecibirDanho(danhoAtaque);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(puntoAtaque.position, radioAtaque);
    }
}

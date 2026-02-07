using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch; 
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
public class CharacterMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    Animator animator;

    [SerializeField]private float speed = 5f;
    [SerializeField]private float jumpForce = 200f;
    [SerializeField]private int MaxJumpCount = 2;

    [SerializeField] float screenLimit = -9.81f;

    private int jumpCount = 0;
    private InputSystem_Actions inputSystem;
    private bool isInversed = false;
    private Camera cam;

    [SerializeField] Transform GroundCheckTransforme;
    [SerializeField] float groundCheckRadius;
    [SerializeField] LayerMask groundLayer;

    bool isGrounded;
    bool isMoving;
    bool flip;

    void Awake()
    {
        inputSystem = new InputSystem_Actions();
        inputSystem.Enable();
        isInversed = false;
        cam = Camera.main;
        EnhancedTouchSupport.Enable();

        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        /*if (cam != null)
        {
            Vector3 CamSize = cam.WorldToViewportPoint(new Vector3(cam.pixelWidth / 2, cam.pixelHeight / 2, 0));

            screenLimit = CamSize.x;
        }*/

        flip = sr.flipX;
    }

    private void OnEnable()
    {
        inputSystem.Player.Jump.performed += Jump;
    }

    private void OnDisable()
    {
        inputSystem.Player.Jump.performed -= Jump;
    }

    private void FixedUpdate()
    {
        if (GroundCheck()) jumpCount = 0;
        else animator.SetFloat("Y Velocity", rb.linearVelocityY);
    }

    private void Update()
    {
        if (Touch.activeTouches.Count > 0)
        {
            Vector2 screenPos = Touch.activeTouches[0].screenPosition;
            float moveDirectionX = MoveDirection(screenPos);

            FlipSprites(moveDirectionX);

            rb.linearVelocityX = speed * moveDirectionX;

            if (!isMoving) animator.SetBool("Move", true); isMoving = true;
        }
        else
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            if (horizontal != 0)
            {
                if (horizontal >= 0.35f) rb.linearVelocityX = speed;
                else if (horizontal <= -0.35f) rb.linearVelocityX = -speed;

                FlipSprites(horizontal);

                if (!isMoving) animator.SetBool("Move", true); isMoving = true;
            }
            else
            {
                rb.linearVelocityX = 0;
                if (isMoving) animator.SetBool("Move", false); isMoving = false;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                JumpKey();
            }
        }

        Border();
    }
    

    void Jump(InputAction.CallbackContext context)
    {
        if (jumpCount < MaxJumpCount)
        {
            rb.linearVelocity = new Vector2(0, 0);
            rb.AddForce(transform.up * jumpForce);
            jumpCount++;
        }
    }

    void JumpKey()
    {
        if (jumpCount < MaxJumpCount)
        {
            rb.linearVelocity = new Vector2(0, 0);
            rb.AddForce(transform.up * jumpForce / 10);
            jumpCount++;
        }
    }
    

    void Border()
    {
        if (transform.position.x > screenLimit)
        {
            transform.position = new Vector3(screenLimit, transform.position.y, transform.position.z);
        }
        else if (transform.position.x < -screenLimit)
        {
            transform.position = new Vector3(-screenLimit, transform.position.y, transform.position.z);
        }
    }

    private float MoveDirection(Vector2 screenPos)
    {
        Vector3 worldPos = cam.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, Mathf.Abs(cam.transform.position.z)));

        return worldPos.x > 0 ? 1f : -1f;
    }

    bool GroundCheck()
    {
        bool ground = Physics2D.OverlapCircle(GroundCheckTransforme.position, groundCheckRadius, groundLayer);
        if (ground != isGrounded)
        {
            isGrounded = ground;
            animator.SetBool("Grounded", ground);
        }

        return ground;
    }

    void FlipSprites(float direction)
    {
        if (direction > 0) flip = true;
        else flip = false;

        if (flip != sr.flipX) sr.flipX = flip;
    }
}

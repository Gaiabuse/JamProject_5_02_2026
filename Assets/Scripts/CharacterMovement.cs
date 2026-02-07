using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch; 
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
public class CharacterMovement : MonoBehaviour
{
    [SerializeField]private Rigidbody2D rb;
    [SerializeField]private float speed = 5f;
    [SerializeField]private float jumpForce = 200f;
    [SerializeField]private int MaxJumpCount = 2;
    [SerializeField] private SpriteRenderer sr;

    [SerializeField] float screenLimit = -9.81f;

    private int jumpCount = 0;
    private InputSystem_Actions inputSystem;
    private bool isInversed = false;
    private Camera cam;

    [SerializeField] Transform GroundCheckTransforme;
    [SerializeField] float groundCheckRadius;
    [SerializeField] LayerMask groundLayer;

    void Awake()
    {
        inputSystem = new InputSystem_Actions();
        inputSystem.Enable();
        isInversed = false;
        cam = Camera.main;
        EnhancedTouchSupport.Enable();

        /*if (cam != null)
        {
            Vector3 CamSize = cam.WorldToViewportPoint(new Vector3(cam.pixelWidth / 2, cam.pixelHeight / 2, 0));

            screenLimit = CamSize.x;
        }*/
    }

    private void OnEnable()
    {
        inputSystem.Player.Jump.performed += Jump;
    }

    private void OnDisable()
    {
        inputSystem.Player.Jump.performed -= Jump;
    }

    private void SwitchMovement()
    {
        isInversed = !isInversed;
    }

    private void FixedUpdate()
    {
        if (GroundCheck()) jumpCount = 0;
    }

    private void Update()
    {
        if (Touch.activeTouches.Count > 0)
        {
            Vector2 screenPos = Touch.activeTouches[0].screenPosition;
            float moveDirectionX = MoveDirection(screenPos);

            if (screenPos.x > 0) rb.linearVelocityX = speed * moveDirectionX;
            else rb.linearVelocityX = speed * moveDirectionX;
        }
        else
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            if (horizontal != 0)
            {
                if (horizontal > 0) rb.linearVelocityX = speed;
                else rb.linearVelocityX = -speed;
            }
            else rb.linearVelocityX = 0;

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
            rb.AddForce(transform.up * jumpForce);
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
        return Physics2D.OverlapCircle(GroundCheckTransforme.position, groundCheckRadius, groundLayer);
    }
}

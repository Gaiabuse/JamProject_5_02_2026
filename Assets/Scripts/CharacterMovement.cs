using System;
using UnityEngine;
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

    private void Update()
    {
        float moveDirectionX = 0;
        
        if (Touch.activeTouches.Count > 0)
        {
            Vector2 screenPos = Touch.activeTouches[0].screenPosition;
            moveDirectionX = MoveDirection(screenPos);
        }
        else
        {
            moveDirectionX = 0;
        }
        
        float directionMultiplier = isInversed ? -1f : 1f;
        rb.linearVelocityX = moveDirectionX * speed * directionMultiplier;
        
        float clampedX = Mathf.Clamp(transform.position.x, -screenLimit, screenLimit);
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);


        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            Debug.Log("Left");
        }

        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            Debug.Log("Right");
        }
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
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log(other.gameObject.name);
        if (other.gameObject.CompareTag("Floor"))
        {
            jumpCount = 0;
        }
        
    }

    private float MoveDirection(Vector2 screenPos)
    {
        Vector3 worldPos = cam.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, Mathf.Abs(cam.transform.position.z)));
        
        return worldPos.x > 0 ? 1f : -1f;
    }
}

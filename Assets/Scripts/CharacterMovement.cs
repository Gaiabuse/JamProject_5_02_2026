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
    [SerializeField]private float leftLimit = -9.81f;
    [SerializeField]private float rightLimit = 9.81f;
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
        if (cam != null)
        {
            rightLimit = cam.orthographicSize*cam.aspect - sr.size.x/2;
            leftLimit = -rightLimit;
        }
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
        float moveDirection = 0;

        if (Touch.activeTouches.Count > 0)
        {
            Vector2 screenPos = Touch.activeTouches[0].screenPosition;
            moveDirection = MoveDirection(screenPos);
        }
        else
        {
            moveDirection = 0;
        }
        if (isInversed)
        {
            rb.linearVelocityX = moveDirection * -speed;
        }
        else
        {
            rb.linearVelocityX = moveDirection * speed;
        }

        if (transform.position.x >= rightLimit)
        {
            transform.position = new Vector3(rightLimit, transform.position.y, transform.position.z);
        }else if (transform.position.x <= leftLimit)
        {
            transform.position = new Vector3(leftLimit, transform.position.y, transform.position.z);
        }
    }
    

    void Jump(InputAction.CallbackContext context)
    {
        if (jumpCount < MaxJumpCount)
        {
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

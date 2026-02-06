using UnityEngine;

public class MovementPlatform : MonoBehaviour
{
    [SerializeField] private float distance;
    [SerializeField] private float Speed;
    [SerializeField] private Vector3 Direction;
    private Vector3 startPosition;
    void Start()
    {
        startPosition = transform.position;
    }
    
    void Update()
    {
        float movement = Mathf.Sin(Time.time * Speed) * distance;
        
        transform.position = startPosition + (Direction * movement);
    }

}

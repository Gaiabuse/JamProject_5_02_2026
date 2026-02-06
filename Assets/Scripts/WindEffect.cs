using System;
using UnityEngine;

public class WindEffect : MonoBehaviour
{
    [SerializeField] private float WindStrength = 200;
    [SerializeField] private Vector2 WindDirection = Vector2.up;
    
    private Rigidbody2D rb;


    private void Start()
    {
        rb = null;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log(other.name);
            if(rb == null)rb = other.GetComponent<Rigidbody2D>();
            rb.AddForce(WindDirection * WindStrength, ForceMode2D.Impulse);
        }
    }
}

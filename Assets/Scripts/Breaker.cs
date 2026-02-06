using System;
using System.Collections;
using UnityEngine;

public class Breaker : MonoBehaviour
{
    [SerializeField] private float breakTime = 3f;


    private void OnTriggerEnter2D(Collider2D other)
    {
        StartCoroutine(BreakCoroutine());
    }

    IEnumerator BreakCoroutine()
    {
        yield return new WaitForSeconds(breakTime);
        Destroy(transform.parent.gameObject);
    }
}

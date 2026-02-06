using System;
using System.Collections;
using UnityEngine;

public class ArrowTrap : MonoBehaviour
{
    private bool StopTrap = false;
    [SerializeField] private float TimeBetweenArrows = 3f;
    [SerializeField] private GameObject Arrow;

    private void Start()
    {
        StartCoroutine(ArrowCoroutine());
    }

    IEnumerator ArrowCoroutine()
    {
        while (!StopTrap)
        {
            yield return new WaitForSeconds(TimeBetweenArrows);
            Instantiate(Arrow, transform.position, Quaternion.identity);
        }
    }
}

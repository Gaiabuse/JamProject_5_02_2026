using System;
using UnityEngine;

public class TraversablePlatform : MonoBehaviour
{
   [SerializeField] private bool activeCollider;
   [SerializeField] private Collider2D collider2D;

   private void OnTriggerEnter2D(Collider2D other)
   {
      if (other.CompareTag("Player"))
      {
         Debug.Log(other.gameObject.name);
         collider2D.enabled = activeCollider;
      }
   }
}

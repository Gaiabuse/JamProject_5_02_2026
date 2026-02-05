using System;
using Unity.VisualScripting;
using UnityEngine;

namespace SimsFolder.Scripting.Manager
{
    public class ZoneEnter : MonoBehaviour
    {
        public event Action zoneEntered;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag == "Player")
            {
                Detection();   
            }
        }
        
        public void Detection()
        {
            zoneEntered?.Invoke();
        }
    }
}
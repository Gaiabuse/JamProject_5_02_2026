using System;
using Unity.VisualScripting;
using UnityEngine;

namespace SimsFolder.Scripting.Manager
{
    public class ZoneEnter : MonoBehaviour
    {
        public event Action zoneEntered;

        public bool entered = false;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag == "Player")
            {
                Detection();   
            }
        }


        void Update()
        {
            if (Input.GetMouseButtonDown(0))
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
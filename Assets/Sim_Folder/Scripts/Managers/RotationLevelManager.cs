using System;
using UnityEngine;

namespace SimsFolder.Scripting.Manager
{
    public enum currentRotationLevel
    {
        level0,
        level1,
        level2,
        level3
    }
    
    public class RotationLevelManager : MonoBehaviour
    {
        [Header("References")]
        [SerializeField]private ZoneEnter zoneEnter;
        [SerializeField]private GameObject map;


        [Header("Rotation Level")]
        [SerializeField] private float rotationMap; //est utile pour touts les levels levels
        [SerializeField]private currentRotationLevel currentLevel = currentRotationLevel.level0;
        
        private int currentLevelRotation = 0;
        private Vector2 baseRotation = Vector2.zero;
        
        /// <summary>
        /// obseverToCheckCurrentRotation
        /// </summary>
        private void OnEnable()
        {
            zoneEnter.zoneEntered += RotationLevel;
        }

        private void OnDisable()
        {
            zoneEnter.zoneEntered -= RotationLevel;
        }

        #region rotations
        /// <summary>
        /// rotationMecha
        /// </summary>
        public void RotationLevel()
        {
            currentLevel++;
            if (currentLevel > currentRotationLevel.level3)
            {
                currentLevel = currentRotationLevel.level0;
                Debug.Log("FIN DU JEU");
                //ou fin de game
            }
            
            switch (currentLevel)
            {
                case currentRotationLevel.level0:
                    MapSwitchingFace(0);
                    break;
                case currentRotationLevel.level1:
                    //donner sens de rotation de la map
                    MapSwitchingFace(rotationMap);
                    break;
                case currentRotationLevel.level2:
                    //donner sens contraire de rotation de la map
                    MapSwitchingFace(0);
                    break;
                case currentRotationLevel.level3:
                    MapSwitchingFace(rotationMap);
                    SpawnPics();
                    //rotationer la map et laissant le joueur en haut
                    //faire apparaitre les pics
                    break;
            }
        }

        public void MapSwitchingFace(float angle)
        {
            map.transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }

        public void SpawnPics()
        {
            
        }
        #endregion
    }
}
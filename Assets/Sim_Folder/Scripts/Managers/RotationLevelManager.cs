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
        [SerializeField] private Vector2 rotationMapLevl0; //est utile pour touts les levels levels
        
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
            currentRotationLevel currentLevel = new currentRotationLevel();
            
            //switch case state pour savoir quel type de level on est la
            switch (currentLevel)
            {
                case currentRotationLevel.level0:
                    MapSwitchingFace(baseRotation);
                    break;
                case currentRotationLevel.level1:
                    //donner sens de rotation de la map
                    MapSwitchingFace(rotationMapLevl0);
                    break;
                case currentRotationLevel.level2:
                    //donner sens contraire de rotation de la map
                    MapSwitchingFace(-rotationMapLevl0);
                    break;
                case currentRotationLevel.level3:
                    MapSwitchingFace(rotationMapLevl0);
                    SpawnPics();
                    //rotationer la map et laissant le joueur en haut
                    //faire apparaitre les pics
                    break;
            }
        }

        public void MapSwitchingFace(Vector2 direction)
        {
            map.transform.rotation = Quaternion.AngleAxis(direction.x, -Vector3.forward);
        }

        public void SpawnPics()
        {
            
        }
        #endregion
    }
}
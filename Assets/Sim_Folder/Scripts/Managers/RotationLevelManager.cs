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
        [SerializeField]private GameObject picsParent;


        [Header("Rotation Level")]
        [SerializeField] private float rotationMap; //est utile pour touts les levels levels
        [SerializeField]private currentRotationLevel currentLevel = currentRotationLevel.level0;
        
        [Header("Player Affectation")]
        [SerializeField] private GameObject player_GO;
        [SerializeField] private Transform start_T;
        [SerializeField] private Transform end_T;
        
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
            }
            
            switch (currentLevel)
            {
                case currentRotationLevel.level0:
                    MapSwitchingFace(0);
                    break;
                case currentRotationLevel.level1: 
                    PlayerGoUp(start_T);
                    MapSwitchingFace(rotationMap);
                    break;
                case currentRotationLevel.level2:
                    PlayerGoUp(start_T);
                    MapSwitchingFace(0);
                    break;
                case currentRotationLevel.level3:
                    MapSwitchingFace(rotationMap);
                    SpawnPics();
                    //on laisse le player en Up
                    break;
            }
        }

        public void MapSwitchingFace(float angle)
        {
            map.transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }

        public void SpawnPics()
        {
            picsParent.SetActive(true);
        }
        #endregion


        #region PlayerAffectation

        private void PlayerGoUp(Transform movePlayerTo)
        {
            player_GO.transform.position = movePlayerTo.position;
        }
        #endregion
    }
}
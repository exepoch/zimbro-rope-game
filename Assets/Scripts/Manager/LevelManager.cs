using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Manager
{
    public class LevelManager : MonoBehaviour
    {
        public List<Button> levelButtons;
        public List<GameObject> levelOpeningParticles;
        
        private SceneManager sceneManager;

        private void Awake()
        {
            sceneManager = GetComponent<SceneManager>();
            
            int currentProggress = PlayerPrefs.GetInt("LastCompletedLevelIndex");

            
            for (int i = 0; i < levelButtons.Count; i++)
            {
                levelButtons[i].interactable = false;
            }
            for (int i = 0; i < currentProggress+1; i++)
            {
                levelButtons[i].interactable = true;
            }
        }

        public void OnClickEvent(int index)
        {
            int ind = index + 1;
            
            sceneManager.ChangeScene(ind);
            GameManager.activeLevelIndex = index;
            for (int i = 0; i < levelButtons.Count; i++)
            {
                levelButtons[i].interactable = false;
            }
        }
    }    
}


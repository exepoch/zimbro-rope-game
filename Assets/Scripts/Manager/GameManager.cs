using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Trade;
using UnityEngine;
using UnityEngine.UI;

namespace Manager
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager manager;
        public  List<bool> levelCompletion;
        public static int activeLevelIndex;
        public static float PlayerDepot = 1000;
        public TextMeshProUGUI priceText;

        public List<ItemBase> items;
        public static Dictionary<string, float> priceDic = new Dictionary<string, float>();
        private int lastCompletedIndex = 0;
        
        private void Awake() => LoadProgress();

        private void Update()
        {
            if (priceText != null)
                UpdatePriceText();
        }

        public void LoadProgress()
        {
            PlayerDepot = PlayerPrefs.GetFloat("playerdepot");

            if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex != 1) return;
            levelCompletion[0] = true;
            lastCompletedIndex = PlayerPrefs.GetInt("LastCompletedLevelIndex");
            print("GamenManager lastCompIndex : " + lastCompletedIndex);
            
            for (int i = 0; i < levelCompletion.Count; i++)
            {
                levelCompletion[i] = false;
            }
            for (int i = 0; i < lastCompletedIndex; i++)
            {
                levelCompletion[i] = true;
            }

            if (!PlayerPrefs.HasKey("activeCharacterIndex"))
                PlayerPrefs.SetInt("activeCharacterIndex", 0);
            if (!PlayerPrefs.HasKey("activeItemIndex"))
                PlayerPrefs.SetInt("activeItemIndex", 0);
        }

        public void ResetSave()
        {
            PlayerPrefs.SetInt("LastCompletedLevelIndex",0);
            PlayerPrefs.SetInt("activeItemIndex", 0);
            PlayerPrefs.SetInt("activeCharacterIndex", 0);

            PlayerDepot = 1000;
            PlayerPrefs.SetFloat("playerdepot", 1000);

            foreach (ItemBase item in items)
            {
                item.ResetItem();
            }

            lastCompletedIndex = 0;
            for (int i = 1; i < levelCompletion.Count; i++)
            {
                levelCompletion[i] = false;
            }
        }

        public void UpdateLevelProgress()
        {
            lastCompletedIndex = activeLevelIndex;
            PlayerPrefs.SetInt("LastCompletedLevelIndex",activeLevelIndex);
        }

        public void UpdatePriceText()
        {
            priceText.text = PlayerDepot.ToString();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager;
using UnityEngine.UI;
using System.Linq;

namespace Trade
{
    public class ItemBase : MonoBehaviour
    {
        public float tradePrice;
        public Toggle toggle;
        public int index;
        public bool isItem;


        public Color red;
        public Color green;

        private bool hasPurchased = false;

        List<ItemBase> itemBaseList = new List<ItemBase>();

        private void Awake() => LoadTradeVariables();

        private void OnEnable()
        {
            print(gameObject.name + " // " + hasPurchased);
            if (hasPurchased)
                transform.GetComponentInParent<Image>().color = green;
            else
            { transform.GetComponentInParent<Image>().color = red; }
        }

        public void TryPurchase()
        {
            if (hasPurchased)
            {
                foreach (ItemBase item in itemBaseList)
                {
                    item.toggle.isOn = false;
                }

                toggle.isOn = true;
                SetIndex();
            }
            else
            {
                if (GameManager.PlayerDepot < tradePrice) return;

                foreach (ItemBase item in itemBaseList)
                {
                    item.toggle.isOn = false;
                }

                SetIndex();

                GameManager.PlayerDepot = GameManager.PlayerDepot - tradePrice;
                PlayerPrefs.SetFloat("playerdepot", GameManager.PlayerDepot);
                hasPurchased = true;
                PlayerPrefs.SetString(gameObject.name, "true");
                toggle.isOn = true;
                transform.GetComponentInParent<Image>().color = green;
            }
        }




        private void LoadTradeVariables()
        {
            itemBaseList.ForEach(o => o.toggle.isOn = false);
            transform.GetComponentInParent<Image>().color = red;
            if (isItem && PlayerPrefs.GetInt("activeItemIndex") == index)
            {
                toggle.isOn = true;
            }
            else if(!isItem && PlayerPrefs.GetInt("activeCharacterIndex") == index)
            {
                toggle.isOn = true;
            }

            if (PlayerPrefs.HasKey(gameObject.name))
            {
                string hasP = PlayerPrefs.GetString(gameObject.name); //true-false
                hasPurchased = bool.Parse(hasP);
              
            }
            else
            {
                PlayerPrefs.SetString(gameObject.name, "false");
                toggle.isOn = false;
            }

            if (!GameManager.priceDic.ContainsKey(gameObject.name))
            {
                GameManager.priceDic.Add(gameObject.name, tradePrice);
            }

             itemBaseList = FindObjectsOfType<ItemBase>().ToList();
        }



        private void SetIndex()
        {
            if (isItem)
            {
                PlayerPrefs.SetInt("activeItemIndex", index);
            }
            else
            {
                PlayerPrefs.SetInt("activeCharacterIndex", index);
            }
        }


        public void ResetItem()
        {
            toggle.isOn = false;
            hasPurchased = false;
            PlayerPrefs.SetString(gameObject.name, "false");
        }
    }
}

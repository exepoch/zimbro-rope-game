using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager;

namespace GamePlay
{
    public class CoinBagBehaviour : MonoBehaviour
    {
        public GameObject Bag;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                GetComponent<Animator>().enabled = true;
                Bag.SetActive(false);

                GameManager.PlayerDepot += 100;
                PlayerPrefs.SetFloat("playerdepot", GameManager.PlayerDepot);

                Destroy(gameObject, 5.5f);
            }
        }
    }
}

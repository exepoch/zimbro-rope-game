using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager;

namespace Trade
{
    public class InGamePlayerSetManage : MonoBehaviour
    {
        public List<GameObject> characterModels;
        public List<GameObject> itemModels;

        private void Awake()
        {
            characterModels.ForEach(o => o.SetActive(false));
            itemModels.ForEach(o => o.SetActive(false));

            characterModels[PlayerPrefs.GetInt("activeCharacterIndex")].SetActive(true);
            itemModels[PlayerPrefs.GetInt("activeItemIndex")].SetActive(true);
        }
    }
}

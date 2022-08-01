using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GamePlay
{
    public class Tower : MonoBehaviour
    {
        public GameObject towerVFX;
        
        public bool hasSet = false;

        public void PowerOn()
        {
            hasSet = true;
            towerVFX.SetActive(true);
        }
    }
}

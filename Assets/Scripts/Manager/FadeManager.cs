using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Manager
{
    public class FadeManager : MonoBehaviour
    {
        //Public Field
        public static FadeManager fadeManager;

        //Private Field
        public CanvasGroup fadePanel { get; set; }

        private void Awake()
        {
            if (fadeManager == null)
            {
                fadeManager = this;
                DontDestroyOnLoad(fadeManager);
            }
            else
            {
                Destroy(this);
            }

            fadePanel = GetComponent<CanvasGroup>();
        }

        public void FadeIn() //Kararma
        {
            fadePanel.DOFade(1, 1f);
        }

        public void FadeOut() //Aydınlanma
        {
            fadePanel.DOFade(0, 1f);
        }
    }
}

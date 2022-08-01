using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Manager;
using UnityEngine;
using UnityEngine.Rendering;

namespace GamePlay
{
    public class GameEngine : MonoBehaviour
    {
        public int successPoint;
        public int maxPoint;
        public Action WrongBiteCall;
        public CanvasGroup infoPanel;

        private DoorBehaviour door;
        private GameManager manager;
        private bool gameHasEnded;
        
        

        private void Awake()
        {
            manager = GetComponent<GameManager>();
            door = FindObjectOfType<DoorBehaviour>();
            gameHasEnded = false;
        }

        private void Start()
        {
            StartCoroutine(InfoSequence());
        }

        private void Update()
        {
            if (!gameHasEnded  && (successPoint == maxPoint) && WrongBiteCall==null)
            {
                gameHasEnded = true;
                door.OpenDoor();
            }
        }

        public void IncreasePoint()
        {
            successPoint++;
        }

        public void ResetPoint()
        {
            successPoint = 0;
        }


        public void GameFinishCall()
        {
            StartCoroutine(Finish());
        }

        IEnumerator Finish()
        {
            print("Bölüm tamamlandı");
            manager.UpdateLevelProgress();
            GetComponent<SceneManager>().ChangeScene(1);
            yield return null;
        }

        IEnumerator InfoSequence()
        {
            infoPanel.DOFade(1, 1f);
            yield return new WaitForSeconds(3f);
            infoPanel.DOFade(0, 1f).OnComplete(() =>
            {
                infoPanel.gameObject.SetActive(false);
            });
        }
    }
}

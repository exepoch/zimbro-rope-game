using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Manager;
using UnityEngine;
using SceneManager = UnityEngine.SceneManagement.SceneManager;
using Manager;

namespace GamePlay
{
    public class RopeLine : MonoBehaviour
    {
        [Header("Script Instance")] public GameEngine engine;
        
        [Space]
        public Transform player;
        public Transform ropeStartPos;
        public LineRenderer lineRenderer;
        public LayerMask collMask;

        public List<Vector3> ropePositions = new List<Vector3>();

        private Manager.SceneManager _sceneManager;
        private bool dead = false;
        private bool killSequence = false;

        private void Awake()
        {
            _sceneManager = FindObjectOfType<Manager.SceneManager>();
            AddPosToRope(ropeStartPos.position); //Rope cstom StartPos for start
        }

        private void Update()
        {
            UpdateRopePositions();
            LastSegmentGoToPlayerPos();

            DetectCollisionEnter();
            if (ropePositions.Count > 2) DetectCollisionExits();
        }

        private void DetectCollisionEnter()
        {
            RaycastHit hit;
            if (Physics.Linecast(player.position, lineRenderer.GetPosition(ropePositions.Count - 2), out hit, collMask))
            {
                ropePositions.RemoveAt(ropePositions.Count - 1);
                AddPosToRope(hit.point);

                if (ropePositions.Count > 5 && lineRenderer.GetPosition(ropePositions.Count - 3) ==
                    lineRenderer.GetPosition(ropePositions.Count - 4))
                {
                    ropePositions.RemoveAt(ropePositions.Count - 4);
                }

                if (hit.collider.gameObject.tag == "kill" && !killSequence)
                {
                    killSequence = true;
                    engine.WrongBiteCall = KillerBiteCall;
                    StartCoroutine(KillerFonk());
                }

                Tower tower = hit.collider.GetComponent<Tower>();
                if (tower && !tower.hasSet)
                {
                    tower.PowerOn();
                    engine.IncreasePoint();   
                }
            }
        }

        IEnumerator KillerFonk()
        {
            lineRenderer.GetComponent<Animator>().SetTrigger("kill");
            
            yield return new WaitForSeconds(1.5f);
            
            _sceneManager.ChangeScene(SceneManager.GetActiveScene().buildIndex);
            
            yield break;
        }

        private void DetectCollisionExits()
        {
            RaycastHit hit;
            if (!Physics.Linecast(player.position, lineRenderer.GetPosition(ropePositions.Count - 3), out hit,
                collMask))
            {
                ropePositions.RemoveAt(ropePositions.Count - 2);
            }
        }

        public void AddPosToRope(Vector3 _pos)
        {
            ropePositions.Add(_pos);
            ropePositions.Add(player.position); //Always the last pos must be the player
        }

        private void UpdateRopePositions()
        {
            lineRenderer.positionCount = ropePositions.Count;
            lineRenderer.SetPositions(ropePositions.ToArray());
        }

        private void LastSegmentGoToPlayerPos()
        {
            Vector3 lasPos = player.localPosition;
            lineRenderer.SetPosition(lineRenderer.positionCount - 1, lasPos);
        }

        private void KillerBiteCall() { } // Fonksiyon boş - Bitiş Actionu doldurmak için kullanıldı Bitise özel özellik istenirse doldurulabilir.
    }
}

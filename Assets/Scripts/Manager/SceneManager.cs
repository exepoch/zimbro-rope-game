using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
 using UnityEngine;
 using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

namespace Manager
{
    public class SceneManager : MonoBehaviour
    {
        public Transform topCamTransform;
        public Transform thirdPersonCamTransfom;
        public List<Toggle> isON;

        private PostProcessVolume volume;
        private Transform cam;
        private bool camChech = false;

        private void Awake()
        {
            cam = Camera.main.transform;
            volume = FindObjectOfType<PostProcessVolume>();
            SetDefaultIfFirst();
            SetUICheckMark();
            if (volume != null) LoadPostEffects();
        }

        public void ChangeScene(int SceneIndex)
        {
            StartCoroutine(LoadLevelScene(SceneIndex));
        }

        public void Mute()
        {
            AudioListener.volume = 0;
        }
        public void UnMute()
        {
            AudioListener.volume = 1;
        }


        public void ChangeCamOri()
        {
            if (camChech)
            {
                cam.DOComplete();
                cam.DOMove(topCamTransform.position, 2f);
                
            }
            else
            {
                cam.DOComplete();
                cam.DOMove(thirdPersonCamTransfom.position, 2f);
            }

            camChech = !camChech;
        }

        IEnumerator LoadLevelScene(int index)
        {
            AsyncOperation asyncOperation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(index);
            asyncOperation.allowSceneActivation = false;
            FadeManager.fadeManager.FadeIn();

            while (!asyncOperation.isDone)
            {
                if(asyncOperation.progress>=.9f) break;
                yield return null;
            }
            yield return new WaitForSeconds(1f);
            asyncOperation.allowSceneActivation = true;
            FadeManager.fadeManager.FadeOut();
        }
        
        
        //GeneralSettings
        public void SetGraphicLevel(TMP_Dropdown menu)
        {
            int x = menu.value+1;
            QualitySettings.SetQualityLevel(x);
        }
        
        //PostProccess Effects
        public void AmbientOcclusion()
        {
            var x = PlayerPrefs.GetInt("ambientocclusion");
            PlayerPrefs.SetInt("ambientocclusion", 1-x);
        }
        public void SetBloom()
        {
            var x = PlayerPrefs.GetInt("bloom");
            PlayerPrefs.SetInt("bloom", 1-x);
        }
        public void SetVignette()
        {
            var x = PlayerPrefs.GetInt("vignette");
            PlayerPrefs.SetInt("vignette", 1-x);
        }
        public void SetFilmGrain()
        {
            var x = PlayerPrefs.GetInt("filmgrain");
            PlayerPrefs.SetInt("filmgrain", 1-x);
        }
        public void SetMotionBlur()
        {
            var x = PlayerPrefs.GetInt("motionblur");
            PlayerPrefs.SetInt("motionblur", 1-x);
        }
        
        private void LoadPostEffects()
        {
            int x;

            //Vigentte
            x = PlayerPrefs.GetInt("vignette");
            volume.sharedProfile.settings[0].active = x == 0 ? false : true;

            //Bloom
            x = PlayerPrefs.GetInt("bloom");
            volume.sharedProfile.settings[1].active = x == 0 ? false : true;


            //MotionBlur
            x = PlayerPrefs.GetInt("motionblur");
            volume.sharedProfile.settings[2].active = x == 0 ? false : true;

            //Ambient Occlusion
            x = PlayerPrefs.GetInt("ambientocclusion");
            volume.sharedProfile.settings[3].active = x == 0 ? false : true;

            //Film Grain
            x = PlayerPrefs.GetInt("filmgrain");
            volume.sharedProfile.settings[4].active = x == 0 ? false : true;
        }

        private void SetDefaultIfFirst()
        {
            if(!PlayerPrefs.HasKey("vignette"))
                PlayerPrefs.SetInt("vignette", 1);
            if(!PlayerPrefs.HasKey("bloom"))
                PlayerPrefs.SetInt("bloom", 1);
            if(!PlayerPrefs.HasKey("motionblur"))
                PlayerPrefs.SetInt("motionblur", 1);
            if(!PlayerPrefs.HasKey("ambientocclusion"))
                PlayerPrefs.SetInt("ambientocclusion", 1);
            if(!PlayerPrefs.HasKey("filmgrain"))
                PlayerPrefs.SetInt("filmgrain", 1);
        }

        private void SetUICheckMark()
        {
            if(isON.Count == 0) return;
            int x;
            
            x = PlayerPrefs.GetInt("vignette");
            isON[0].isOn = x == 1 ? true : false;
            
            x = PlayerPrefs.GetInt("bloom");
            isON[1].isOn = x == 1 ? true : false;
            
            x = PlayerPrefs.GetInt("motionblur");
            isON[2].isOn = x == 1 ? true : false;
            
            x = PlayerPrefs.GetInt("ambientocclusion");
            isON[3].isOn = x == 1 ? true : false;
            
            x = PlayerPrefs.GetInt("filmgrain");
            isON[4].isOn = x == 1 ? true : false;
        }
    }
}

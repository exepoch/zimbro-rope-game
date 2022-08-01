using System;
using System.Collections;
using System.Collections.Generic;
using GamePlay;
using UnityEngine;

public class DoorBehaviour : MonoBehaviour
{
    public GameObject openVFX;
    
    private GameEngine gEngine;
    private bool hastTriggered = false;
    
    private void Awake()
    {
        gEngine = FindObjectOfType<GameEngine>();
    }

    public void OpenDoor()
    {
        openVFX.SetActive(true);
        GetComponent<BoxCollider>().enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(hastTriggered) return;
        hastTriggered = true;
        
        gEngine.GameFinishCall();
    }
}

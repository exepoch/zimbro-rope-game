using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnAroundSelf : MonoBehaviour
{
    Transform tra;
    private void Awake()
    {
        tra = GetComponent<Transform>();
    }
    private void FixedUpdate()
    {
        tra.transform.Rotate(new Vector3(0, 1, 0));
    }
}

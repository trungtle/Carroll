﻿using UnityEngine;
using System.Collections;

public class ropeEndPushTarget : MonoBehaviour
{

    // Use this for initialization
    void Start ()
    {
	
    }
	
    // Update is called once per frame
    void Update ()
    {
	
    }

    void OnTriggerEnter (Collider other)
    {
        if (other.CompareTag ("Player")) {
            Character character = other.GetComponentInParent<Character> ();
            character.AliceOntoPlatformComplete ();
        }
    }
}
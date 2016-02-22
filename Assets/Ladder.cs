using UnityEngine;
using System.Collections;

public class Ladder : MonoBehaviour
{
    public bool isBottom;

    private bool characterClimbing = false;
    private bool isInsideTrigger = false;

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


        }
            
    }

    void OnTriggerExit (Collider other)
    {
        if (other.CompareTag ("Player")) {
       
        }
    }
}

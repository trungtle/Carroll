using UnityEngine;
using System.Collections;

public class RopeTrigger : MonoBehaviour
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
            character.SetIsNearRope (true);
        }
    }

    void OnTriggerExit (Collider other)
    {
        if (other.CompareTag ("Player")) {
            Character character = other.GetComponentInParent<Character> ();
            character.SetIsNearRope (false);

        }
    }

}

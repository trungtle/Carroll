using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Potion : MonoBehaviour
{

    public string potionType = "";
    public Text pressEText;

    private bool consumed = false;
    private Rigidbody rigidBody;

    // Use this for initialization
    void Start ()
    {
        rigidBody = GetComponent<Rigidbody> ();
    }
	
    // Update is called once per frame
    void Update ()
    {
	
    }

    void OnTriggerEnter (Collider other)
    {
        if (other.CompareTag ("Player") && !consumed) {
            Character character = other.GetComponentInParent<Character> ();
            character.SetIsNearPotion (true, this);
            pressEText.enabled = true;
        }
    }

    void OnTriggerExit (Collider other)
    {
        if (other.CompareTag ("Player")) {
            Character character = other.GetComponentInParent<Character> ();
            character.SetIsNearPotion (false, null);
            pressEText.enabled = false;
        }
    }

    public void Consume ()
    {
        consumed = true;
        Debug.Log (transform.position.ToString () + " consumed");
        rigidBody.AddExplosionForce (100.0f, transform.position - new Vector3 (1, 4, 2), 10.0f);
        Invoke ("OnExplosionComplete", 2);
    }

    void OnExplosionComplete ()
    {
        Destroy (transform.parent.gameObject);
    }
}

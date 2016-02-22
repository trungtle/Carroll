using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{
    public Image endGameScreen;
    public Text endGameText;

    private bool animateEndGame = false;

    // Use this for initialization
    void Start ()
    {
    }
	
    // Update is called once per frame
    void Update ()
    {
        if (animateEndGame) {
            endGameScreen.color = Color.Lerp (endGameScreen.color, Color.white, Time.deltaTime);
            endGameText.color = Color.Lerp (endGameText.color, Color.black, Time.deltaTime);
        }
    }

    void OnTriggerEnter (Collider other)
    {
        if (other.CompareTag ("Player")) {
            
            animateEndGame = true;
        }
    }
}

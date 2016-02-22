using UnityEngine;
using System.Collections;
using UnityEngine.UI;

using UnityStandardAssets.Characters.FirstPerson;

public class Character : MonoBehaviour
{
    public Transform aliceXform;
    public Camera aliceCamera;
    public Text log;
    public Image keyIcon;

    // Scale stuff
    public float SCALE_BIG;
    public float SCALE_NORMAL;
    public float SCALE_HALF;
    public float SCALE_TINY;
    public float startScaleFactor;
    public float scaleSpeed = 2;

    private Vector3 targetScale;
    private Vector3 baseScale;

    // FOV stuff
    public float FOV_BIG;
    public float FOV_NORMAL;
    public float FOV_HALF;
    public float FOV_TINY;
    public float fovSpeed = 2;

    private float targetFOV;

    // Location
    private Vector3 targetPosition;
    private float positionDirectSpeed = 0.1f;

    public float MASS_BIG;
    public float MASS_NORMAL;
    public float MASS_HALF;
    public float MASS_TINY;

    private bool isNearPotion = false;
    private Potion potion;
    private string potionTypeDebug = "normal";
  
    private Animator animController;
    private Rigidbody rigidBody;

    // Game logic variables

    // Rope
    public GameObject lookAtWhileOnRope;
    public GameObject ropeEndToPlatform;
    private bool isNearRope = false;
    public bool needToBeMoved = false;


    private bool moveOnClockPath;

    // Use this for initialization
    void Start ()
    {
        // Initialize scaling variables
        baseScale = transform.localScale;
        transform.localScale = baseScale * startScaleFactor;
        targetScale = transform.localScale * startScaleFactor;
        targetFOV = FOV_NORMAL;

        // Grab components
        animController = GetComponentInChildren<Animator> ();
        rigidBody = GetComponentInChildren<Rigidbody> ();
    }
	
    // Update is called once per frame
    void Update ()
    {
        // For  quick debugging of scaling by cycle through different scaling factors
        if (Input.GetKeyDown ("t")) {
            DebugScale ();
        }

        // Drinking potion logic
        if (potion && isNearPotion && Input.GetKeyDown ("e")) {
            Debug.Log (potion.potionType);

            AliceDrinkPotion ();
        }

        if (isNearRope && Input.GetKeyDown ("e")) {
            AliceFollowRope ();
        }

        // Perform transforming if there's a new target scale
        AliceTransform ();


        // Restart game
        if (Input.GetKeyDown ("r")) {
            UnityEngine.SceneManagement.SceneManager.LoadScene ("Level1");
        }

        // Quit game
        if (Input.GetKeyDown ("q")) {
            Application.Quit ();
        }
    }

    void DebugScale ()
    {
        animController.SetTrigger ("Xforming");
        switch (potionTypeDebug) {
        case "grow":
            {
                Debug.Log ("grow");
                AliceChangeSize (SCALE_BIG, FOV_BIG);
                potionTypeDebug = "normal";
                break;
            }
        case "normal":
            {
                Debug.Log ("normal");
                AliceChangeSize (SCALE_NORMAL, FOV_NORMAL);
                potionTypeDebug = "shrink_half";
                break;
            }
        case "shrink_half":
            {
                Debug.Log ("shrink_half");
                AliceChangeSize (SCALE_HALF, FOV_HALF);
                potionTypeDebug = "shrink_tiny";
                break;
            }
        case "shrink_tiny":
            {
                Debug.Log ("shrink_tiny");
                AliceChangeSize (SCALE_TINY, FOV_TINY);
                potionTypeDebug = "grow";
                break;
            }
        default:
            break;           
        }        
    }


    void AliceDrinkPotion ()
    {        
        animController.SetTrigger ("Xforming");
        string potionType = potion.potionType;
        potion.Consume ();
        Hallucinate ();
        log.text = "Drank " + potionType;
        switch (potionType) {
        case "grow":
            AliceChangeSize (SCALE_BIG, FOV_BIG);
            break;
        case "normal":
            AliceChangeSize (SCALE_NORMAL, FOV_NORMAL);
            break;
        case "shrink_half":
            keyIcon.enabled = true;
            AliceChangeSize (SCALE_HALF, FOV_HALF);
            break;
        case "shrink_tiny":
            AliceChangeSize (SCALE_TINY, FOV_TINY);
            break;
        default:
            break;           
        }        
    }

    void Hallucinate ()
    {
        aliceCamera.transform.Rotate (aliceCamera.transform.forward);
    }

    void AliceFollowRope ()
    {
        rigidBody.isKinematic = true;
        needToBeMoved = true;
        positionDirectSpeed = 0.1f;
        targetPosition = transform.position + Vector3.up * 5;
        iTween.LookTo (gameObject, iTween.Hash ("looktarget", lookAtWhileOnRope, "time", 5));
    }

    public void AliceOntoPlatform ()
    {
        targetPosition = transform.position + Vector3.left * 3f;
        positionDirectSpeed = 1f;
        needToBeMoved = true;
    }

    public void AliceOntoPlatformComplete ()
    {
        rigidBody.isKinematic = false;
        needToBeMoved = false;
    }

    void AliceTransform ()
    {
        if (needToBeMoved) {
            transform.position = Vector3.Lerp (transform.position, targetPosition, positionDirectSpeed * Time.deltaTime);
        }
        transform.localScale = Vector3.Lerp (transform.localScale, targetScale, scaleSpeed * Time.deltaTime);
        aliceCamera.fieldOfView = Mathf.Lerp (aliceCamera.fieldOfView, targetFOV, fovSpeed * Time.deltaTime);
    }

    void AliceChangeSize (float factor, float fov)
    {
        targetFOV = fov;
        targetScale = new Vector3 (baseScale.x, factor * baseScale.y, baseScale.z);
    }

    public void SetIsNearPotion (bool isNearP, Potion p)
    {
        isNearPotion = isNearP;
        potion = p;
    }

    public void SetIsNearRope (bool isNearR)
    {
        isNearRope = isNearR;
    }

    void onBackToDynamicsComplete ()
    {
//        rigidBody.isKinematic = false;
    }

    public void EnableKinematic (bool en)
    {
        rigidBody.isKinematic = en;
    }

}

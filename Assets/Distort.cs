using System.Collections;
using UnityEngine;

public class Distort : MonoBehaviour
{

    Camera cam;
    bool go = true;
    bool forward;
    bool approachingGoal = false;
    float currentFOV;
    float targetFOV = 70;
    float stepSize;
    // Use this for initialization
    void Start ()
    {
        cam = GetComponent<Camera> ();
        forward = true;

        currentFOV = cam.fieldOfView;
        stepSize = 360.0f / (240 - currentFOV - targetFOV); 
    }

    // Update is called once per frame
    void Update ()
    {
        if (go) {
            cam.fieldOfView = currentFOV;
            cam.transform.Rotate (new Vector3 (0, 0, stepSize));
            //float degree = Mathf.Sin (seed) * 10;

            if (forward) {
                currentFOV++;
            } else {
                currentFOV--;
            }

            if (cam.fieldOfView >= 120) {
                forward = false;
                approachingGoal = true;
            } else if (approachingGoal && cam.fieldOfView <= targetFOV) {
                go = false;
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationCam : MonoBehaviour
{
    public GameObject personnage; 
    public GameObject camera3emePersonne; 
    public GameObject positionRayCastCamera; 
    public float hauteurPivot; 
    public float distanceCameraLoin; 
    public float distanceCameraPret; 
    public float vitesseCameraX; 
    public float vitesseCameraY; 
    
    void Start(){
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        transform.position = personnage.transform.position + new Vector3(0, hauteurPivot, 0);
        transform.Rotate(-Input.GetAxis("Mouse Y") * vitesseCameraY, Input.GetAxis("Mouse X") * vitesseCameraX, 0);

        transform.localEulerAngles = new Vector3(Mathf.Clamp(transform.localEulerAngles.x, 10, 50), transform.localEulerAngles.y, 0);

            camera3emePersonne.transform.localPosition = new Vector3(0, 0, distanceCameraLoin);

    }
}

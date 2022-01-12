using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationCam : MonoBehaviour
{
    public GameObject personnage; 
    public GameObject camera3emePersonne; 
    public GameObject positionRayCastCamera; 
    public float hauteurPivot; 
    public float distanceCameraLoin = -5; 
    public float distanceCameraPret = -2; 
    public float vitesseCameraX; 
    public float vitesseCameraY; 
    public static bool jeuPause; 
    
    

    void Update()
    {
        transform.position = personnage.transform.position + new Vector3(0, hauteurPivot, 0);
        transform.Rotate(-Input.GetAxis("Mouse Y") * vitesseCameraY, Input.GetAxis("Mouse X") * vitesseCameraX, 0);

        transform.localEulerAngles = new Vector3(Mathf.Clamp(transform.localEulerAngles.x, 10, 80), transform.localEulerAngles.y, 0);

        if (Physics.Raycast(positionRayCastCamera.transform.position, positionRayCastCamera.transform.forward, -distanceCameraLoin))
        {
            camera3emePersonne.transform.localPosition = new Vector3(0, 1, distanceCameraPret);
        }
        else
        {
            camera3emePersonne.transform.localPosition = new Vector3(0, 0, distanceCameraLoin);
        }

    }
}

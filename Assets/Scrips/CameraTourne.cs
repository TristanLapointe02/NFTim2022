using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTourne : MonoBehaviour
{
    public float vitesse;

    void Update()
    {
        // Exercer une rotation
        transform.Rotate(0, vitesse * Time.deltaTime, 0);
    }
}

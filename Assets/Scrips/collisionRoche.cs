using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collisionRoche : MonoBehaviour
{
    bool etourdi;

    private void OnCollisionEnter(Collision infoCollisionRoche)
    {
        if (infoCollisionRoche.gameObject.tag == "Player")
        {
            print("roche touche autre joueur");
            Destroy(gameObject);
            // linker au script pour faire le joueur touche etourdi
            // etourdi = true;
        }
    }

}

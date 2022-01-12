using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class testPointsAndy : MonoBehaviour
{
    private void OnTriggerEnter(Collider infoCollisionAnimaux)
    {
        if (infoCollisionAnimaux.gameObject.tag == "vache")
        {
            scoreManager.instance.pointsVache();
            Destroy(infoCollisionAnimaux.gameObject);
        }

        else if (infoCollisionAnimaux.gameObject.tag == "mouton")
        {
            scoreManager.instance.pointsMouton();
            Destroy(infoCollisionAnimaux.gameObject);
        }

        else if (infoCollisionAnimaux.gameObject.tag == "chien")
        {
            scoreManager.instance.pointsChien();
            Destroy(infoCollisionAnimaux.gameObject);
        }

        else if (infoCollisionAnimaux.gameObject.tag == "cheval")
        {
            scoreManager.instance.pointsCheval();
            Destroy(infoCollisionAnimaux.gameObject);
        }

        else if (infoCollisionAnimaux.gameObject.tag == "lama")
        {
            scoreManager.instance.pointsLama();
            Destroy(infoCollisionAnimaux.gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class testPointsAndy : MonoBehaviour
{
    private void OnTriggerEnter(Collider infoCollision)
    {
        if (infoCollision.gameObject.tag == "vache")
        {
            scoreManager.instance.pointsVache();
        }

        else if (infoCollision.gameObject.tag == "mouton")
        {
            scoreManager.instance.pointsMouton();
        }

        else if (infoCollision.gameObject.tag == "chien")
        {
            scoreManager.instance.pointsChien();
        }

        else if (infoCollision.gameObject.tag == "cheval")
        {
            scoreManager.instance.pointsCheval();
        }

        else if (infoCollision.gameObject.tag == "lama")
        {
            scoreManager.instance.pointsLama();
        }
    }
}

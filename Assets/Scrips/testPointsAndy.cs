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
            Destroy(infoCollision.gameObject);
        }

        else if (infoCollision.gameObject.tag == "mouton")
        {
            scoreManager.instance.pointsMouton();
            Destroy(infoCollision.gameObject);
        }

        else if (infoCollision.gameObject.tag == "chien")
        {
            scoreManager.instance.pointsChien();
            Destroy(infoCollision.gameObject);
        }

        else if (infoCollision.gameObject.tag == "cheval")
        {
            scoreManager.instance.pointsCheval();
            Destroy(infoCollision.gameObject);
        }

        else if (infoCollision.gameObject.tag == "lama")
        {
            scoreManager.instance.pointsLama();
            Destroy(infoCollision.gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using Photon.Pun;

public class testPointsAndy : MonoBehaviourPunCallbacks
{
    public static string animalPris;
    public void OnTriggerEnter(Collider infoCollisionAnimaux)
    {
        //Si c'est un animal...
        if(infoCollisionAnimaux.gameObject.layer == 6)
        {
            print("tag de l'animal" + animalPris);
            animalPris = infoCollisionAnimaux.gameObject.tag.ToString();
        }
    }
}

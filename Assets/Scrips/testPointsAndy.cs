using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using Photon.Pun;

public class testPointsAndy : MonoBehaviourPunCallbacks
{
    public static string animalPris;
    private void OnTriggerEnter(Collider infoCollisionAnimaux)
    {
        if (infoCollisionAnimaux.gameObject.tag == "vache")
        {
            animalPris = infoCollisionAnimaux.gameObject.tag.ToString();
            PhotonNetwork.Destroy(infoCollisionAnimaux.gameObject);
        }
    }
}

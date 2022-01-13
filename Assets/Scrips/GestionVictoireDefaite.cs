using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using UnityEngine.UI;
using TMPro;

public class GestionVictoireDefaite : MonoBehaviourPunCallbacks
{
    public TextMeshProUGUI texteFin;
   
    // Update is called once per frame
    void Update()
    {
        if (gestionScore.pointage1 > gestionScore.pointage2)
        {
            texteFin.text = PhotonNetwork.PlayerList[0].NickName + " est gagnant!!!";
        }
        else if (gestionScore.pointage2 > gestionScore.pointage1)
        {
            texteFin.text = PhotonNetwork.PlayerList[1].NickName + " est gagnant!!!";
        }
    }
    
    //Quitter la partie
    public void QuitterPartie()
    {
        //Fermer l'appli
        Application.Quit();
    }
}

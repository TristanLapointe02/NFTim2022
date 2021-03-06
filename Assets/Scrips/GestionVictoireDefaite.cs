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
   

    void Start(){
        Cursor.lockState = CursorLockMode.None;
    }
    // Update is called once per frame
    void Update()
    {
        if (PhotonNetwork.PlayerList.Length == 2)
        {
            if (gestionScore.pointage1 > gestionScore.pointage2)
            {
                texteFin.text = PhotonNetwork.PlayerList[0].NickName + " est gagnant!!!";
            }
            else if (gestionScore.pointage2 > gestionScore.pointage1)
            {
                texteFin.text = PhotonNetwork.PlayerList[1].NickName + " est gagnant!!!";
            }
            else
            {
                texteFin.text = "Partie nulle";
            }
        }     
    }
    
    //Quitter la partie
    public void QuitterPartie()
    {
        //Fermer l'appli
        Application.Quit();
    }
}

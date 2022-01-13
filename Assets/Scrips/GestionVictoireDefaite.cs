using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

public class GestionVictoireDefaite : MonoBehaviourPunCallbacks
{
    public GameObject texteVictoire;
    public GameObject texteDefaite;
   
    // Update is called once per frame
    void Update()
    {
    if (PhotonNetwork.LocalPlayer == PhotonNetwork.PlayerList[0] && photonView.IsMine)
            {
                print("Je suis joueur 0");
                if(gestionScore.pointage1 > gestionScore.pointage2){
                    texteVictoire.SetActive(true);
                    texteDefaite.SetActive(false);
                    print(" joueur 0 à gagné");
                }
                else if(gestionScore.pointage1 < gestionScore.pointage2){
                    texteVictoire.SetActive(false);
                    texteDefaite.SetActive(true);
                }else{
                    texteVictoire.SetActive(true);
                    texteDefaite.SetActive(false);
                }
            }
    if (PhotonNetwork.LocalPlayer == PhotonNetwork.PlayerList[1] && photonView.IsMine)
        {
            print("Je suis joueur 1");
            if(gestionScore.pointage2 > gestionScore.pointage1){
                    texteVictoire.SetActive(true);
                    texteDefaite.SetActive(false);
                    print(" joueur 1 à gagné");
                }
                else if(gestionScore.pointage2 < gestionScore.pointage1){
                    texteVictoire.SetActive(false);
                    texteDefaite.SetActive(true);
                }else{
                    texteVictoire.SetActive(true);
                    texteDefaite.SetActive(false);
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

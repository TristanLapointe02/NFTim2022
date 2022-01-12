using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GererPersoMulti : MonoBehaviourPunCallbacks
{
    public static GameObject joueurLocal;
    public GameObject posJoueur1;
    public GameObject posJoueur2;
    // Start is called before the first frame update
    void Start()
    {
         //INSTANCIER Edgar
        if (GestionConnexion.PersonnageChoisi == "Edgar" && PhotonNetwork.LocalPlayer.IsMasterClient ==true)
        {
            joueurLocal = PhotonNetwork.Instantiate("Edgar", posJoueur1.transform.position, Quaternion.identity, 0, null);
        }else{
            joueurLocal = PhotonNetwork.Instantiate("Edgar", posJoueur2.transform.position, Quaternion.identity, 0, null);
        }
        //INSTANCIER Joseph
        if (GestionConnexion.PersonnageChoisi == "Joseph" && PhotonNetwork.LocalPlayer.IsMasterClient ==true)
        {
            joueurLocal = PhotonNetwork.Instantiate("Joseph", posJoueur1.transform.position, Quaternion.identity, 0, null);
        }else{
            joueurLocal = PhotonNetwork.Instantiate("Joseph", posJoueur2.transform.position, Quaternion.identity, 0, null);
        }
    }

    // Update is called once per frame
    void Update()
    {
         //Permettre au joueur de quitter avec escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //Dire � tous les joueur qu'il est d�connect�
            // photonView.RPC("AfficherTexteDeco", RpcTarget.Others, GestionConnexionSallePrive.NomJoueurStatic);

            //Quitter la salle du serveur
            PhotonNetwork.LeaveRoom();
        }
    }
}

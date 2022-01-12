using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GererPersoMulti : MonoBehaviourPunCallbacks
{
    public Vector3[] positions; //Positions aléatoires où le joueur peut spawn
    public int positionTableau; //Position dans le tableau pigée au hasard quand on fait spawn le joueur
    public static GameObject joueurLocal;
    // Start is called before the first frame update
    void Start()
    {
        //Choisir une valeur aléatoire de position de spawn du personnage
        positionTableau = Random.Range(0, 12);
         //INSTANCIER Edgar
        if (GestionConnexion.PersonnageChoisi == "Edgar")
        {
            joueurLocal = PhotonNetwork.Instantiate("Edgar", positions[positionTableau], Quaternion.identity, 0, null);
        }
        //INSTANCIER Joseph
        if (GestionConnexion.PersonnageChoisi == "Joseph")
        {
            joueurLocal = PhotonNetwork.Instantiate("Joseph", positions[positionTableau], Quaternion.identity, 0, null);
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

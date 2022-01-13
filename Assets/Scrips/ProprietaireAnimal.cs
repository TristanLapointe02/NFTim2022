using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

public class ProprietaireAnimal : MonoBehaviourPunCallbacks
{
    public GameObject joueurAvecAnimal;
    public static bool tiensAnimal;
    /* public GameObject refControleurJeu; */

    public void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        //Suivre le mouvement du joueur
         if((joueurAvecAnimal.gameObject.tag != "mouton" || joueurAvecAnimal.gameObject.tag != "cochon"
        || joueurAvecAnimal.gameObject.tag != "cheval" || joueurAvecAnimal.gameObject.tag != "chien"
        || joueurAvecAnimal.gameObject.tag != "lama" || joueurAvecAnimal.gameObject.tag != "vache"
        || joueurAvecAnimal.gameObject.tag != "zebre") && photonView.IsMine && tiensAnimal == true && joueurAvecAnimal != null)
        {
            gameObject.transform.position = joueurAvecAnimal.transform.position;
            // La ligne commenté explose la vitesse des autres animaux, faudrait trouver un moyen de target seulement l'animal picked up
            /* gameObject.transform.position = joueurAvecAnimal.transform.position + transform.up * 0.5f + transform.forward * 1.1f; */
        } 
        
    }

    public void OnTriggerEnter(Collider collision)
    {
        //Si le drapeau touche un joueur local
        if (collision.gameObject.tag == "Player")
        {
            tiensAnimal = true;

            //Transf�rer le ownership 
            photonView.TransferOwnership(PhotonNetwork.LocalPlayer);

            //Garder en m�moire quel joueur/gameobject le drapeau doit suivre
            joueurAvecAnimal = collision.gameObject;

        }

        if(collision.gameObject.name == "CAGE1"){
            Destroy(gameObject);
        }
    }
}

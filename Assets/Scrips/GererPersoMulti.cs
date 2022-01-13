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
    public GameObject quitterPanneau;
    public GameObject[] FR;
    public GameObject[] EN;
    bool ctrlActive; // est-ce que les commandes sont affichés
    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            //Choisir une valeur aléatoire de position de spawn du personnage
            positionTableau = Random.Range(0, 7);
            //INSTANCIER Edgar
            if (GestionConnexion.PersonnageChoisi == "Edgar")
            {
                joueurLocal = PhotonNetwork.Instantiate("ParentEdgard", positions[positionTableau], Quaternion.identity, 0, null);
            }
            //INSTANCIER Joseph
            if (GestionConnexion.PersonnageChoisi == "Joseph")
            {
                joueurLocal = PhotonNetwork.Instantiate("ParentJoseph", positions[positionTableau], Quaternion.identity, 0, null);
            }
            //CHANGER NOM JOUEUR LOCAL         
            joueurLocal.name = PhotonNetwork.LocalPlayer.NickName;
        }  
    }

    // Update is called once per frame
    void Update()
    {
        //Permettre au joueur de quitter avec escape
        if (Input.GetKeyDown(KeyCode.Escape) && ctrlActive == false)
        {
            ctrlActive = true;
            quitterPanneau.SetActive(true); // ouvrir menu
            Cursor.lockState = CursorLockMode.None;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && ctrlActive == true)
        {
            ctrlActive = false;
            Cursor.lockState = CursorLockMode.Locked;
            quitterPanneau.SetActive(false); // fermer menu
        }
    }

    public void Quitter()
    {
        //Fermer l'appli
        Application.Quit();
    }
    public void toutFR()
    {
        foreach (GameObject obj in FR)
        {
            obj.SetActive(true);
        }

        foreach (GameObject obj in EN)
        {
            obj.SetActive(false);
        }
    }
    public void toutEN()
    {
        foreach (GameObject obj in EN)
        {
            obj.SetActive(true);
        }

        foreach (GameObject obj in FR)
        {
            obj.SetActive(false);
        }
    }
}


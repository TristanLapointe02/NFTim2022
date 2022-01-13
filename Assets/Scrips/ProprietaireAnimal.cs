using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

public class ProprietaireAnimal : MonoBehaviourPunCallbacks
{
    public GameObject joueurAvecAnimal;
    public static bool tiensAnimal;
    public AudioClip sonVache;
    public AudioClip sonMouton;
    public AudioClip sonChien;
    public AudioClip sonCheval;
    public AudioClip sonLama;
    public AudioClip sonZebre;
    public AudioClip sonCochon;
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
        || joueurAvecAnimal.gameObject.tag != "zebre") && photonView.IsMine && tiensAnimal == true && joueurAvecAnimal != null && TimerPartieMultiplayer.partieCommencer == true)
        {
            gameObject.transform.position =  joueurAvecAnimal.transform.position;
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

            switch (gameObject.tag)
            {
                case "vache":
                    photonView.RPC("JouerSonVache", RpcTarget.All);
                    break;
                case "mouton":
                    photonView.RPC("JouerSonMouton", RpcTarget.All);
                    break;
                case "pug":
                    photonView.RPC("JouerSonChien", RpcTarget.All);
                    break;
                case "cheval":
                    photonView.RPC("JouerSonCheval", RpcTarget.All);
                    break;
                case "lama":
                    photonView.RPC("JouerSonLama", RpcTarget.All);
                    break;
                case "zebre":
                    photonView.RPC("JouerSonZebre", RpcTarget.All);
                    break;
                case "cochon":
                    photonView.RPC("JouerSonCochon", RpcTarget.All);
                    break;
                default:
                    break;
            }
        }

        if(collision.gameObject.name == "CAGE1"){
            PhotonNetwork.Destroy(gameObject);
        }
    }

    [PunRPC]
    public void JouerSonVache()
    {
        GetComponent<AudioSource>().PlayOneShot(sonVache);
    }

    [PunRPC]
    public void JouerSonMouton()
    {
        GetComponent<AudioSource>().PlayOneShot(sonMouton);
    }

    [PunRPC]
    public void JouerSonChien()
    {
        GetComponent<AudioSource>().PlayOneShot(sonChien);
    }

    [PunRPC]
    public void JouerSonCheval()
    {
        GetComponent<AudioSource>().PlayOneShot(sonCheval);
    }

    [PunRPC]
    public void JouerSonLama()
    {
        GetComponent<AudioSource>().PlayOneShot(sonLama);
    }

    [PunRPC]
    public void JouerSonZebre()
    {
        GetComponent<AudioSource>().PlayOneShot(sonZebre);
    }

    [PunRPC]
    public void JouerSonCochon()
    {
        GetComponent<AudioSource>().PlayOneShot(sonCochon);
    }
}
